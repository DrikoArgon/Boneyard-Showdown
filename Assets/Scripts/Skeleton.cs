using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour {

	public float life;
	public int damage;
	public float speed;
	public DirectionToMove direction;
	public AudioClip riseSound;
	public AudioClip attackingCastleSound;

	private Animator animator;
	private Skeleton skeletonToAttack;
	private GameManager gameManager;
	private AudioSource source;

	private bool attackingSkeleton;
	private bool attackingCastle;
	private bool dying;
	private bool rising;

	public enum DirectionToMove {

		Right,
		Left
	}

	// Use this for initialization
	void Start () {

		source = GetComponent<AudioSource> ();
		animator = GetComponent<Animator> ();
		attackingSkeleton = false;
		attackingCastle = false;
		dying = false;
		rising = true;
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();


		source.PlayOneShot (riseSound, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {

		if (!attackingSkeleton && !dying && !rising && !attackingCastle) {
			if (direction == DirectionToMove.Right) {
				GetComponent<Rigidbody2D> ().transform.position += Vector3.right * speed * Time.deltaTime;
			} else {
				GetComponent<Rigidbody2D> ().transform.position += Vector3.left * speed * Time.deltaTime;
			}
		}

		if (life <= 0) {
			animator.SetBool ("attacking", false);
			animator.SetBool ("walking", false);
			animator.SetBool ("dead", true);
			attackingSkeleton = false;
			attackingCastle = false;
			dying = true;
			gameManager.player1CastleUnderAttack = false;
			gameManager.player2CastleUnderAttack = false;

			Destroy (GetComponent<Rigidbody2D> ());
			Destroy (GetComponent<BoxCollider2D> ());
		}
			
		
	}

	void OnTriggerEnter2D(Collider2D other){

		if (direction == DirectionToMove.Right) {
			if (other.tag == "Player2Skeleton" && !attackingCastle) {
				attackingSkeleton = true;
				skeletonToAttack = other.GetComponent<Skeleton> ();
				animator.SetBool ("walking", false);
				animator.SetBool ("attacking", true);

			} else if(other.tag == "Player2Castle"){
				attackingCastle = true;
				animator.SetBool ("walking", false);
				animator.SetBool ("attacking", true);
			}
		} else {
			if (other.tag == "Player1Skeleton" && !attackingCastle) {
				attackingSkeleton = true;
				skeletonToAttack = other.GetComponent<Skeleton> ();
				animator.SetBool ("walking", false);
				animator.SetBool ("attacking", true);

			} else if(other.tag == "Player1Castle"){
				attackingCastle = true;
				animator.SetBool ("walking", false);
				animator.SetBool ("attacking", true);
			}
		}

			
	}

	public void DoDamage(){

		if (attackingSkeleton && !attackingCastle) {
			skeletonToAttack.life -= damage;

			if (skeletonToAttack.life <= 0) {
				animator.SetBool ("attacking", false);
				animator.SetBool ("walking", true);
				attackingSkeleton = false;
			}
		} 

		if(attackingCastle) {
			
			if (direction == DirectionToMove.Right) {
				gameManager.player2CastleLife -= damage;
				gameManager.player2CastleUnderAttack = true;
			} else {
				gameManager.player1CastleLife -= damage;
				gameManager.player1CastleUnderAttack = true;
			}

			source.PlayOneShot (attackingCastleSound, 1.0f);
		}


	}

	public void Disappear(){

		Destroy (gameObject);
	}

	public void StartWalking(){

		rising = false;
		animator.SetBool ("rising", false);
		animator.SetBool ("walking", true);
	}
}
