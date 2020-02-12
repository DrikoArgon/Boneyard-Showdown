using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerSpotted {
    Player1,
    Player2,
    None
}

public class DetectionSystem {
    private Transform player1;
    private Transform player2;
    private Transform enemyTransform;

    private Transform currentTarget;
    private Player currentTargetVariables;
    private Player player1Variables;
    private Player player2Variables;

    private bool targetSpotted;
    private float detectionRange;

    public DetectionSystem(Transform transform, Transform player1, Transform player2,float detectionRange) {

        enemyTransform = transform;
        this.detectionRange = detectionRange; 
        this.player1 = player1;
        this.player2 = player2;

        player1Variables = player1.GetComponent<Player>();
        player2Variables = player2.GetComponent<Player>();
    }

    void LookForTarget() {

        if ((Vector2.Distance(enemyTransform.position, player1.transform.position) <= detectionRange || Vector2.Distance(enemyTransform.position, player2.transform.position) <= detectionRange)) {

            if (Vector2.Distance(enemyTransform.position, player1.transform.position) <= detectionRange && !player1Variables.dead) {     
                
                currentTarget = player1;

            } else {

                if (!player2Variables.dead) {
                    currentTarget = player2;

                }
                
            }

            if(currentTarget != null) {
                currentTargetVariables = currentTarget.GetComponent<Player>();
                targetSpotted = true;
            }
   
        }

    }

    void CheckDistanceToTarget() {
        if (Vector2.Distance(enemyTransform.position, currentTarget.position) > detectionRange || currentTargetVariables.dead) {
            targetSpotted = false;
            currentTarget = null;
        }
    }

    public void HandlePlayerDetection() {

        if (!targetSpotted) {

            LookForTarget();

        } else {

            CheckDistanceToTarget();

        }
    }

    public Transform GetCurrentTarget() {
        return currentTarget;
    }

    public bool IsTargetDetected() {
        return targetSpotted;
    }
}
