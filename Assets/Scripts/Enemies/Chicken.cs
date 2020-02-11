using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : Enemy {

	public float delayToAct;
    
	private int direction;

    private WanderHandler wanderHandler;

	// Use this for initialization
	void Start () {

		source = GetComponent<AudioSource> ();
		animator = GetComponent<Animator> ();
        myRigidBody = GetComponent<Rigidbody2D>();
		receivedDamage = false;
		mySpriteRenderer = GetComponent<SpriteRenderer> ();
		dying = false;
        wanderHandler = new WanderHandler(delayToAct);
		int randomNumber = Random.Range (1, 6);

	}
	
	// Update is called once per frame
	void Update () {

        if (dying) {
            return;
        }

        //Chicken dying
        CheckDeath();

        //Chicken walking logic
        enemyDirection = wanderHandler.RandomWander();

        //Move chicken
        BasicMovement();

        CheckInvinsibility();

        CheckDamageReceived();

        DefineDirectionToLook();

    }


}
