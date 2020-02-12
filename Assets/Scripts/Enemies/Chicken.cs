using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : PassiveEnemy {

    // Use this for initialization

    private void Awake() {
        InitializeEnemy();
    }

    void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        //Chicken dying
        CheckDeath();


        if (dying) {
            return;
        }


        //Chicken walking logic
        enemyDirection = wanderHandler.RandomWander();

        CheckInvinsibility();

        CheckDamageReceived();

        DefineDirectionToLook();

    }

    private void FixedUpdate() {

        if (dying) {
            return;
        }
        //Move chicken
        BasicMovement();
    }


}
