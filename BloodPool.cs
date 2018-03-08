using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodPool : MonoBehaviour {


	private bool insideCircle;
	private Player playerInsideCircle;

	private float recoverCooldown;
	private float recoverTimeStamp;


	// Use this for initialization
	void Start () {
		insideCircle = false;
		recoverCooldown = 1;
		recoverTimeStamp = 0;
	}

	// Update is called once per frame
	void Update () {

		if (recoverTimeStamp < Time.time) {
			if (insideCircle) {
				
				if (!playerInsideCircle.dead && (playerInsideCircle.currentLife < playerInsideCircle.maxLife)) {

					playerInsideCircle.currentLife += 2;

					if (playerInsideCircle.currentLife > playerInsideCircle.maxLife) {
						playerInsideCircle.currentLife = playerInsideCircle.maxLife;
					}

					recoverTimeStamp = Time.time + recoverCooldown;

				}
			}
		}

	}
	void OnTriggerEnter2D(Collider2D other){

		if (other.tag == "Player1" || other.tag == "Player2") {
			
			playerInsideCircle = other.GetComponent<Player> ();

			insideCircle = true;

			if (playerInsideCircle.dead) {
				playerInsideCircle.Reborn ();
			}

		}

	}

	void OnTriggerExit2D(Collider2D other){

		if (other.tag == "Player1" || other.tag == "Player2") {

			insideCircle = false;
		}

	}
}
