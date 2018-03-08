using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditSlash : MonoBehaviour {

	public float banditDamage;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Disappear(){
		Destroy (gameObject);
	}

	void OnTriggerEnter2D(Collider2D other){

		if (other.tag == "Player1" || other.tag == "Player2") {
			Player playerVariables = other.GetComponent<Player> ();
			if (!playerVariables.invulnerable) {
				playerVariables.currentLife -= banditDamage;
				playerVariables.receivedDamage = true;
			}
				
		}



	}
}
