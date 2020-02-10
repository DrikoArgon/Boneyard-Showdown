using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonTeleporter : MonoBehaviour {

	public Transform DestinationToTeleport;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){

		other.transform.position = DestinationToTeleport.position;

	}
}
