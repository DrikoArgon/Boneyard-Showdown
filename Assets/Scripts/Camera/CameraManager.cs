using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

	public Camera player1Camera;
	public Camera player2Camera;
	public Camera mergedCamera;
	public GameObject player1RightCameraLimit;
	public GameObject player2RightCameraLimit;
	public GameObject player1LeftCameraLimit;
	public GameObject player2LeftCameraLimit;

	private bool mergedCameraActivated;
	private bool player1AlreadyCrossedPlayer2;

	// Use this for initialization
	void Start () {
		mergedCameraActivated = false;
		player1AlreadyCrossedPlayer2 = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (player1RightCameraLimit.transform.position.x >= player2LeftCameraLimit.transform.position.x) {
			
			if (!mergedCameraActivated && !player1AlreadyCrossedPlayer2) {
				
				//mergedCamera.transform.position = new Vector3 (player1RightCameraLimit.transform.position.x,mergedCamera.transform.position.y , mergedCamera.transform.position.z);
				player1Camera.enabled = false;
				player2Camera.enabled = false;
				mergedCamera.enabled = true;
				mergedCameraActivated = true;
			}
		} else {
			if (mergedCameraActivated) {
				
				mergedCameraActivated = false;
				player1Camera.enabled = true;
				player2Camera.enabled = true;
				mergedCamera.enabled = false;
				player1AlreadyCrossedPlayer2 = false;

			}
		}

		if (player1LeftCameraLimit.transform.position.x >= player2RightCameraLimit.transform.position.x) {
			if (mergedCameraActivated) {

				mergedCameraActivated = false;
				player1Camera.enabled = true;
				player2Camera.enabled = true;
				mergedCamera.enabled = false;
				player1AlreadyCrossedPlayer2 = true;

			}
		} else {
			if (!mergedCameraActivated && player1AlreadyCrossedPlayer2) {

				//mergedCamera.transform.position = new Vector3 (player2RightCameraLimit.transform.position.x,mergedCamera.transform.position.y, mergedCamera.transform.position.z);
				player1Camera.enabled = false;
				player2Camera.enabled = false;
				mergedCamera.enabled = true;
				mergedCameraActivated = true;

			} 

		}


		
	}
}
