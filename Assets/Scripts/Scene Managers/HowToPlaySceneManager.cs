using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToPlaySceneManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.V) || Input.GetKey (KeyCode.B) || Input.GetKey (KeyCode.K) || Input.GetKey (KeyCode.L) || Input.GetKey (KeyCode.Return) || Input.GetKey (KeyCode.K) || Input.GetKey (KeyCode.L) || Input.GetKey (KeyCode.Return) || Input.GetButtonDown("X") || Input.GetButtonDown("A") || Input.GetButtonDown("X2") || Input.GetButtonDown("A2")) {

			SceneManager.LoadScene (0);

		}
		
	}
}
