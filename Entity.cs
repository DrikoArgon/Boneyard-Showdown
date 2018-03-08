using UnityEngine;
using System.Collections;

public abstract class Entity : MonoBehaviour {

	public float speed;
	public EntityDirection direction;


	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void TeleportToPosition(Vector2 pos){

		GetComponent<Rigidbody2D>().transform.position = pos;

	}

}


public enum EntityDirection{

	Up,
	Right,
	Down,
	Left
}
