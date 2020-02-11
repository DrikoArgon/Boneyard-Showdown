using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandit : Enemy {

	public float delayToAct;
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
	public Transform slashSpawnPointLeft;
	public GameObject slashPrefab;
	public GameObject player1;
	public GameObject player2;

	private float timeToAct;
	private int direction;

	private Player player1Variables;
	private Player player2Variables;

    private ChaseHandler chasehandler;
    private WanderHandler wanderHandler;
    private DetectionSystem detectionSystem;

	// Use this for initialization
	void Start () {

		animator = GetComponent<Animator> ();
		source = GetComponent<AudioSource> ();
        myRigidBody = GetComponent<Rigidbody2D>();
		receivedDamage = false;
		mySpriteRenderer = GetComponent<SpriteRenderer> ();

        player1 = GameObject.Find("Player");
        player2 = GameObject.Find("Player2");

        chasehandler = new ChaseHandler(animator, myRigidBody, transform, speed);
        wanderHandler = new WanderHandler(delayToAct);
        detectionSystem = new DetectionSystem(transform, player1.transform, player2.transform);

		player1Variables = player1.GetComponent<Player> ();
		player2Variables = player2.GetComponent<Player> ();

		dying = false;
		
	}

    private void Update() {

        if (dying) {
            return;
        }

        CheckDeath();


        // Finding Target Logic --------------------------------------------------------

        detectionSystem.HandlePlayerDetection();

        //--------------------------------------------------------------------------------

        CheckInvinsibility();

        CheckDamageReceived();

        DefineDirectionToLook();

        CheckStatusForAnimation();
    }

    // Update is called once per frame
    void FixedUpdate () {

		if (!detectionSystem.IsTargetDetected()) {

            //Random movement -------------------------------------------------
            enemyDirection = wanderHandler.RandomWander();

            BasicMovement();
			//----------------------------------------------------------------------

		} else {

			if(!attacking){
				//Chasing Target --------------------------------------------------------

                enemyDirection = chasehandler.ChaseMelee(detectionSystem.GetCurrentTarget());

                walking = true;
                //------------------------------------------------------------------------	
            }

		}

    }


	void OnCollisionEnter2D(Collision2D other){
	
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

	public void StopAttacking(){

		attacking = false;

	}

    void CheckStatusForAnimation() {
        if (!dying) {
            if (!attacking) {
                if (walking) {
                    animator.Play("Walk");
                } else {
                    animator.Play("Idle");
                }
            }
        }
    }

}
