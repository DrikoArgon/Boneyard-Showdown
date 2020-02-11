using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderHandler {


    private float actionDelayTime;
    private float elapsedTime = 0;
    private EnemyDirection lastDirection;

    public WanderHandler(float timeToChangeDirection){
        actionDelayTime = timeToChangeDirection;
        lastDirection = EnemyDirection.None;
    }

    public EnemyDirection RandomWander() {

        elapsedTime += Time.deltaTime;

        if (elapsedTime > actionDelayTime) {

            elapsedTime = 0;

            int randomNumber = Random.Range(1, 30);

            if (randomNumber <= 5) {
                lastDirection = EnemyDirection.Up;
            } else if (randomNumber > 5 && randomNumber <= 10) {
                lastDirection = EnemyDirection.Right;
            } else if (randomNumber > 10 && randomNumber <= 15) {
                lastDirection = EnemyDirection.Left;
            } else if (randomNumber > 15 && randomNumber <= 20) {
                lastDirection = EnemyDirection.Down;
            } else {
                lastDirection = EnemyDirection.None;
            }

        }

        return lastDirection;

    }

}
