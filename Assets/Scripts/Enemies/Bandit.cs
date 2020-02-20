using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandit : MeleeEnemy {


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

        // Finding Target Logic --------------------------------------------------------

        detectionSystem.HandlePlayerDetection();

        //--------------------------------------------------------------------------------

        if (!detectionSystem.IsTargetDetected()) {
            wanderHandler.RandomWander(timeBetweenWanderMovements, randomWanderRadius);
        } else {
            chasehandler.SetTarget(detectionSystem.GetCurrentTarget());
        }

        CheckInvinsibility();

        CheckDamageReceived();

        DefineAnimationDirection();

        CheckStatusForAnimation();
    }

    // Update is called once per frame
    void FixedUpdate () {

        
    }


	void OnCollisionEnter2D(Collision2D other){

        if (!isAttackOnCooldown && !attacking) {
            if (other.gameObject.tag == "Player1" && !player1Variables.dead) {
                attacking = true;
                aiPath.canMove = false;
                isAttackOnCooldown = true;

                animator.Play("Attack");
            }
            //
            if (other.gameObject.tag == "Player2" && !player2Variables.dead) {
                attacking = true;
                aiPath.canMove = false;
                isAttackOnCooldown = true;

                animator.Play("Attack");
            }


        }
		
//
	}


	void onCollisionStay(Collision2D other){

        if (!isAttackOnCooldown && !attacking) {
            if (other.gameObject.tag == "Player1" && !player1Variables.dead) {
                attacking = true;
                aiPath.canMove = false;
                isAttackOnCooldown = true;

                animator.Play("Attack");
            }
            //
            if (other.gameObject.tag == "Player2" && !player2Variables.dead) {
                attacking = true;
                aiPath.canMove = false;
                isAttackOnCooldown = true;

                animator.Play("Attack");
            }


        }

    }


}
