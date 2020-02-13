using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour {

	public PlayerWhoOwnsTheKnife player;
	public float stabDamage;

	private Direction directionToStab;
	private Player playerController;
	private float timeToDestroy;

	public enum PlayerWhoOwnsTheKnife
	{
		Player1,
		Player2
	}

	// Use this for initialization
	void Start () {

		timeToDestroy = 0.5f;
	

		if (player == PlayerWhoOwnsTheKnife.Player1) {
			playerController = GameObject.Find ("Player").GetComponent<Player> ();
		} else {
			playerController = GameObject.Find ("Player2").GetComponent<Player> ();
		}

        stabDamage = playerController.GetMeleeDamage(); ;

		if (playerController.direction == PlayerDirection.Up) {
			directionToStab = Direction.Up;
		} else if (playerController.direction == PlayerDirection.Right) {
			directionToStab = Direction.Right;
		} else if (playerController.direction == PlayerDirection.Left) {
			directionToStab = Direction.Left;
		} else {
			directionToStab = Direction.Down;
		}

		Destroy (gameObject, timeToDestroy);
		
	}
	
	// Update is called once per frame
	void Update () {

        stabDamage = playerController.GetMeleeDamage();

	}

	void OnTriggerEnter2D(Collider2D other){

		if (other.tag == "Enemy") {
			Enemy enemyVariables = other.GetComponent<Enemy> ();
			enemyVariables.life -= stabDamage;
			enemyVariables.receivedDamage = true;

            if (player == PlayerWhoOwnsTheKnife.Player1) {
                CameraManager.instance.ShakePlayerCamera(0.1f, 3f, 0.5f, true);
            } else {
                CameraManager.instance.ShakePlayerCamera(0.1f, 3f, 0.5f, false);
            }

        }

		if (player == PlayerWhoOwnsTheKnife.Player1) {
			if (other.tag == "Player2") {
				Player enemyPlayer = other.GetComponent<Player> ();
				if (!enemyPlayer.invulnerable) {
					enemyPlayer.DoDamageToPlayer(stabDamage);
				}

            }
		} else {
			if (other.tag == "Player1") {
				Player enemyPlayer = other.GetComponent<Player> ();
				if (!enemyPlayer.invulnerable) {
					enemyPlayer.DoDamageToPlayer(stabDamage);
				}

            }
		}

        

    }

}
