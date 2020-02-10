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
				doorAnimator.SetBool("opening", true);
				doorAnimator.SetBool ("closed", false);
				doorAnimator.SetBool("opened", false);
				doorAnimator.SetBool ("closing", false);
			}
		} else {
			if (other.tag == "Player2") {
				doorAnimator.SetBool ("opening", true);
				doorAnimator.SetBool ("closed", false);
				doorAnimator.SetBool("opened", false);
				doorAnimator.SetBool ("closing", false);
			}
		}



	}

	void OnTriggerExit2D(Collider2D other){

		if (player == PlayerWhoOwnsTheCastle.Player1) {
			if (other.tag == "Player1") {
				doorAnimator.SetBool("closing", true);
				doorAnimator.SetBool ("opened", false);
				doorAnimator.SetBool("closed", false);
				doorAnimator.SetBool ("opening", false);
			}
		} else {
			if (other.tag == "Player2") {
				doorAnimator.SetBool ("closing", true);
				doorAnimator.SetBool ("opened", false);
				doorAnimator.SetBool("closed", false);
				doorAnimator.SetBool ("opening", false);
			}
		}
	}

	public void FinishedOpening(){
		doorAnimator.SetBool ("opened", true);
		doorAnimator.SetBool("opening", false);
		doorAnimator.SetBool("closed", false);
		doorAnimator.SetBool ("closing", false);
	}

	public void FinishedClosing(){
		doorAnimator.SetBool ("closed", true);
		doorAnimator.SetBool ("closing", false);
		doorAnimator.SetBool("opened", false);
		doorAnimator.SetBool ("opening", false);
	}
}
