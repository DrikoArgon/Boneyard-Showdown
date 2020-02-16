﻿using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgressiveEnemy : Enemy
{
    public float detectionRange = 2;
    public float delayToAct;
    public float attackCooldown;

    protected GameObject player1;
    protected GameObject player2;

    protected Player player1Variables;
    protected Player player2Variables;

    protected AIDestinationSetter pathfinder;

    protected ChaseHandler chasehandler;
    protected WanderHandler wanderHandler;
    protected DetectionSystem detectionSystem;

    protected bool isAttackOnCooldown;
    protected float elapsedTime;

    protected override void InitializeEnemy() {
        base.InitializeEnemy();

        player1 = GameObject.Find("Player");
        player2 = GameObject.Find("Player2");

        pathfinder = GetComponent<AIDestinationSetter>();

        chasehandler = new ChaseHandler(animator, myRigidBody, transform, speed, pathfinder);
        wanderHandler = new WanderHandler(delayToAct);
        detectionSystem = new DetectionSystem(transform, player1.transform, player2.transform, detectionRange);

        player1Variables = player1.GetComponent<Player>();
        player2Variables = player2.GetComponent<Player>();
    }

    public virtual void StopAttacking() {

        attacking = false;

    }

    protected virtual void CheckAttackCooldown() {
        if (isAttackOnCooldown) {
            elapsedTime += Time.deltaTime;

            if (elapsedTime > attackCooldown) {
                elapsedTime = 0;
                isAttackOnCooldown = false;
            }
        }
    }

}
