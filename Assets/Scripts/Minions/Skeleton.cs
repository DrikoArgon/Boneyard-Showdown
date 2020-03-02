using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Minion {


    private void Awake() {
        Initialize();
    }

    // Use this for initialization
    void Start () {

		source = GetComponent<AudioSource> ();
		animator = GetComponent<Animator> ();

		source.PlayOneShot (minionStats.riseSound, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {

        CheckIfIsDead();
		
	}

    private void FixedUpdate() {

        Move();

    }

    void OnTriggerEnter2D(Collider2D other){

		if (direction == DirectionToMove.Right) {
			if (other.tag == "Player2Minion" && !attackingCastle && !attackingMinion) {

                TryToStartAttackingSkeleton(other);
				

			} else if(other.tag == "Player2Castle" && !attackingCastle) {

                StartAttackingCastle(other);

            }
		} else {
			if (other.tag == "Player1Minion" && !attackingCastle && !attackingMinion) {

                TryToStartAttackingSkeleton(other);

            } else if(other.tag == "Player1Castle" && !attackingCastle) {

                StartAttackingCastle(other);

            }
		}

			
	}

    void TryToStartAttackingSkeleton(Collider2D other) {

        Minion target = other.GetComponent<Minion>();

        attackingMinion = true;
        minionToAttack = target;
        animator.Play("Attack");


    }

    void StartAttackingCastle(Collider2D other) {

        attackingCastle = true;
        animator.Play("Attack");

    }

	public void DoDamage(){

		if (attackingMinion && !attackingCastle) {
            minionToAttack.AttackMinion(minionStats.damage);

			if (minionToAttack.IsMinionDead()) {
                animator.Play("Walk");
                attackingMinion = false;
			}
		} 

		if(attackingCastle) {
			
			if (direction == DirectionToMove.Right) {
                GameManager.instance.AttackCastle(false, minionStats.damage);
			} else {
                GameManager.instance.AttackCastle(true, minionStats.damage);
            }

			source.PlayOneShot (minionStats.attackingCastleSound, 1.0f);
		}


	}

	public void Disappear(){

		Destroy (gameObject);
	}

	public void StartWalking(){

		rising = false;
        animator.Play("Walk");
	}
}
