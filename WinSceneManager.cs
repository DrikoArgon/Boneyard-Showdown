using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinSceneManager : MonoBehaviour {

	public GameObject playAgainButton;
	public GameObject BackToTitleButton;

	public Sprite playAgainButtonHighlighted;
	public Sprite playAgainButtonNormal;

	public Sprite BackToTitleButtonHighlighted;
	public Sprite BackToTitleButtonNormal;

	public bool isPlayAgainHighlighted;

	// Use this for initialization
	void Start () {

		isPlayAgainHighlighted = true;

	}

	// Update is called once per frame
	void Update () {


		if (isPlayAgainHighlighted) {

			playAgainButton.GetComponent<SpriteRenderer> ().sprite = playAgainButtonHighlighted;
			BackToTitleButton.GetComponent<SpriteRenderer> ().sprite = BackToTitleButtonNormal;

			if (Input.GetKey (KeyCode.V) || Input.GetKey (KeyCode.B) || Input.GetKey (KeyCode.K) || Input.GetKey (KeyCode.L) || Input.GetKey (KeyCode.Return) || Input.GetKey (KeyCode.K) || Input.GetKey (KeyCode.L) || Input.GetButtonDown("X") || Input.GetButtonDown("A") || Input.GetButtonDown("X2") || Input.GetButtonDown("A2")) {
				SceneManager.LoadScene (1);
			}

			if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) {
				isPlayAgainHighlighted = false;
			}

		} else {

			playAgainButton.GetComponent<SpriteRenderer> ().sprite = playAgainButtonNormal;
			BackToTitleButton.GetComponent<SpriteRenderer> ().sprite = BackToTitleButtonHighlighted;

			if (Input.GetKey (KeyCode.V) || Input.GetKey (KeyCode.B) || Input.GetKey (KeyCode.K) || Input.GetKey (KeyCode.L) || Input.GetKey (KeyCode.Return) || Input.GetKey (KeyCode.K) || Input.GetKey (KeyCode.L) || Input.GetButtonDown("X") || Input.GetButtonDown("A") || Input.GetButtonDown("X2") || Input.GetButtonDown("A2")) {
				SceneManager.LoadScene (0);
			}

			if (Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow)) {
				isPlayAgainHighlighted = true;
			}

		}

	}

}
