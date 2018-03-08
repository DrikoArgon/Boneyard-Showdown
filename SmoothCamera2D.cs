
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class SmoothCamera2D : MonoBehaviour {

	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;
	public Transform target;

	// Update is called once per frame
	void Update () 
	{
		if (target)
		{
			Vector3 point = GetComponent<Camera>().WorldToViewportPoint(new Vector3(target.position.x,0,target.position.z));
			Vector3 delta = new Vector3(target.position.x,0,target.position.z) - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
			Vector3 destination = new Vector3(transform.position.x,0,transform.position.z) + delta;
			transform.position = Vector3.SmoothDamp( new Vector3(transform.position.x,0,transform.position.z), destination, ref velocity, dampTime);
		}

	}
}