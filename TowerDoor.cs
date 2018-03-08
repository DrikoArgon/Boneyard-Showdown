using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDoor : MonoBehaviour {

	public Transform destinationSpawnPoint;
	public PlayerWhoOwnsTheCastle castleOwner;

	public enum PlayerWhoOwnsTheCastle
	{
		Player1,
		Player2
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){

		if (castleOwner == PlayerWhoOwnsTheCastle.Player1) {
			if (other.tag == "Player1") {
				other.transform.position = destinationSpawnPoint.position;
			}
		} else {
			if (other.tag == "Player2") {
				other.transform.position = destinationSpawnPoint.position;
			}
		}
			
	}
}
