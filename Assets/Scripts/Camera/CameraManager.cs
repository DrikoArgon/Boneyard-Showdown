using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    public float distanceBetweenPlayersToActivate = 3.2f;
    public float distanceBetweenPlayersToDeactivate = 6f;
    public GameObject player1Camera;
	public GameObject player2Camera;
	public GameObject mergedCamera;


    private Transform player1;
    private Transform player2;

	private bool mergedCameraActivated;
	private bool player1AlreadyCrossedPlayer2;

    private float currentDistanceBetweenPlayers;

    private void Awake() {
        player1 = GameObject.Find("Player").transform;
        player2 = GameObject.Find("Player2").transform;
    }

    // Use this for initialization
    void Start () {
		mergedCameraActivated = false;
		player1AlreadyCrossedPlayer2 = false;
	}
	
	// Update is called once per frame
	void Update () {

        currentDistanceBetweenPlayers = Mathf.Abs(player1.position.x - player2.position.x);

        /*if (player1RightCameraLimit.transform.position.x >= player2LeftCameraLimit.transform.position.x) {
			
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
        */

        if (!mergedCameraActivated) {
            if(currentDistanceBetweenPlayers <= distanceBetweenPlayersToActivate) {
                player1Camera.SetActive(false);
                player2Camera.SetActive(false);
                mergedCamera.SetActive(true);
                mergedCameraActivated = true;
            }
        } else {
            if (currentDistanceBetweenPlayers > distanceBetweenPlayersToDeactivate) {
                player1Camera.SetActive(true);
                player2Camera.SetActive(true);
                mergedCamera.SetActive(false);
                mergedCameraActivated = false;
            }
        }

		
	}
}
