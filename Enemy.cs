using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float life;
	public float damage;
	public float speed;
	public GameObject collectibleBonePrefab;
	public bool receivedDamage;
	public bool invulnerable;
	public int numberOfBones;

	private int flashDelay = 2;
	protected SpriteRenderer mySpriteRenderer;
	private int flashingCounter;
	private bool toggleFlashing = false;
	protected float lifeWhileInvulnerable;

	public float invulnerableSeconds = 1;

	protected bool attacking;
	protected bool dying;

	protected float invulnerableTimeStamp;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

//		if (life <= 0) {
//			
//			animator.SetBool ("attacking", false);
//			animator.SetBool ("dead", true);
//			attacking = false;
//			dying = true;
//			Destroy (GetComponent<Rigidbody2D> ());
//			if (GetComponent<BoxCollider2D> () != null) {
//				Destroy (GetComponent<BoxCollider2D> ());
//			} else if(GetComponent<CircleCollider2D> () != null){
//				Destroy (GetComponent<CircleCollider2D> ());
//			}
//
//		}
		
	}

	public void Flash(){


		if(flashingCounter >= flashDelay){ 

			flashingCounter = 0;

			toggleFlashing = !toggleFlashing;

			if(toggleFlashing) {
				mySpriteRenderer.enabled = true;
			}
			else {
				mySpriteRenderer.enabled = false;
			}

		}
		else {
			flashingCounter++;
		}

	}

	public void ToggleInvinsibility(){
		receivedDamage = false;
		invulnerable = true;
		invulnerableTimeStamp = Time.time + invulnerableSeconds;

	}

	virtual public void DoDamage(){

	}

	virtual public void Disappear(){
		Destroy (gameObject);
	}
}
