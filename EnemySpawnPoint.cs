using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour {

	public float timeToSpawn;
	public GameObject enemyToSpawnPrefab;
	public EnemyToSpawn myTypeOfEnemy;

	private float timeStampToSpawn;
	private bool canSpawn;

	public enum EnemyToSpawn{

		Chicken,
		Bandit,
		Thief
	}

	// Use this for initialization
	void Start () {

		canSpawn = true;
		timeStampToSpawn = 0;
	}
	
	// Update is called once per frame
	void Update () {

		if (canSpawn) {

			
			if (timeStampToSpawn < Time.time) {

				canSpawn = false;

				if (myTypeOfEnemy == EnemyToSpawn.Chicken) {
					
					GameObject enemy = (GameObject)Instantiate (enemyToSpawnPrefab, transform.position, Quaternion.identity);
					enemy.GetComponent<Chicken> ().mySpawner = gameObject;
				} else if (myTypeOfEnemy == EnemyToSpawn.Bandit) {
					GameObject enemy = (GameObject)Instantiate (enemyToSpawnPrefab, transform.position, Quaternion.identity);
					enemy.GetComponent<Bandit> ().mySpawner = gameObject;
				} else if (myTypeOfEnemy == EnemyToSpawn.Thief) {
					GameObject enemy = (GameObject)Instantiate (enemyToSpawnPrefab, transform.position, Quaternion.identity);
					enemy.GetComponent<Thief> ().mySpawner = gameObject;
				}


			}

		}
			
	}

	public void ResetSpawn(){

		canSpawn = true;
		timeStampToSpawn = Time.time + timeToSpawn;

	}
}
