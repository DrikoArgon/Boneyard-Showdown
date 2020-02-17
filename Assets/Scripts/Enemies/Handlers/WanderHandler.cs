using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderHandler {

    private float actionDelayTime;
    private float elapsedTime = 0;

    private Vector3 currentTarget;

    private int currentWaypointIndex;

    private AIPath ai;
    private List<Vector3> currentWaypoints = new List<Vector3>();

    private bool isWaiting;

    public WanderHandler(AIPath aiPath) {
        ai = aiPath;
    }

    public void WaypointWander(float timeBetweenMovements, Transform[] waypoints) {

        if(currentWaypoints.Count == 0) {
            foreach(Transform waypoint in waypoints) {
                currentWaypoints.Add(waypoint.position);
            }
        }

        if (isWaiting) {

            elapsedTime += Time.deltaTime;

            if (elapsedTime > timeBetweenMovements) {
                elapsedTime = 0;
                isWaiting = false;
            }

        } else {

            if (ai.reachedEndOfPath) {
                isWaiting = true;
                currentWaypointIndex++;

                if(currentWaypointIndex >= currentWaypoints.Count) {
                    currentWaypointIndex = 0;
                }
            }

            if (!ai.pathPending && (ai.reachedEndOfPath || !ai.hasPath)) {

                ai.destination = currentWaypoints[currentWaypointIndex];
                ai.SearchPath();
            }

        }

    }

    public void RandomWander(float timeBetweenMovements, CircleCollider2D randomMovementRadius) {

        if (isWaiting) {

            elapsedTime += Time.deltaTime;

            if(elapsedTime > timeBetweenMovements) {
                elapsedTime = 0;
                isWaiting = false;
            }

        } else {

            if (ai.reachedEndOfPath) {
                isWaiting = true;
            }      

            if (!ai.pathPending && (ai.reachedEndOfPath || !ai.hasPath)) {

                Vector3 point = Random.insideUnitCircle * randomMovementRadius.radius;

                point += ai.position;
                ai.destination = point;
                ai.SearchPath();
            }

        }
     
    }

}
