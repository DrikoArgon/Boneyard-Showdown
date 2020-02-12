using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefKnife : MonoBehaviour {

	public float thiefDamage;
	public int direction;
	public float speed;

	public float timeToDestroy;

	// Use this for initialization
	void Start () {

		Destroy (gameObject, timeToDestroy);
	}

	// Update is called once per frame
	void FixedUpdate () {

		if (direction == 1) {
			GetComponent<Rigidbody2D> ().transform.position += Vector3.up * speed * Time.deltaTime;
		} else if (direction == 2) {
			GetComponent<Rigidbody2D> ().transform.position += Vector3.right * speed * Time.deltaTime;
		} else if (direction == 3) {
			GetComponent<Rigidbody2D> ().transform.position += Vector3.down * speed * Time.deltaTime;
		} else if (direction == 4) {
			GetComponent<Rigidbody2D> ().transform.position += Vector3.left * speed * Time.deltaTime;
		}
	}
		

	void OnTriggerEnter2D(Collider2D other){

		if (other.tag == "Player1" || other.tag == "Player2") {
			Player playerVariables = other.GetComponent<Player> ();
			if (!playerVariables.invulnerable) {
				playerVariables.currentLife -= thiefDamage;
				playerVariables.receivedDamage = true;
			}


			Destroy (gameObject);
		} 

		if (other.tag == "Scenario") {
			Destroy (gameObject);
		}



	}
}

