//using UnityEngine;
//using System.Collections;
//
//
//public class Player {
//
//	public static GameObject instance;
//
//	private Animator animator;
//
//	//booleanos para controle de movimento do jogador
//	private bool movingUp = false;
//	private bool movingRight = false;
//	private bool movingDown = false;
//	private bool movingLeft = false;
//
//	void Start () {
//		instance = this.gameObject;
//		LoadPosition();
//		animator = GetComponent<Animator>();
//		direction = EntityDirection.Down;
//
//	}
//
//	void FixedUpdate(){
//
//		if (movingUp) {
//			MoveUp();
//		} else if (movingRight) {
//			MoveRight ();
//		} else if (movingDown) {
//			MoveDown();
//		} else if(movingLeft){
//			MoveLeft();
//		}
//
//	}
//
//	void OnDestroy(){
//		SavePosition();
//	}
//
//	void OnApplicationQuit(){
//		SavePosition();
//	}
//
//	void OnApplicationPause(){
//		SavePosition();
//	}
//	// função moveUp
//	//
//	// Move o jogador para cima e aciona as animações respectivas
//	//   
//	public void MoveUp(){
//		GetComponent<Rigidbody2D> ().transform.position += Vector3.up * speed * Time.deltaTime;
//		animator.SetBool ("moving", true);
//		animator.SetInteger ("direction", 1);
//		movingUp = true;
//		movingRight = false;
//		movingDown = false;
//		movingLeft = false;
//		direction = EntityDirection.Up;
//	}
//
//	// função moveRight
//	//
//	// Move o jogador para direita e aciona as animações respectivas
//	//   
//	public void MoveRight(){
//		GetComponent<Rigidbody2D> ().transform.position += Vector3.right * speed * Time.deltaTime;
//		animator.SetBool ("moving", true);
//		animator.SetInteger ("direction", 2);
//		movingUp = false;
//		movingRight = true;
//		movingDown = false;
//		movingLeft = false;
//		direction = EntityDirection.Right;
//	}
//
//	// função moveDown
//	//
//	// Move o jogador para baixo e aciona as animações respectivas
//	//   
//	public void MoveDown(){
//		GetComponent<Rigidbody2D> ().transform.position += Vector3.down * speed * Time.deltaTime;
//		animator.SetBool ("moving", true);
//		animator.SetInteger ("direction", 3);
//		movingUp = false;
//		movingRight = false;
//		movingDown = true;
//		movingLeft = false;
//		direction = EntityDirection.Down;
//	}
//
//	// função moveLeft
//	//
//	// Move o jogador para esquerda e aciona as animações respectivas
//	//   
//	public void MoveLeft(){
//		GetComponent<Rigidbody2D> ().transform.position += Vector3.left * speed * Time.deltaTime;
//		animator.SetBool ("moving", true);
//		animator.SetInteger ("direction", 4);
//		movingUp = false;
//		movingRight = false;
//		movingDown = false;
//		movingLeft = true;
//		direction = EntityDirection.Left;
//	}
//
//
//	// função idle
//	//
//	// retira as animações de movimento e os movimentos do jogador
//	//   
//	public void Idle(){
//		animator.SetBool ("moving", false);
//
//		movingUp = false;
//		movingRight = false;
//		movingDown = false;
//		movingLeft = false;
//	}
//
//	public void SavePosition(){
//		PlayerPrefs.SetFloat("PlayerX",transform.position.x);
//		PlayerPrefs.SetFloat("PlayerY",transform.position.y);
//		PlayerPrefs.Save();
//	}
//
//	public void LoadPosition(){
//		transform.position = new Vector3(PlayerPrefs.GetFloat("PlayerX"),PlayerPrefs.GetFloat("PlayerY"));
//
//	}
//}
