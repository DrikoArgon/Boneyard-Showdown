using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public static GameObject instance;
	public GameObject boneCounter;
	public GameObject mergedBoneCounter;
	public Image returnText;
	public Image mergedReturnText;
	public Transform lifeBar;
	public Transform expBar;
	public GameObject levelUpEffectPrefab;
	public Transform levelUpEffectSpawner;
	public SpriteRenderer levelDisplayer;
	public Sprite lvl2Sprite;
	public Sprite lvl3Sprite;
	public Sprite lvl4Sprite;
	public Sprite lvl5Sprite;
	public GameObject collectibleBonePrefab;

	public GameObject magicProjectilePrefab;
	public GameObject knifePrefab;

	//Firing points
	public Transform upFiringPoint; 
	public Transform rightFiringPoint; 
	public Transform downFiringPoint; 

	//Stats variables
	public float speed;
	public float maxLife;
	private float lifeWhileInvulnerable;
	public float currentLife;
    public float currentExp;
    public float expNeededToNextLevel;
    public float magicDamage;
    public float stabDamage;

    public int boneAmount;
	public int level;
	public int maxLevel;


	//Controls
	public KeyCode moveUpKey;
	public KeyCode moveRightKey;
	public KeyCode moveLeftKey;
	public KeyCode moveDownKey;
	public KeyCode shootKey;
	public KeyCode stabKey;

	public string moveHorizontalGamepadAxis;
	public string moveVerticalGamepadAxis;
	public string shootMagicGamepadButton;
	public string stabGamepadButton;

	//Player Direction
	public PlayerDirection direction;
	public PlayerDirection inicialDirection;

	//Cooldowns
	public float shotCooldownInSeconds;
	public float knifeCooldownInSeconds;
	public float invulnerableSeconds;

	private float shotTimeStamp;
	private float knifeTimeStamp;
	private float invulnerableTimeStamp;

    //States
    private bool moving;
	private bool attacking;
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
		attacking = false;
		receivedDamage = false;
		invulnerable = false;
		levelingUp = false;
		direction = inicialDirection;
		boneCounterText = boneCounter.GetComponent<Text> ();
		mergedBoneCounterText = mergedBoneCounter.GetComponent<Text> ();
		invulnerableTimeStamp = 0;
		currentLife = maxLife;
		currentExp = 0;
		expNeededToNextLevel = 200;

		magicDamage = 1;
		stabDamage = 2;
	}

    private void Update() {
        
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
			if ((Input.GetKeyDown (shootKey) || Input.GetButtonDown(shootMagicGamepadButton)) && !dying && !dead) {

				if (shotTimeStamp <= Time.time) {
				
					if (direction == PlayerDirection.Up) {
						Instantiate (magicProjectilePrefab, upFiringPoint.position, Quaternion.Euler (new Vector3 (0, 0, 90)));
					} else if (direction == PlayerDirection.Right) {
						Instantiate (magicProjectilePrefab, rightFiringPoint.position, Quaternion.identity);
					} else if (direction == PlayerDirection.Left) {
						Instantiate (magicProjectilePrefab, rightFiringPoint.position, Quaternion.Euler (new Vector3 (0, 0, 180)));
					} else {
						Instantiate (magicProjectilePrefab, downFiringPoint.position, Quaternion.Euler (new Vector3 (0, 0, 270)));
					}

					shotTimeStamp = Time.time + shotCooldownInSeconds;

				}
			}

			//Attacking logic
			if ((Input.GetKeyDown (stabKey) || Input.GetButtonDown(stabGamepadButton)) && !dying && !dead) {

				if (knifeTimeStamp <= Time.time) {

					if (direction == PlayerDirection.Up) {
						Instantiate (knifePrefab, upFiringPoint.position, Quaternion.Euler (new Vector3 (0, 0, 90)));
					} else if (direction == PlayerDirection.Right) {
						Instantiate (knifePrefab, rightFiringPoint.position, Quaternion.identity);
					} else if (direction == PlayerDirection.Left) {
						Instantiate (knifePrefab, rightFiringPoint.position, Quaternion.Euler (new Vector3 (0, 0, 180)));
					} else {
						Instantiate (knifePrefab, downFiringPoint.position, Quaternion.Euler (new Vector3 (0, 0, 270)));
					}

					attacking = true;

					knifeTimeStamp = Time.time + knifeCooldownInSeconds;

				}
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

		if (level < maxLevel) {

			expBar.localScale = new Vector3(currentExp / expNeededToNextLevel,1,1);

			if (currentExp >= expNeededToNextLevel && !levelingUp) {
				LevelUp ();
			}
		}
		
			
	}

    void CheckStatusForAnimation() {
        if (!dying && !dead) {
            animator.Play("Walk");
        }
    }

    public void Move() {
        if (Input.GetKey(moveUpKey) || (Input.GetAxis(moveVerticalGamepadAxis) >= 0.5f)) {
            MoveUp();
        } else if (Input.GetKey(moveRightKey) || (Input.GetAxis(moveHorizontalGamepadAxis) >= 0.5f)) {
            MoveRight();
        } else if (Input.GetKey(moveLeftKey) || (Input.GetAxis(moveHorizontalGamepadAxis) <= -0.5f)) {
            MoveLeft();
        } else if (Input.GetKey(moveDownKey) || (Input.GetAxis(moveVerticalGamepadAxis) <= -0.5f)) {
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

	public void LevelUp(){

		levelingUp = true;
		Instantiate (levelUpEffectPrefab, levelUpEffectSpawner.transform.position, Quaternion.identity);
		level += 1;
		stabDamage += 1.5f;
		magicDamage += 1;
		maxLife += 1;
		currentLife = maxLife;
		currentExp -= expNeededToNextLevel;
		expNeededToNextLevel = expNeededToNextLevel * 2;

		if (level == 2) {
			levelDisplayer.sprite = lvl2Sprite;
		} else if (level == 3) {
			levelDisplayer.sprite = lvl3Sprite;
		} else if (level == 4) {
			levelDisplayer.sprite = lvl4Sprite;
		} else if (level == 5) {
			levelDisplayer.sprite = lvl5Sprite;
		}

		if (currentExp >= expNeededToNextLevel) {
			LevelUp ();
		} else {
			levelingUp = false;
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
