using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergedCameraController : MonoBehaviour {

	private GameObject player1;
	private GameObject player2;

	// Use this for initialization
	void Start () {
		player1 = GameObject.Find ("Player");
		player2 = GameObject.Find ("Player2");

	}
	
	// Update is called once per frame
	void Update () {

		Vector3 midPoint = new Vector3 (player2.transform.position.x - (player2.transform.position.x - player1.transform.position.x) / 2, 0, 1);

		transform.position = midPoint;


	}
}
