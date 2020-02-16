using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandit : MeleeEnemy {

	public Transform boneSpawnPoint1;
	public Transform boneSpawnPoint2;
	public Transform boneSpawnPoint3;
	public Transform boneSpawnPoint4;
	public Transform boneSpawnPoint5;
	public Transform boneSpawnPoint6;
	public Transform boneSpawnPoint7;
	public Transform slashSpawnPointUp;
	public Transform slashSpawnPointRight;
	public Transform slashSpawnPointDown;
	public GameObject slashPrefab;

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
            enemyDirection = wanderHandler.RandomWander();
            chasehandler.StopChasing();
        } else {
            chasehandler.SetTarget(detectionSystem.GetCurrentTarget());
        }

        CheckInvinsibility();

        CheckDamageReceived();

        DefineDirectionToLook();

        CheckStatusForAnimation();
    }

    // Update is called once per frame
    void FixedUpdate () {

        if (dying) {
            return;
        }
        
        if (!detectionSystem.IsTargetDetected()) {

            //Random movement -------------------------------------------------

            //BasicMovement();
			//----------------------------------------------------------------------

		} else {
            
			if(!attacking){
				//Chasing Target --------------------------------------------------------

                /*enemyDirection = chasehandler.ChaseMelee(detectionSystem.GetCurrentTarget());*/

                walking = true;
                //------------------------------------------------------------------------	
            }
            
		}
        
    }


	void OnCollisionEnter2D(Collision2D other){

        if (!isAttackOnCooldown) {
            if (other.gameObject.tag == "Player1" && !player1Variables.dead) {
                attacking = true;

                SpawnSlash();

                animator.Play("Attack");
            }
            //
            if (other.gameObject.tag == "Player2" && !player2Variables.dead) {
                attacking = true;

                SpawnSlash();

                animator.Play("Attack");
            }
        }
		
//
	}

    void SpawnSlash() {

        switch (enemyDirection) {
            case EnemyDirection.Up:
                Instantiate(slashPrefab, slashSpawnPointUp.position, Quaternion.Euler(new Vector3(0, 0, 180)));
                break;
            case EnemyDirection.Right:
                Instantiate(slashPrefab, slashSpawnPointRight.position, Quaternion.Euler(new Vector3(0, 0, 90)));
                break;
            case EnemyDirection.Down:
                Instantiate(slashPrefab, slashSpawnPointDown.position, Quaternion.identity);
                break;
            case EnemyDirection.Left:
                Instantiate(slashPrefab, slashSpawnPointRight.position, Quaternion.Euler(new Vector3(0, 0, 270)));
                break;
            case EnemyDirection.None:
                break;
            default:
                Instantiate(slashPrefab, slashSpawnPointDown.position, Quaternion.identity);
                break;
        }

        isAttackOnCooldown = true;
    }

	void onCollisionStay(Collision2D other){

		if (!attacking) {
			if (other.gameObject.tag == "Player1" && !player1Variables.dead) {
				attacking = true;

                SpawnSlash();

                animator.Play("Attack");
			}
			//
			if (other.gameObject.tag == "Player2" && !player2Variables.dead) {
                attacking = true;

                SpawnSlash();

                animator.Play("Attack");
            }
		}

	}


}
