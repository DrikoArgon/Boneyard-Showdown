using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseHandler 
{

    private Animator enemyAnimator;
    private Rigidbody2D enemyRigidBody;
    private Transform enemyTransform;
    private float enemySpeed;

    public ChaseHandler(Animator animator, Rigidbody2D rb, Transform transform, float speed) {
        enemyAnimator = animator;
        enemyRigidBody = rb;
        enemyTransform = transform;
        enemySpeed = speed;
    }

    public EnemyDirection ChaseMelee(Transform target) {

        if (target.position.x >= enemyTransform.position.x) {

            //If close enough, chase on the y axis
            if (Mathf.Abs(target.position.x - enemyTransform.position.x) < 0.1) {
                if (target.position.y > enemyTransform.position.y) {

                    ChaseUp();
                    return EnemyDirection.Up;

                } else if (target.position.y < enemyTransform.position.y) {

                    ChaseDown();
                    return EnemyDirection.Down;
                }

            } else {

                ChaseRight();
                return EnemyDirection.Right;
            }

        }else if (target.position.x < enemyTransform.position.x) {

            if (Mathf.Abs(target.position.x - enemyTransform.position.x) < 0.1) {
                if (target.position.y > enemyTransform.position.y) {

                    ChaseUp();
                    return EnemyDirection.Up;

                } else if (target.position.y < enemyTransform.position.y) {

                    ChaseDown();
                    return EnemyDirection.Down;
                }
            } else {

                ChaseLeft();
                return EnemyDirection.Left;
            }

        }

        return EnemyDirection.Right;
    }


    void ChaseUp() {

        enemyAnimator.SetFloat("HorizontalMovement", 0f);
        enemyAnimator.SetFloat("VerticalMovement", 1f);
        enemyRigidBody.transform.position += Vector3.up * enemySpeed * Time.deltaTime;
    }

    void ChaseDown() {

        enemyAnimator.SetFloat("HorizontalMovement", 0f);
        enemyAnimator.SetFloat("VerticalMovement", -1f);
        enemyRigidBody.transform.position += Vector3.down * enemySpeed * Time.deltaTime;
    }

    void ChaseRight() {

        enemyAnimator.SetFloat("HorizontalMovement", 1f);
        enemyAnimator.SetFloat("VerticalMovement", 0f);
        enemyRigidBody.transform.position += Vector3.right * enemySpeed * Time.deltaTime;

    }

    void ChaseLeft() {


        enemyAnimator.SetFloat("HorizontalMovement", 1f);
        enemyAnimator.SetFloat("VerticalMovement", 0f);
        enemyRigidBody.transform.position += Vector3.left * enemySpeed * Time.deltaTime;

    }

}
