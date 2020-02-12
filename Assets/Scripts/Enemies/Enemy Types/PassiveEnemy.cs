using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveEnemy : Enemy {

    public float delayToAct;

    protected WanderHandler wanderHandler;


    protected override void InitializeEnemy() {
        base.InitializeEnemy();

        wanderHandler = new WanderHandler(delayToAct);
    }

}
