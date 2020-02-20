using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleDoorAnimationManager : MonoBehaviour {


	public PlayerWhoOwnsTheCastle player;

	private Animator doorAnimator;

	public enum PlayerWhoOwnsTheCastle
	{
		Player1,
		Player2
	}

	// Use this for initialization
	void Start () {

		doorAnimator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){

		if (player == PlayerWhoOwnsTheCastle.Player1) {
			if (other.tag == "Player1") {
                doorAnimator.Play("Open");
			}
		} else {
			if (other.tag == "Player2") {
                doorAnimator.Play("Open");
            }
		}



	}

	void OnTriggerExit2D(Collider2D other){

		if (player == PlayerWhoOwnsTheCastle.Player1) {
			if (other.tag == "Player1") {
                doorAnimator.Play("Close");
            }
		} else {
			if (other.tag == "Player2") {
                doorAnimator.Play("Close");
            }
		}
	}

}
