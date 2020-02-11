using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour {

	public GameObject playGameButton;
	public GameObject howToPlayButton;

	public Sprite playGameButtonHighlighted;
	public Sprite playGameButtonNormal;

	public Sprite howToPlayButtonHighlighted;
	public Sprite howToPlayButtonNormal;

	public bool isPlayGameHighlighted;

	// Use this for initialization
	void Start () {

		isPlayGameHighlighted = true;
		
	}
	
	// Update is called once per frame
	void Update () {


		if (isPlayGameHighlighted) {

			playGameButton.GetComponent<SpriteRenderer> ().sprite = playGameButtonHighlighted;
			howToPlayButton.GetComponent<SpriteRenderer> ().sprite = howToPlayButtonNormal;

			if (Input.GetKey (KeyCode.V) || Input.GetKey (KeyCode.B) || Input.GetKey (KeyCode.K) || Input.GetKey (KeyCode.L) || Input.GetKey (KeyCode.Return) || Input.GetKey (KeyCode.K) || Input.GetKey (KeyCode.L) || Input.GetButtonDown("X") || Input.GetButtonDown("A") || Input.GetButtonDown("X2") || Input.GetButtonDown("A2")) {
				SceneManager.LoadScene (1);
			}

			if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow) || Input.GetAxis("Left Analogic Horizontal") >= 0.5f) {
				isPlayGameHighlighted = false;
			}

		} else {

			playGameButton.GetComponent<SpriteRenderer> ().sprite = playGameButtonNormal;
			howToPlayButton.GetComponent<SpriteRenderer> ().sprite = howToPlayButtonHighlighted;

			if (Input.GetKey (KeyCode.V) || Input.GetKey (KeyCode.B) || Input.GetKey (KeyCode.K) || Input.GetKey (KeyCode.L) || Input.GetKey (KeyCode.Return) || Input.GetButtonDown("X") || Input.GetButtonDown("A") || Input.GetButtonDown("X2") || Input.GetButtonDown("A2")) {
				SceneManager.LoadScene (2);
			}

			if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow) || Input.GetAxis("Left Analogic Horizontal") <= -0.5f) {
				isPlayGameHighlighted = true;
			}

		}
		
	}
}
