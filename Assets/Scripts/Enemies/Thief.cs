using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : RangedEnemy {

	public Transform knifeSpawnPointUp;
	public Transform knifeSpawnPointRight;
	public Transform knifeSpawnPointDown;

	public bool targetSpotted;

    private void Awake() {
        InitializeEnemy();

    }

    // Use this for initialization
    void Start () {

	}

    private void Update() {

        CheckDeath();

        if (dying) {
            return;
        }

        CheckAttackCooldown();

        detectionSystem.HandlePlayerDetection();

        if (!detectionSystem.IsTargetDetected()) {
            wanderHandler.RandomWander(timeBetweenWanderMovements, randomWanderRadius);
        } else {

            chasehandler.SetTarget(detectionSystem.GetCurrentTarget());

            if (!attacking) {

                //Shooting Knife Logic ---------------------------------------------------
                if (!isAttackOnCooldown) {
                    FireProjectile(detectionSystem.GetCurrentTarget());
                }
                

            }
        }

        CheckInvinsibility();

        CheckDamageReceived();

        DefineAnimationDirection();

        CheckStatusForAnimation();

    }



    // Update is called once per frame
    void FixedUpdate () {

	}

}

