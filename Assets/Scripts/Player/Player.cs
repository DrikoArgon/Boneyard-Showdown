﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public static GameObject instance;

    public enum RepresentsPlayer {
        Player1,
        Player2
    }

    public RepresentsPlayer representsPlayer;

    //UI
	public GameObject boneCounter;
	public GameObject mergedBoneCounter;
	public Image returnText;
	public Image mergedReturnText;
	public Transform lifeBar;
	public Transform expBar;

    //Level up
    private PlayerLevelManager levelManager;

    public GameObject collectibleBonePrefab;

	private GameObject magicProjectilePrefab;
	private GameObject meleeAttackPrefab;

	//Firing points
	private Transform upFiringPoint;
    private Transform rightFiringPoint;
    private Transform downFiringPoint;

    private Transform upRightFiringPoint;
    private Transform downRightFiringPoint;

    //Stats variables
    public CharacterBaseStats baseStats;
    public CharacterStats characterStats;

    private float speed;
    private float maxLife;
    private float magicDamage;
    private float stabDamage;

    private float currentLife;
    private float lifeWhileInvulnerable;

    private int boneAmount;

    //Controls
    public PlayerInput inputMappings;

	//Player Direction
	public PlayerDirection direction;
	public PlayerDirection inicialDirection;

	//Cooldowns
	private float rangedAttackCooldown;
    private float meleeAttackCooldown;

    private float meleeAttackTimer;
    private float rangedAttackTimer;

	public float invulnerableSeconds;

    private float timeToMoveAfterAttack = 0.5f;
    private float elapsedTime;
	private float invulnerableTimeStamp;

    //States
    private Vector3 moveDirection;
    private bool moving;
	private bool meleeAttacking;
    private bool rangedAttacking;
    private bool isMeleeAttackOnCooldown;
    private bool isRangedAttackOnCooldown;

	public bool receivedDamage;
	public bool invulnerable;
	public bool dead;
	public bool dying;

	private Animator animator;
    private Rigidbody2D myRigidBody;
	private Text boneCounterText;
	private Text mergedBoneCounterText;

	//Variables for flashing effect
	public int flashDelay = 100;
	private SpriteRenderer mySpriteRenderer;
	private int flashingCounter;
	private bool toggleFlashing = false;

    private float meleeAttackAnimationLength;

    private void Awake() {

        animator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Use this for initialization
    void Start () {
		instance = this.gameObject;

		direction = inicialDirection;

		boneCounterText = boneCounter.GetComponent<Text> ();
		mergedBoneCounterText = mergedBoneCounter.GetComponent<Text> ();

		invulnerableTimeStamp = 0;

        maxLife = baseStats.maxLife;
        speed = baseStats.speed;
        magicDamage = baseStats.initialMagicDamage;
        stabDamage = baseStats.initialMeleeDamage;

        meleeAttackCooldown = baseStats.meleeAttackCooldown;
        rangedAttackCooldown = baseStats.rangedAttackCooldown;

        currentLife = maxLife;

        levelManager = GetComponent<PlayerLevelManager>();

        DefineCharacterStats();

        animator.runtimeAnimatorController = characterStats.animatorController;

        GameObject firingPointsParent = Instantiate(characterStats.firingPointsPrefab, transform );

        upFiringPoint = firingPointsParent.transform.Find("UpFiringPoint");
        downFiringPoint = firingPointsParent.transform.Find("DownFiringPoint");
        rightFiringPoint = firingPointsParent.transform.Find("RightFiringPoint");

        upRightFiringPoint = firingPointsParent.transform.Find("UpRightFiringPoint");
        downRightFiringPoint = firingPointsParent.transform.Find("DownRightFiringPoint");

        if (representsPlayer == RepresentsPlayer.Player1) {
            magicProjectilePrefab = characterStats.projectilePrefab;
            meleeAttackPrefab = characterStats.meleeAttackPrefab;
        } else {
            magicProjectilePrefab = characterStats.projectilePrefabPlayer2;
            meleeAttackPrefab = characterStats.meleeAttackPrefabPlayer2;
        }

        DefineAnimationLengths();
    }

    private void DefineCharacterStats() {

        if (representsPlayer == RepresentsPlayer.Player1) {
            
            characterStats = (CharacterStats)Instantiate(Resources.Load("ScriptableObjects/Characters/" + GameManager.instance.player1ChosenCharacter.ToString()));

        } else {

            characterStats = (CharacterStats)Instantiate(Resources.Load("ScriptableObjects/Characters/" + GameManager.instance.player2ChosenCharacter.ToString()));

            magicProjectilePrefab = characterStats.projectilePrefabPlayer2;
            meleeAttackPrefab = characterStats.meleeAttackPrefabPlayer2;
        }


    }

    private void Update() {

        CollectMoveInputs();
        CheckForCooldowns();
        CheckForAttackAnimationEnd();
        CheckStatusForAnimation();

        myRigidBody.velocity = Vector2.zero;

    }

    // Update is called once per frame
    void FixedUpdate () {

		//movement Logic
		if (!meleeAttacking && !dying && !rangedAttacking) {

            Move();

		}

		if (invulnerableTimeStamp < Time.time) {

			invulnerable = false;
			mySpriteRenderer.enabled = true;

			//Magic logic
			if ((Input.GetKeyDown (inputMappings.shootKey) || Input.GetButtonDown(inputMappings.shootMagicGamepadButton)) && !dying && !dead && !meleeAttacking && !rangedAttacking) {

                DoRangedAttack();
			}

			//Attacking logic
			if ((Input.GetKeyDown (inputMappings.stabKey) || Input.GetButtonDown(inputMappings.stabGamepadButton)) && !dying && !dead && !meleeAttacking && !rangedAttacking) {

                DoMeleeAttack();
			}

		} 

		if (invulnerable) {
			Flash ();
			lifeWhileInvulnerable = currentLife;
		}

        	
		if (receivedDamage && currentLife > 0) {
			ToggleInvinsibility ();
		}

		if (currentLife >= 0) {

			lifeBar.localScale = new Vector3 (currentLife / maxLife, 1, 1);

		}

		if (currentLife <= 0 && !dead && !dying) {

            ShakeCamera();

            dying = true;
            animator.Play("Dying");
			StartCoroutine (SpawnBones ());
		}

		boneCounterText.text = boneAmount.ToString ();
		mergedBoneCounterText.text = boneAmount.ToString ();

        if (!levelManager.IsMaxLevel()) {
            expBar.localScale = new Vector3(levelManager.GetCurrentExp() / levelManager.GetExpNeededForNextLevel(), 1, 1);
        }
        


    }

    void DefineAnimationLengths() {

        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;

        foreach(AnimationClip clip in clips) {

            String[] words = clip.name.Split('_');

            if(words[1] == "Attack") {
                meleeAttackAnimationLength = clip.length / 1.5f;
            }

        }


    }

    void CheckStatusForAnimation() {
        if (!dying && !dead && !meleeAttacking && !rangedAttacking) {
            animator.Play("Walk");
        }
    }

    void DoMeleeAttack() {

        if (!isMeleeAttackOnCooldown && !meleeAttacking) {

            animator.Play("MeleeAttack");

            if (direction == PlayerDirection.Up) {
                Instantiate(meleeAttackPrefab, upFiringPoint.position, Quaternion.identity);
            } else if (direction == PlayerDirection.Right) {
                Instantiate(meleeAttackPrefab, rightFiringPoint.position, Quaternion.identity);
            } else if (direction == PlayerDirection.UpRight) {
                Instantiate(meleeAttackPrefab, upRightFiringPoint.position, Quaternion.identity);
            } else if (direction == PlayerDirection.Down) {
                Instantiate(meleeAttackPrefab, downFiringPoint.position, Quaternion.identity);
            } else if (direction == PlayerDirection.DownRight) {
                Instantiate(meleeAttackPrefab, downRightFiringPoint.position, Quaternion.identity);
            }

            meleeAttacking = true;
            isMeleeAttackOnCooldown = true;
        }
       
    }

    void DoRangedAttack() {

        if (!isRangedAttackOnCooldown && !meleeAttacking) {

            if (direction == PlayerDirection.Up) {
                Instantiate(magicProjectilePrefab, upFiringPoint.position, Quaternion.Euler(new Vector3(0, 0, 90)));
            } else if (direction == PlayerDirection.Right) {
                Instantiate(magicProjectilePrefab, rightFiringPoint.position, Quaternion.identity);
            } else if (direction == PlayerDirection.UpRight) {
                Instantiate(magicProjectilePrefab, upRightFiringPoint.position, Quaternion.Euler(new Vector3(0, 0, 180)));
            } else if (direction == PlayerDirection.Down) {
                Instantiate(magicProjectilePrefab, downFiringPoint.position, Quaternion.Euler(new Vector3(0, 0, 270)));
            } else if (direction == PlayerDirection.DownRight) {
                Instantiate(magicProjectilePrefab, downRightFiringPoint.position, Quaternion.Euler(new Vector3(0, 0, 270)));
            }

            rangedAttacking = true;
            isRangedAttackOnCooldown = true;
        }
    }

    void CheckForAttackAnimationEnd() {
        if (meleeAttacking) {
            elapsedTime += Time.deltaTime;

            if(elapsedTime > meleeAttackAnimationLength) {
                isMeleeAttackOnCooldown = true;
                meleeAttacking = false;
                elapsedTime = 0;
            }

        }

        if (rangedAttacking) {

            elapsedTime += Time.deltaTime;

            if (elapsedTime > meleeAttackAnimationLength) {
                isRangedAttackOnCooldown = true;
                rangedAttacking = false;
                elapsedTime = 0;
            }
        }

    }

    void CheckForCooldowns() {

        if (isMeleeAttackOnCooldown) {

            meleeAttackTimer += Time.deltaTime;

            if (meleeAttackTimer > meleeAttackCooldown) {
                meleeAttackTimer = 0;
                isMeleeAttackOnCooldown = false;
            }
        }

        if (isRangedAttackOnCooldown) {

            rangedAttackTimer += Time.deltaTime;

            if (rangedAttackTimer > rangedAttackCooldown) {
                rangedAttackTimer = 0;
                isRangedAttackOnCooldown = false;
            }
        }

    }


    void CollectMoveInputs() {

        if (Input.GetKey(inputMappings.moveUpKey) || (Input.GetAxis(inputMappings.moveVerticalGamepadAxis) >= 0.5f)) {
            moveDirection.y = 1;
        } else if (Input.GetKey(inputMappings.moveDownKey) || (Input.GetAxis(inputMappings.moveVerticalGamepadAxis) <= -0.5f)) {
            moveDirection.y = -1;
        } else {
            moveDirection.y = 0;
        }

        if (Input.GetKey(inputMappings.moveRightKey) || (Input.GetAxis(inputMappings.moveHorizontalGamepadAxis) >= 0.5f)) {
            moveDirection.x = 1;
        } else if (Input.GetKey(inputMappings.moveLeftKey) || (Input.GetAxis(inputMappings.moveHorizontalGamepadAxis) <= -0.5f)) {
            moveDirection.x = -1;
        } else {
            moveDirection.x = 0;
        }

    }

    public void Move() {

        if(moveDirection.x != 0 && moveDirection.y != 0) {
            myRigidBody.transform.position += moveDirection * ( speed / 1.4f ) * Time.deltaTime;
        } else {
            myRigidBody.transform.position += moveDirection * speed * Time.deltaTime;
        }

        if(moveDirection.x > 0) {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), 1, 1);
            lifeBar.parent.localScale = new Vector3(Mathf.Abs(transform.localScale.x), 1, 1);
        } else if(moveDirection.x < 0) {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, 1, 1);
            lifeBar.parent.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, 1, 1);
        }

        if(moveDirection.x != 0 || moveDirection.y != 0) {
            if(moveDirection.x != 0) {
                animator.SetFloat("HorizontalMovement", Mathf.Abs(moveDirection.x));
                animator.SetFloat("VerticalMovement", 0f);
            } else {
                animator.SetFloat("HorizontalMovement", Mathf.Abs(moveDirection.x));
                animator.SetFloat("VerticalMovement", moveDirection.y);
            }          
        }

        DefinePlayerDirection();

    }

    void DefinePlayerDirection() {

        if (moveDirection.x != 0 && moveDirection.y != 0) { //Moving diagonally

            if (moveDirection.y > 0) {
                direction = PlayerDirection.UpRight;
            } else {
                direction = PlayerDirection.DownRight;
            }

        } else {

            if (moveDirection.x != 0) {
                direction = PlayerDirection.Right;
            }

            if (moveDirection.y != 0) {
                if (moveDirection.y > 0) {
                    direction = PlayerDirection.Up;
                } else {
                    direction = PlayerDirection.Down;
                }
            }

        }

    }

	public void TeleportToPosition(Vector3 pos){

		GetComponent<Rigidbody2D>().transform.position = pos;

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

    private void ShakeCamera() {
        if (transform.name == "Player") {
            CameraManager.instance.ShakePlayerCamera(0.1f, 3f, 0.5f, true);
        } else {
            CameraManager.instance.ShakePlayerCamera(0.1f, 3f, 0.5f, false);
        }
    }

	private void ToggleInvinsibility(){

        ShakeCamera();

        receivedDamage = false;
		lifeWhileInvulnerable = currentLife;
		invulnerable = true;
		invulnerableTimeStamp = Time.time + invulnerableSeconds;

	}


	public void TurnToGhost(){
        animator.Play("Dead");
		dying = false;
		dead = true;

		returnText.enabled = true;
		mergedReturnText.enabled = true;

	}

	public void Reborn(){
        animator.Play("Walk");
        dead = false;
		currentLife = maxLife;

		returnText.enabled = false;
		mergedReturnText.enabled = false;
	}

    public void DoDamageToPlayer(float damage) {
        currentLife -= damage;
        receivedDamage = true;
    }

    public bool IsMaxLife() {
        if (currentLife < maxLife) {
            return false;
        } else {
            return true;
        }
    }

    public void HealPlayer(float amount) {

        if(amount < 0) {
            return;
        }

        float newAmount = currentLife + amount;

        if(newAmount > maxLife) {
            newAmount = maxLife;
        }

        currentLife = newAmount;

    }

    public void HealPlayerCompletely() {
        currentLife = maxLife;
    }

    public void IncreaseMeleeDamage(float amount) {
        stabDamage += amount;
    }

    public void IncreaseRangedDamage(float amount) {
        magicDamage += amount;
    }

    public void IncreasePlayerMaxHealth(float amount) {
        maxLife += amount;
    }

    public void IncreaseBoneAmount(int amount) {
        boneAmount += amount;
    }

    public void DecreaseBoneAmount(int amount) {
        boneAmount -= amount;
    }

    public int GetBoneAmount() {
        return boneAmount;
    }

    public void GrantExp(float exp) {
        levelManager.GrantExp(exp);
    }

    public float GetMeleeDamage() {
        return stabDamage; 
    }

    public float GetMagicDamage() {
        return magicDamage;
    }

    IEnumerator SpawnBones(){

		int bonesToLose = boneAmount / 2;

		for (int i = 0; i < bonesToLose; i++) {
			Instantiate (collectibleBonePrefab, transform.position, Quaternion.identity);
			boneAmount--;
			yield return new WaitForSeconds(0.1f);

		}
	}

}


public enum PlayerDirection{

	Up,
	Right,
	Down,
	Left,
    UpRight,
    DownRight
}
