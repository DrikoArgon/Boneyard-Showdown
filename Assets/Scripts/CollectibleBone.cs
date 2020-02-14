using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleBone : MonoBehaviour {


	private CollectibleAudioManager audioManager;
	private Vector3 destinationPoint;
	private float hoverSpeed = 2;
	private float step;

	// Use this for initialization
	void Awake(){

		audioManager = GameObject.Find ("CollectibleAudioManager").GetComponent<CollectibleAudioManager> ();
	}

	void Start () {

		float randomDistanceX = Random.Range (0.0f,0.3f);
		float randomDistanceY = Random.Range (0.0f, 0.3f);
		int randomDirectionX = Random.Range (1, 4);
		int randomDirectionY = Random.Range (1, 4);

		if (randomDirectionX >= 1 && randomDirectionX <= 2) { // Positive X direction
			if (randomDirectionY >= 1 && randomDirectionY <= 2) { //Positive Y direction
				destinationPoint = new Vector3(transform.position.x + randomDistanceX,transform.position.y + randomDistanceY,0);
			} else { //Negative Y Direction
				destinationPoint = new Vector3(transform.position.x + randomDistanceX,transform.position.y - randomDistanceY,0);
			}

		} else { // Negative X direction
			if (randomDirectionY >= 1 && randomDirectionY <= 2) { //Positive Y direction
				destinationPoint = new Vector3(transform.position.x - randomDistanceX,transform.position.y + randomDistanceY,0);
			} else { //Negative Y Direction
				destinationPoint = new Vector3(transform.position.x - randomDistanceX,transform.position.y - randomDistanceY,0);
			}
		}

		audioManager.PlayBoneSpawnSound ();
			
	}
	
	// Update is called once per frame
	void Update () {

		float step = hoverSpeed * Time.deltaTime;
		GetComponent<Rigidbody2D>().transform.position = Vector3.MoveTowards(transform.position, destinationPoint, step);
	}

	void OnTriggerEnter2D(Collider2D other){

		if (other.tag == "Player1" || other.tag == "Player2") {
			Player playerVariables = other.GetComponent<Player> ();
			if (!playerVariables.dead && !playerVariables.dying) {
				
				playerVariables.IncreaseBoneAmount(1);

				audioManager.PlayCollectBoneSound ();

				Destroy (gameObject);
			}
		}

	}
}
