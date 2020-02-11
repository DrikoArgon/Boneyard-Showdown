using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : Enemy {

	public float delayToAct;
	public Transform boneSpawnPoint1;
	public Transform boneSpawnPoint2;
	public Transform boneSpawnPoint3;
	public Transform boneSpawnPoint4;
	public Transform knifeSpawnPointUp;
	public Transform knifeSpawnPointRight;
	public Transform knifeSpawnPointDown;
	public Transform knifeSpawnPointLeft;
	public GameObject knifePrefab;
	public GameObject player1;
	public GameObject player2;

	private float timeToAct;
	private int direction;

	private Player player1Variables;
	private Player player2Variables;
	public bool targetSpotted;
	public PlayerSpotted playerSpotted;

	public float knifeThrowCooldown;

	private float knifeThrowTimestamp;

	public float distanceToPlayer1;
	public float distanceToPlayer2;

    private ChaseHandler chasehandler;
    private WanderHandler wanderHandler;
    private DetectionSystem detectionSystem;

    public enum PlayerSpotted{
		Player1,
		Player2,
		None
	}

	// Use this for initialization
	void Start () {

		targetSpotted = false;
		playerSpotted = PlayerSpotted.None;
		animator = GetComponent<Animator> ();
		source = GetComponent<AudioSource> ();
		receivedDamage = false;
		mySpriteRenderer = GetComponent<SpriteRenderer> ();

		player1 = GameObject.Find ("Player");
		player2 = GameObject.Find ("Player2");

        chasehandler = new ChaseHandler(animator, myRigidBody, transform, speed);
        wanderHandler = new WanderHandler(delayToAct);
        detectionSystem = new DetectionSystem(transform, player1.transform, player2.transform);

        player1Variables = player1.GetComponent<Player> ();
		player2Variables = player2.GetComponent<Player> ();

		dying = false;

		knifeThrowTimestamp = 0;
		timeToAct = Time.time + delayToAct;

	}

    private void Update() {

        if (dying) {
            return;
        }

        distanceToPlayer1 = Vector3.Distance(transform.position, player1.transform.position);
        distanceToPlayer2 = Vector3.Distance(transform.position, player2.transform.position);

        CheckDeath();

        detectionSystem.HandlePlayerDetection();


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

        //Shooting Knife Logic -----------------------------------------------------------
        if (targetSpotted){
			if (knifeThrowTimestamp < Time.time) {
				if (playerSpotted == PlayerSpotted.Player1) {
					if (Mathf.Abs (player1.transform.position.x - transform.position.x) < 0.2 && Mathf.Abs (player1.transform.position.y - transform.position.y) < 1.5) {
						attacking = true;
						animator.SetBool ("idle", false);
						animator.SetBool ("attacking", true);
						animator.SetBool ("walking", false);

						if (player1.transform.position.y > transform.position.y) {
							GameObject knife = (GameObject)Instantiate (knifePrefab, knifeSpawnPointUp.position, Quaternion.Euler (new Vector3 (0, 0, 180)));
							ThiefKnife knifeVariables = knife.GetComponent<ThiefKnife> ();
							knifeVariables.direction = 1;
						} else {
							GameObject knife = (GameObject)Instantiate (knifePrefab, knifeSpawnPointDown.position, Quaternion.identity);
							ThiefKnife knifeVariables = knife.GetComponent<ThiefKnife> ();
							knifeVariables.direction = 3;
						}

						knifeThrowTimestamp = Time.time + knifeThrowCooldown;

					}
					if (Mathf.Abs (player1.transform.position.x - transform.position.x) < 1.5 && Mathf.Abs (player1.transform.position.y - transform.position.y) < 0.2) {
						attacking = true;
						animator.SetBool ("idle", false);
						animator.SetBool ("attacking", true);
						animator.SetBool ("walking", false);


						if (player1.transform.position.x > transform.position.x) {
							GameObject knife = (GameObject)Instantiate (knifePrefab, knifeSpawnPointRight.position, Quaternion.Euler (new Vector3 (0, 0, 90)));
							ThiefKnife knifeVariables = knife.GetComponent<ThiefKnife> ();
							knifeVariables.direction = 2;
						} else {
							GameObject knife = (GameObject)Instantiate (knifePrefab, knifeSpawnPointLeft.position, Quaternion.Euler (new Vector3 (0, 0, 270)));
							ThiefKnife knifeVariables = knife.GetComponent<ThiefKnife> ();
							knifeVariables.direction = 4;
						}
						knifeThrowTimestamp = Time.time + knifeThrowCooldown;
					}
				} else {
					if (Mathf.Abs (player2.transform.position.x - transform.position.x) < 0.2 && Mathf.Abs (player2.transform.position.y - transform.position.y) < 1.5) {
						attacking = true;
						animator.SetBool ("idle", false);
						animator.SetBool ("attacking", true);
						animator.SetBool ("walking", false);

						if (player2.transform.position.y > transform.position.y) {
							GameObject knife = (GameObject)Instantiate (knifePrefab, knifeSpawnPointUp.position, Quaternion.Euler (new Vector3 (0, 0, 180)));
							ThiefKnife knifeVariables = knife.GetComponent<ThiefKnife> ();
							knifeVariables.direction = 1;
						} else {
							GameObject knife = (GameObject)Instantiate (knifePrefab, knifeSpawnPointDown.position, Quaternion.identity);
							ThiefKnife knifeVariables = knife.GetComponent<ThiefKnife> ();
							knifeVariables.direction = 3;
						}

						knifeThrowTimestamp = Time.time + knifeThrowCooldown;

					}
					if (Mathf.Abs (player2.transform.position.x - transform.position.x) < 1.5 && Mathf.Abs (player2.transform.position.y - transform.position.y) < 0.2) {
						attacking = true;
						animator.SetBool ("idle", false);
						animator.SetBool ("attacking", true);
						animator.SetBool ("walking", false);


						if (player2.transform.position.x > transform.position.x) {
							GameObject knife = (GameObject)Instantiate (knifePrefab, knifeSpawnPointRight.position, Quaternion.Euler (new Vector3 (0, 0, 90)));
							ThiefKnife knifeVariables = knife.GetComponent<ThiefKnife> ();
							knifeVariables.direction = 2;
						} else {
							GameObject knife = (GameObject)Instantiate (knifePrefab, knifeSpawnPointLeft.position, Quaternion.Euler (new Vector3 (0, 0, 270)));
							ThiefKnife knifeVariables = knife.GetComponent<ThiefKnife> ();
							knifeVariables.direction = 4;
						}
						knifeThrowTimestamp = Time.time + knifeThrowCooldown;
					}


				}
			}
		}
		//---------------------------------------------------------------------------------

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

    public void StopAttacking() {

        attacking = false;


    }

}

