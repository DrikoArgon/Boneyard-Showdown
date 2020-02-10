using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : Enemy {

	public float delayToAct;
	public GameObject mySpawner;
	public AudioClip dieSound;

	private Animator animator;
	private AudioSource source;
	private float timeToAct;
	private int direction;


	// Use this for initialization
	void Start () {

		source = GetComponent<AudioSource> ();
		animator = GetComponent<Animator> ();
		receivedDamage = false;
		mySpriteRenderer = GetComponent<SpriteRenderer> ();
		dying = false;

		int randomNumber = Random.Range (1, 6);

		if (randomNumber > 3) {
			animator.SetBool ("facingRight", true);
		} else {
			animator.SetBool ("facingRight", false); 
		}

		timeToAct = Time.time + delayToAct;
	}
	
	// Update is called once per frame
	void Update () {

		//Chicken dying
		if (life <= 0 && !dying) {

			source.PlayOneShot (dieSound, 1.0f);
			animator.SetBool ("dead", true);
			dying = true;
			Destroy (GetComponent<Rigidbody2D> ());
			Destroy (GetComponent<CircleCollider2D> ());


		}

		//Chicken walking logic

		if (timeToAct < Time.time) {

			invulnerable = false;

			timeToAct = Time.time + delayToAct;


			int randomNumber = Random.Range (1, 30);

			if (randomNumber <= 5) {
				direction = 1;
			} else if (randomNumber > 5 && randomNumber <= 10) {
				animator.SetBool ("facingRight", true);
				direction = 2;
			} else if (randomNumber > 10 && randomNumber <= 15) {
				direction = 3;
				animator.SetBool ("facingRight", false);
			} else if (randomNumber > 15 && randomNumber <= 20) {
				direction = 4;
			} else {
				direction = 5;
			}

		}


		if (direction == 1) {
			GetComponent<Rigidbody2D> ().transform.position += Vector3.up * speed * Time.deltaTime;
		} else if (direction == 2) {
			animator.SetBool ("facingRight", true);
			GetComponent<Rigidbody2D> ().transform.position += Vector3.right * speed * Time.deltaTime;
		} else if (direction == 3) {
			animator.SetBool ("facingRight", false);
			GetComponent<Rigidbody2D> ().transform.position += Vector3.left * speed * Time.deltaTime;
		} else if (direction == 4) {
			GetComponent<Rigidbody2D> ().transform.position += Vector3.down * speed * Time.deltaTime;
		}


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


	override public void Disappear(){
		for (int i = 0; i < numberOfBones; i++) {
			Instantiate (collectibleBonePrefab, transform.position, Quaternion.identity);
		}
		mySpawner.GetComponent<EnemySpawnPoint>().ResetSpawn ();
		Destroy (gameObject);
	}
}
