using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public enum EnemyDirection {
    Up,
    Right,
    Down,
    Left,
    None
}

public class Enemy : MonoBehaviour {

    public float life;
	public float damage;
	public float speed;
	public GameObject collectibleBonePrefab;
	public bool receivedDamage;
	public bool invulnerable;
	public int numberOfBones;
    public AudioClip deathSound;
    public GameObject mySpawner;

    protected AudioSource source;
    protected Animator animator;
    protected Rigidbody2D myRigidBody;

    private int flashDelay = 2;
	protected SpriteRenderer mySpriteRenderer;
	private int flashingCounter;
	private bool toggleFlashing = false;

	protected float lifeWhileInvulnerable;
    protected EnemyDirection enemyDirection;

    public float invulnerableSeconds = 1;

	protected bool attacking;
	protected bool dying;
    protected bool walking;
    public AIPath aiPath;

	protected float invulnerableTimeStamp;

    protected virtual void InitializeEnemy() {

        source = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        //aiPath.GetComponent<AIPath>();

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

    protected virtual void CheckInvinsibility() {
        if (invulnerableTimeStamp < Time.time) {
            invulnerable = false;
            mySpriteRenderer.enabled = true;
        }

        if (invulnerable) {
            Flash();
            lifeWhileInvulnerable = life;
        }
    }

    protected virtual void CheckDamageReceived() {
        if (receivedDamage && life > 0) {
            ToggleInvinsibility();
        }
    }

    protected virtual void CheckStatusForAnimation() {
        if (!dying) {
            if (!attacking) {
                if (walking) {
                    animator.Play("Walk");
                } else {
                    animator.Play("Idle");
                }
            }
        }
    }

    protected void DefineDirectionToLook() {
        if(enemyDirection == EnemyDirection.Right) {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), 1, 1);
        } else if(enemyDirection == EnemyDirection.Left){
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, 1, 1);
        }
    }


    protected virtual void CheckDeath() {

        if (life <= 0 && !dying) {

            source.PlayOneShot(deathSound, 1.0f);
            animator.Play("Dying");
            dying = true;
            Destroy(GetComponent<Rigidbody2D>());
            Destroy(GetComponent<Collider2D>());

        }

    }

    protected virtual void DefineDirection() {

        switch (enemyDirection) {
            case EnemyDirection.Up:
                animator.SetFloat("HorizontalMovement", 0f);
                animator.SetFloat("VerticalMovement", 1f);
                break;
            case EnemyDirection.Right:
                animator.SetFloat("HorizontalMovement", 1f);
                animator.SetFloat("VerticalMovement", 0f);
                break;
            case EnemyDirection.Down:
                animator.SetFloat("HorizontalMovement", 0f);
                animator.SetFloat("VerticalMovement", -1f);
                break;
            case EnemyDirection.Left:
                animator.SetFloat("HorizontalMovement", 1f);
                animator.SetFloat("VerticalMovement", 0f);
                break;
            case EnemyDirection.None:
                walking = false;
                break;
            default:
                break;
        }
    }

    protected virtual void BasicMovement() {

        switch (enemyDirection) {
            case EnemyDirection.Up:
                myRigidBody.transform.position += Vector3.up * speed * Time.deltaTime;
                walking = true;
                break;
            case EnemyDirection.Right:
                myRigidBody.transform.position += Vector3.right * speed * Time.deltaTime;
                walking = true;
                break;
            case EnemyDirection.Down:
                myRigidBody.transform.position += Vector3.down * speed * Time.deltaTime;
                walking = true;
                break;
            case EnemyDirection.Left:
                myRigidBody.transform.position += Vector3.left * speed * Time.deltaTime;
                walking = true;
                break;
            case EnemyDirection.None:
                walking = false;
                break;
            default:
                break;
        }

        DefineDirection();

    }

    protected void DefineAnimationDirection() {

        if (aiPath.desiredVelocity.x > 0) {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), 1, 1);
        } else if (aiPath.desiredVelocity.x < 0) {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, 1, 1);
        }

        if (aiPath.desiredVelocity.x != 0 || aiPath.desiredVelocity.y != 0) {
            if (aiPath.desiredVelocity.x != 0) {
                animator.SetFloat("HorizontalMovement", 1f);
                animator.SetFloat("VerticalMovement", 0f);
            } else {
                animator.SetFloat("HorizontalMovement", 0f);
                if(aiPath.desiredVelocity.y > 0) {
                    animator.SetFloat("VerticalMovement", 1f);
                } else {
                    animator.SetFloat("VerticalMovement", -1f);
                }                
            }

            walking = true;
        } else {
            walking = false;
        }

    }

    protected IEnumerator SpawnBones() {

        for (int i = 0; i < numberOfBones; i++) {
            Instantiate(collectibleBonePrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);

        }
    }

    protected IEnumerator ResetSpawner() {

        float timeToWait = 0.1f * numberOfBones + 0.2f;
        yield return new WaitForSeconds(timeToWait);
        mySpawner.GetComponent<EnemySpawnPoint>().ResetSpawn();
        Destroy(gameObject);
    }

    virtual public void DoDamage(){

	}

	virtual public void Disappear(){
        mySpriteRenderer.enabled = false;

        StartCoroutine(SpawnBones());

        StartCoroutine(ResetSpawner());
    }
}
