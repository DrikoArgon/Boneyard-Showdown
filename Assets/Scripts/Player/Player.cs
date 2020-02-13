using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public static GameObject instance;

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
	public Transform upFiringPoint; 
	public Transform rightFiringPoint; 
	public Transform downFiringPoint;

    //Stats variables
    public CharacterBaseStats baseStats;
    public CharacterStats characterStats;

    private float speed;
    private float maxLife;
    private float magicDamage;
    private float stabDamage;

    private float currentLife;
    private float lifeWhileInvulnerable;

    public int boneAmount;

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

	private float shotTimeStamp;
	private float knifeTimeStamp;
	private float invulnerableTimeStamp;

    //States
    private bool moving;
	private bool attacking;
    private bool isMeleeAttackOnCooldown;
    private bool isRangedAttackOnCooldown;

	public bool receivedDamage;
	public bool invulnerable;
	public bool dead;
	public bool dying;
	public bool levelingUp;


	private Animator animator;
    private Rigidbody2D myRigidBody;
	private Text boneCounterText;
	private Text mergedBoneCounterText;

	//Variables for flashing effect
	public int flashDelay = 100;
	private SpriteRenderer mySpriteRenderer;
	private int flashingCounter;
	private bool toggleFlashing = false;


    private void Awake() {

        animator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Use this for initialization
    void Start () {
		instance = this.gameObject;

		shotTimeStamp = 0;
		knifeTimeStamp = 0;

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

        magicProjectilePrefab = characterStats.projectilePrefab;
        meleeAttackPrefab = characterStats.meleeAttackPrefab;

        if(transform.name == "Player") {
            meleeAttackPrefab.GetComponent<MeleeAttack>().player = MeleeAttack.PlayerWhoOwnsTheKnife.Player1;
            magicProjectilePrefab.GetComponent<MagicProjectile>().player = MagicProjectile.PlayerWhoOwnsTheProjectile.Player1;
        } else {
            meleeAttackPrefab.GetComponent<MeleeAttack>().player = MeleeAttack.PlayerWhoOwnsTheKnife.Player2;
            magicProjectilePrefab.GetComponent<MagicProjectile>().player = MagicProjectile.PlayerWhoOwnsTheProjectile.Player2;
        }
        

    }

    private void Update() {

        CheckForCooldowns();

    }

    // Update is called once per frame
    void FixedUpdate () {
		//movement Logic
		if (!attacking && !dying) {

            Move();

		}


		if (invulnerableTimeStamp < Time.time) {

			invulnerable = false;
			mySpriteRenderer.enabled = true;

			//Magic logic
			if ((Input.GetKeyDown (inputMappings.shootKey) || Input.GetButtonDown(inputMappings.shootMagicGamepadButton)) && !dying && !dead) {

                DoRangedAttack();
			}

			//Attacking logic
			if ((Input.GetKeyDown (inputMappings.stabKey) || Input.GetButtonDown(inputMappings.stabGamepadButton)) && !dying && !dead) {

                DoMeleeAttack();
			}

		} 

		if (invulnerable) {
			Flash ();
			lifeWhileInvulnerable = currentLife;
		}


		if (knifeTimeStamp <= Time.time) {
			attacking = false;
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

    void CheckStatusForAnimation() {
        if (!dying && !dead) {
            animator.Play("Walk");
        }
    }

    void DoMeleeAttack() {

        if (!isMeleeAttackOnCooldown && !attacking) {

            if (direction == PlayerDirection.Up) {
                Instantiate(meleeAttackPrefab, upFiringPoint.position, Quaternion.Euler(new Vector3(0, 0, 90)));
            } else if (direction == PlayerDirection.Right) {
                Instantiate(meleeAttackPrefab, rightFiringPoint.position, Quaternion.identity);
            } else if (direction == PlayerDirection.Left) {
                Instantiate(meleeAttackPrefab, rightFiringPoint.position, Quaternion.Euler(new Vector3(0, 0, 180)));
            } else {
                Instantiate(meleeAttackPrefab, downFiringPoint.position, Quaternion.Euler(new Vector3(0, 0, 270)));
            }

            attacking = true;
            isMeleeAttackOnCooldown = true;
        }
       
    }

    void DoRangedAttack() {

        if (!isRangedAttackOnCooldown && !attacking) {

            if (direction == PlayerDirection.Up) {
                Instantiate(magicProjectilePrefab, upFiringPoint.position, Quaternion.Euler(new Vector3(0, 0, 90)));
            } else if (direction == PlayerDirection.Right) {
                Instantiate(magicProjectilePrefab, rightFiringPoint.position, Quaternion.identity);
            } else if (direction == PlayerDirection.Left) {
                Instantiate(magicProjectilePrefab, rightFiringPoint.position, Quaternion.Euler(new Vector3(0, 0, 180)));
            } else {
                Instantiate(magicProjectilePrefab, downFiringPoint.position, Quaternion.Euler(new Vector3(0, 0, 270)));
            }

            attacking = true;
            isRangedAttackOnCooldown = true;
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

    public void Move() {
        if (Input.GetKey(inputMappings.moveUpKey) || (Input.GetAxis(inputMappings.moveVerticalGamepadAxis) >= 0.5f)) {
            MoveUp();
        } else if (Input.GetKey(inputMappings.moveRightKey) || (Input.GetAxis(inputMappings.moveHorizontalGamepadAxis) >= 0.5f)) {
            MoveRight();
        } else if (Input.GetKey(inputMappings.moveLeftKey) || (Input.GetAxis(inputMappings.moveHorizontalGamepadAxis) <= -0.5f)) {
            MoveLeft();
        } else if (Input.GetKey(inputMappings.moveDownKey) || (Input.GetAxis(inputMappings.moveVerticalGamepadAxis) <= -0.5f)) {
            MoveDown();
        }

    }

    public void MoveUp(){
		myRigidBody.transform.position += Vector3.up * speed * Time.deltaTime;
		animator.SetFloat("HorizontalMovement", 0f);
        animator.SetFloat("VerticalMovement", 1f);
		direction = PlayerDirection.Up;
	}

	// função moveRight
	//
	// Move o jogador para direita e aciona as animações respectivas
	//   
	public void MoveRight(){
        myRigidBody.transform.position += Vector3.right * speed * Time.deltaTime;
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), 1, 1);
        lifeBar.parent.localScale = new Vector3(Mathf.Abs(transform.localScale.x), 1, 1);
        animator.SetFloat("HorizontalMovement", 1f);
        animator.SetFloat("VerticalMovement", 0f);
        direction = PlayerDirection.Right;
    }

	// função moveDown
	//
	// Move o jogador para baixo e aciona as animações respectivas
	//   
	public void MoveDown(){
        myRigidBody.transform.position += Vector3.down * speed * Time.deltaTime;
        animator.SetFloat("HorizontalMovement", 0f);
        animator.SetFloat("VerticalMovement", -1f);
        direction = PlayerDirection.Down;
    }

	// função moveLeft
	//
	// Move o jogador para esquerda e aciona as animações respectivas
	//   
	public void MoveLeft(){
        myRigidBody.transform.position += Vector3.left * speed * Time.deltaTime;
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, 1, 1);
        lifeBar.parent.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, 1, 1);
        animator.SetFloat("HorizontalMovement", 1f);
        animator.SetFloat("VerticalMovement", 0f);
        direction = PlayerDirection.Left;
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
	Left
}
