using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseHandler 
{

    private Animator enemyAnimator;
    private Rigidbody2D enemyRigidBody;
    private Transform enemyTransform;
    private float enemySpeed;
    private AIDestinationSetter pathfinder;

    public ChaseHandler(Animator animator, Rigidbody2D rb, Transform transform, float speed, AIDestinationSetter _pathfinder) {
        enemyAnimator = animator;
        enemyRigidBody = rb;
        enemyTransform = transform;
        enemySpeed = speed;
        pathfinder = _pathfinder;
    }

    public void SetTarget(Transform target) {
        if(pathfinder.target == null) {
            pathfinder.target = target;
        }      
    }

}
