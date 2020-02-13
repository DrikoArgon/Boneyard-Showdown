using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    public static CameraManager instance;

    public float distanceBetweenPlayersToActivate = 3.2f;
    public float distanceBetweenPlayersToDeactivate = 6f;
    public GameObject player1Camera;
	public GameObject player2Camera;
	public GameObject mergedCamera;

    private CameraShake player1CameraShaker;
    private CameraShake player2CameraShaker;
    private CameraShake mergedCameraShaker;

    private CinemachineConfiner player1CameraConfiner;
    private CinemachineConfiner player2CameraConfiner;
    private CinemachineConfiner mergedCameraConfiner;

    private Transform player1;
    private Transform player2;

	private bool mergedCameraActivated;
	private bool player1AlreadyCrossedPlayer2;

    private float currentDistanceBetweenPlayers;

    private void Awake() {


        if(CameraManager.instance == null) {
            instance = this;
            player1 = GameObject.Find("Player").transform;
            player2 = GameObject.Find("Player2").transform;

            player1CameraShaker = player1Camera.GetComponent<CameraShake>();
            player2CameraShaker = player2Camera.GetComponent<CameraShake>();
            mergedCameraShaker = mergedCamera.GetComponent<CameraShake>();

            player1CameraConfiner = player1Camera.GetComponentInChildren<CinemachineConfiner>();
            player2CameraConfiner = player2Camera.GetComponentInChildren<CinemachineConfiner>();
            mergedCameraConfiner = mergedCamera.GetComponentInChildren<CinemachineConfiner>();

        } else {
            Destroy(this);
        }
        

    }

    // Use this for initialization
    void Start () {
		mergedCameraActivated = false;
		player1AlreadyCrossedPlayer2 = false;
	}
	
	// Update is called once per frame
	void Update () {

        currentDistanceBetweenPlayers = Mathf.Abs(player1.position.x - player2.position.x);

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

    public void UpdateCameraConfiner(PolygonCollider2D confiner, bool isPlayer1) {

        if (mergedCameraActivated) {
            mergedCameraConfiner.m_BoundingShape2D = confiner;
            player1CameraConfiner.m_BoundingShape2D = confiner;
            player2CameraConfiner.m_BoundingShape2D = confiner;
        } else {
            if (isPlayer1) {
                player1CameraConfiner.m_BoundingShape2D = confiner;
            } else {
                player2CameraConfiner.m_BoundingShape2D = confiner;
            }

        }
    }


    public void ShakePlayerCamera(float duration, float amplitude, float frequency, bool isPlayer1) {
        if (mergedCameraActivated) {
            ShakeMergedCamera(duration, amplitude, frequency);
        } else {
            if (isPlayer1) {
                player1CameraShaker.ShakeCameraCinemachine(duration, amplitude, frequency);
            } else {
                player2CameraShaker.ShakeCameraCinemachine(duration, amplitude, frequency);
            }
            
        }
    }

    void ShakeMergedCamera(float duration, float amplitude, float frequency) {
        mergedCameraShaker.ShakeCameraCinemachine(duration, amplitude, frequency);
    }

}
