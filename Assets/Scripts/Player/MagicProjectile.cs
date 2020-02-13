using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicProjectile : MonoBehaviour {

	public float projectileSpeed;
	public float timeToDestroy;
	public float magicDamage;
	public PlayerWhoOwnsTheProjectile player;
	public AudioClip shootSound;


	private AudioSource source;
	private Player playerController;
	private Direction directionToFire;
	public enum PlayerWhoOwnsTheProjectile
	{
		Player1,
		Player2
	}

	void Awake(){

		source = GetComponent<AudioSource> ();
	}

	// Use this for initialization
	void Start () {
		if (player == PlayerWhoOwnsTheProjectile.Player1) {
			playerController = GameObject.Find ("Player").GetComponent<Player> ();
		} else {
			playerController = GameObject.Find ("Player2").GetComponent<Player> ();
		}

		magicDamage = playerController.magicDamage;

		if (playerController.direction == PlayerDirection.Up) {
			directionToFire = Direction.Up;
		} else if (playerController.direction == PlayerDirection.Right) {
			directionToFire = Direction.Right;
		} else if (playerController.direction == PlayerDirection.Left) {
			directionToFire = Direction.Left;
		} else {
			directionToFire = Direction.Down;
		}

		source.PlayOneShot (shootSound, 1.0f);
			
		Destroy (gameObject, timeToDestroy);

		
	}
	
	// Update is called once per frame
	void Update () {

		magicDamage = playerController.magicDamage;

		if (directionToFire == Direction.Up) {
			GetComponent<Rigidbody2D> ().transform.position += Vector3.up * projectileSpeed * Time.deltaTime;
		} else if (directionToFire == Direction.Right) {
			GetComponent<Rigidbody2D> ().transform.position += Vector3.right * projectileSpeed * Time.deltaTime;
		} else if (directionToFire == Direction.Left) {
			GetComponent<Rigidbody2D> ().transform.position += Vector3.left * projectileSpeed * Time.deltaTime;
		} else {
			GetComponent<Rigidbody2D> ().transform.position += Vector3.down * projectileSpeed * Time.deltaTime;
		}

	}

	void OnTriggerEnter2D(Collider2D other){

		if (other.tag == "Scenario") {
			Destroy (gameObject);
		} else if (other.tag == "Enemy") {
			Enemy enemyVariables = other.GetComponent<Enemy> ();
			enemyVariables.life -= magicDamage;
			enemyVariables.receivedDamage = true;

            if (player == PlayerWhoOwnsTheProjectile.Player1) {
                CameraManager.instance.ShakePlayerCamera(0.1f, 3f, 0.5f, true);
            } else {
                CameraManager.instance.ShakePlayerCamera(0.1f, 3f, 0.5f, false);
            }


            Destroy (gameObject);

		}

		if (player == PlayerWhoOwnsTheProjectile.Player1) {
			if (other.tag == "Player2") {
				Player enemyPlayer = other.GetComponent<Player> ();
				if (!enemyPlayer.invulnerable) {
					enemyPlayer.currentLife -= magicDamage;
					enemyPlayer.receivedDamage = true;
				}

                Destroy (gameObject);
			}
		} else {
			if (other.tag == "Player1") {
				Player enemyPlayer = other.GetComponent<Player> ();
				if (!enemyPlayer.invulnerable) {
					enemyPlayer.currentLife -= magicDamage;
					enemyPlayer.receivedDamage = true;
				}

                Destroy (gameObject);
			}
		}

	}
}

public enum Direction{

	Up,
	Right,
	Down,
	Left
}
