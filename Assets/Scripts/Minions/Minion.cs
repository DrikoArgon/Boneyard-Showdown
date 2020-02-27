using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Minion : MonoBehaviour
{

    public MinionStats minionStats;
    public DirectionToMove direction;

    protected bool attackingMinion;
    protected bool attackingCastle;
    protected bool dying;
    protected bool rising;

    protected Animator animator;
    protected Minion minionToAttack;
    protected AudioSource source;
    protected Rigidbody2D rigidBody;

    protected float currentLife;

    public enum DirectionToMove {
        Right,
        Left
    }

    protected void Initialize() {
        source = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        rising = true;
        currentLife = minionStats.life;
    }

    public virtual void AttackMinion(float damageAmount) {
        currentLife -= damageAmount;

        if(currentLife < 0) {
            currentLife = 0;
        }
    }

    protected virtual void Move() {

        if (!attackingMinion && !dying && !rising && !attackingCastle) {
            if (direction == DirectionToMove.Right) {
                rigidBody.transform.position += Vector3.right * minionStats.speed * Time.deltaTime;
            } else {
                rigidBody.transform.position += Vector3.left * minionStats.speed * Time.deltaTime;
            }
        }

    }

    protected void CheckIfIsDead() {

        if (currentLife <= 0) {
            Die();
        }

    }

    protected virtual void Die() {

        animator.Play("Dying");
        attackingMinion = false;
        attackingCastle = false;
        dying = true;
        GameManager.instance.player1CastleUnderAttack = false;
        GameManager.instance.player2CastleUnderAttack = false;

        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<Collider2D>());

    }

    public bool IsMinionDead() {
        return dying;
    }


}
