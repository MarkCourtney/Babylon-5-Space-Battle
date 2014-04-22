using UnityEngine;
using System.Collections;

public class FollowLeader : MonoBehaviour {

	GameObject target;
	Vector3 look;
	// Use this for initialization
	void Start () {
	
		target = GameObject.FindGameObjectWithTag("Leader");
	}
	
	// Update is called once per frame
	void Update () {
	
		look = (target.transform.position - transform.position);
		transform.rotation = Quaternion.LookRotation(look);
	}
}