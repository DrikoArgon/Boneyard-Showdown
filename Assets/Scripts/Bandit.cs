using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandit : Enemy {

	public float delayToAct;
	public GameObject mySpawner;
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
	public AudioClip deathSound;

	private Animator animator;
	private AudioSource source;
	private float timeToAct;
	private int direction;

	private Player player1Variables;
	private Player player2Variables;
	private bool walking;
	public bool targetSpotted;
	public PlayerSpotted playerSpotted;

	public float distanceToPlayer1;
	public float distanceToPlayer2;

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

		player1Variables = player1.GetComponent<Player> ();
		player2Variables = player2.GetComponent<Player> ();

		dying = false;


		timeToAct = Time.time + delayToAct;
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (life <= 0 && !dying) {

			source.PlayOneShot (deathSound, 1.0f);
			animator.SetBool ("dead", true);
			dying = true;
			Destroy (GetComponent<Rigidbody2D> ());
			Destroy (GetComponent<BoxCollider2D> ());

		}

		distanceToPlayer1 = Vector3.Distance (transform.position, player1.transform.position);
		distanceToPlayer2 = Vector3.Distance (transform.position, player2.transform.position);

		if (!targetSpotted) {

			//Random movement -------------------------------------------------
			if (timeToAct < Time.time) {

				timeToAct = Time.time + delayToAct;


				int randomNumber = Random.Range (1, 40);

				if (randomNumber <= 5) {
					
					animator.SetBool ("walking", true);
					animator.SetInteger ("direction", 1);
					animator.SetBool ("idle", false);
					direction = 1;

				} else if (randomNumber > 5 && randomNumber <= 10) {
					
					animator.SetBool ("walking", true);
					animator.SetInteger ("direction", 2);
					animator.SetBool ("idle", false);
					direction = 2;

				} else if (randomNumber > 10 && randomNumber <= 15) {
					
					animator.SetBool ("walking", true);
					animator.SetInteger ("direction", 3);
					animator.SetBool ("idle", false);
					direction = 3;

				} else if (randomNumber > 15 && randomNumber <= 20) {
					
					animator.SetBool ("walking", true);
					animator.SetInteger ("direction", 4);
					animator.SetBool ("idle", false);
					direction = 4;

				} else {
					
					animator.SetBool ("walking", false);
					animator.SetBool ("idle", true);
					direction = 5;
				}

			}


			if (direction == 1) {
				GetComponent<Rigidbody2D> ().transform.position += Vector3.up * speed * Time.deltaTime;
			} else if (direction == 2) {
				GetComponent<Rigidbody2D> ().transform.position += Vector3.right * speed * Time.deltaTime;
			} else if (direction == 3) {
				GetComponent<Rigidbody2D> ().transform.position += Vector3.down * speed * Time.deltaTime;
			} else if (direction == 4) {
				GetComponent<Rigidbody2D> ().transform.position += Vector3.left * speed * Time.deltaTime;
			}
			//----------------------------------------------------------------------

		} else {

			if(!attacking){
				//Chasing Target --------------------------------------------------------
				if (playerSpotted == PlayerSpotted.Player1) {

					//Chase to the right
					if (player1.transform.position.x > transform.position.x) {

						//If close enough, chase on the y axis
						if (Mathf.Abs (player1.transform.position.x - transform.position.x) < 0.1) {
							if (player1.transform.position.y > transform.position.y) {

								direction = 1;
								animator.SetBool ("walking", true);
								animator.SetInteger ("direction", 1);
								GetComponent<Rigidbody2D> ().transform.position += Vector3.up * speed * Time.deltaTime;

							} else if (player1.transform.position.y < transform.position.y) {
								direction = 3;
								animator.SetBool ("walking", true);
								animator.SetInteger ("direction", 3);
								GetComponent<Rigidbody2D> ().transform.position += Vector3.down * speed * Time.deltaTime;
							}

						} else {

							direction = 2;
							animator.SetBool ("walking", true);
							animator.SetInteger ("direction", 2);
							GetComponent<Rigidbody2D> ().transform.position += Vector3.right * speed * Time.deltaTime;
						}

					}  

					if (player1.transform.position.x < transform.position.x) {

						if (Mathf.Abs (player1.transform.position.x - transform.position.x) < 0.1) {
							if (player1.transform.position.y > transform.position.y) {
								direction = 1;
								animator.SetBool ("walking", true);
								animator.SetInteger ("direction", 1);
								GetComponent<Rigidbody2D> ().transform.position += Vector3.up * speed * Time.deltaTime;
							} else if (player1.transform.position.y < transform.position.y) {
								direction = 3;
								animator.SetBool ("walking", true);
								animator.SetInteger ("direction", 3);
								GetComponent<Rigidbody2D> ().transform.position += Vector3.down * speed * Time.deltaTime;
							}
						} else {


							direction = 4;
							animator.SetBool ("walking", true);
							animator.SetInteger ("direction", 4);
							GetComponent<Rigidbody2D> ().transform.position += Vector3.left * speed * Time.deltaTime;
						}

					}

				} else if (playerSpotted == PlayerSpotted.Player2) {

					if (player2.transform.position.x > transform.position.x) {

						if (Mathf.Abs (player2.transform.position.x - transform.position.x) < 0.1) {
							if (player2.transform.position.y > transform.position.y) {

								direction = 1;
								animator.SetBool ("walking", true);
								animator.SetInteger ("direction", 1);
								GetComponent<Rigidbody2D> ().transform.position += Vector3.up * speed * Time.deltaTime;

							} else if (player2.transform.position.y < transform.position.y) {
								direction = 3;
								animator.SetBool ("walking", true);
								animator.SetInteger ("direction", 3);
								GetComponent<Rigidbody2D> ().transform.position += Vector3.down * speed * Time.deltaTime;
							} 

						} else {

							direction = 2;
							animator.SetBool ("walking", true);
							animator.SetInteger ("direction", 2);
							GetComponent<Rigidbody2D> ().transform.position += Vector3.right * speed * Time.deltaTime;
						}


					} if (player2.transform.position.x < transform.position.x) {

						if (Mathf.Abs (player2.transform.position.x - transform.position.x) < 0.1) {
							if (player2.transform.position.y > transform.position.y) {
								direction = 1;
								animator.SetBool ("walking", true);
								animator.SetInteger ("direction", 1);
								GetComponent<Rigidbody2D> ().transform.position += Vector3.up * speed * Time.deltaTime;
							} else if (player2.transform.position.y < transform.position.y) {
								direction = 3;
								animator.SetBool ("walking", true);
								animator.SetInteger ("direction", 3);
								GetComponent<Rigidbody2D> ().transform.position += Vector3.down * speed * Time.deltaTime;
							}
						} else {


							direction = 4;
							animator.SetBool ("walking", true);
							animator.SetInteger ("direction", 4);
							GetComponent<Rigidbody2D> ().transform.position += Vector3.left * speed * Time.deltaTime;
						}
					}
				}
				//------------------------------------------------------------------------	
			}

		}
			
		// Finding Target Logic --------------------------------------------------------

		if ((Vector3.Distance ( transform.position, player1.transform.position) <= 2 || Vector3.Distance ( transform.position, player2.transform.position) <= 2 ) && !targetSpotted) {
			animator.SetBool ("idle", false);
			targetSpotted = true;
			if (Vector3.Distance (transform.position, player1.transform.position) <= 2) {
				playerSpotted = PlayerSpotted.Player1;
			} else {
				playerSpotted = PlayerSpotted.Player2;
			}

		}  

		if((Vector3.Distance ( transform.position, player1.transform.position) > 2 && Vector3.Distance ( transform.position, player2.transform.position) > 2 ) && targetSpotted){
			targetSpotted = false;
			playerSpotted = PlayerSpotted.None;
		}
			
		//--------------------------------------------------------------------------------

		if (invulnerableTimeStamp < Time.time) {
			invulnerable = false;
			mySpriteRenderer.enabled = true;
		}

		if (invulnerable) {
			Flash ();
			lifeWhileInvulnerable = life;
		}

		if (receivedDamage && life > 0) {
			ToggleInvinsibility ();
		}
	}

	void OnCollisionEnter2D(Collision2D other){

//		if (direction == 1) {
//			attacking = true;
//			animator.SetBool ("attacking", true);
//			
//		} else if (direction == 2) {
//			
//		} else if (direction == 3) {
//
//		} else {
//
//		}
	
		if (other.gameObject.tag == "Player1" && playerSpotted == PlayerSpotted.Player1 && !player1Variables.dead) {
			attacking = true;
			if (direction == 1) {
				Instantiate (slashPrefab, slashSpawnPointUp.position, Quaternion.Euler( new Vector3(0, 0, 180)));
			} else if (direction == 2) {
				Instantiate (slashPrefab, slashSpawnPointRight.position, Quaternion.Euler (new Vector3(0, 0, 90)));
			} else if (direction == 3) {
				Instantiate (slashPrefab, slashSpawnPointDown.position, Quaternion.identity);
			} else {
				Instantiate (slashPrefab, slashSpawnPointLeft.position, Quaternion.Euler (new Vector3(0, 0, 270)));
			}

			animator.SetBool ("attacking", true);
			animator.SetBool ("walking", false);
		}
//
		if (other.gameObject.tag == "Player2" && playerSpotted == PlayerSpotted.Player2 && !player2Variables.dead) {
			attacking = true;
			animator.SetBool ("attacking", true);
			animator.SetBool ("walking", false);

			if (direction == 1) {
				Instantiate (slashPrefab, slashSpawnPointUp.position, Quaternion.Euler (0, 0, 180));
			} else if (direction == 2) {
				Instantiate (slashPrefab, slashSpawnPointRight.position, Quaternion.Euler (0, 0, 90));
			} else if (direction == 3) {
				Instantiate (slashPrefab, slashSpawnPointDown.position, Quaternion.identity);
			} else {
				Instantiate (slashPrefab, slashSpawnPointLeft.position, Quaternion.Euler (0, 0, 270));
			}
		}
//
	}

	void onCollisionStay(Collision2D other){

		if (!attacking) {
			if (other.gameObject.tag == "Player1" && playerSpotted == PlayerSpotted.Player1 && !player1Variables.dead) {
				attacking = true;
				if (direction == 1) {
					Instantiate (slashPrefab, slashSpawnPointUp.position, Quaternion.Euler( new Vector3(0, 0, 180)));
				} else if (direction == 2) {
					Instantiate (slashPrefab, slashSpawnPointRight.position, Quaternion.Euler (new Vector3(0, 0, 90)));
				} else if (direction == 3) {
					Instantiate (slashPrefab, slashSpawnPointDown.position, Quaternion.identity);
				} else {
					Instantiate (slashPrefab, slashSpawnPointLeft.position, Quaternion.Euler (new Vector3(0, 0, 270)));
				}

				animator.SetBool ("attacking", true);
				animator.SetBool ("walking", false);
			}
			//
			if (other.gameObject.tag == "Player2" && playerSpotted == PlayerSpotted.Player2 && !player2Variables.dead) {
				attacking = true;
				animator.SetBool ("attacking", true);
				animator.SetBool ("walking", false);

				if (direction == 1) {
					Instantiate (slashPrefab, slashSpawnPointUp.position, Quaternion.Euler (0, 0, 180));
				} else if (direction == 2) {
					Instantiate (slashPrefab, slashSpawnPointRight.position, Quaternion.Euler (0, 0, 90));
				} else if (direction == 3) {
					Instantiate (slashPrefab, slashSpawnPointDown.position, Quaternion.identity);
				} else {
					Instantiate (slashPrefab, slashSpawnPointLeft.position, Quaternion.Euler (0, 0, 270));
				}
			}
		}

	}

	public void StopAttacking(){


		animator.SetBool ("walking", true);
		animator.SetBool ("attacking", false);
		attacking = false;


	}
//
	override public void Disappear(){

		GetComponent<SpriteRenderer> ().enabled = false;

		StartCoroutine (SpawnBones ());

		StartCoroutine (ResetSpawner ());
	}

	IEnumerator SpawnBones(){

		for (int i = 0; i < numberOfBones; i++) {
			Instantiate (collectibleBonePrefab, transform.position, Quaternion.identity);
			yield return new WaitForSeconds(0.1f);

		}
	}

	IEnumerator ResetSpawner(){
		
		float timeToWait = 0.1f * numberOfBones + 0.2f;
		yield return new WaitForSeconds(timeToWait);
		mySpawner.GetComponent<EnemySpawnPoint>().ResetSpawn ();
		Destroy (gameObject);
	}
}
