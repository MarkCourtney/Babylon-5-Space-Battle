using UnityEngine;
using System.Collections;

public class FollowLeader : MonoBehaviour {

	GameObject target;
	Vector3 look;

	void Start () {
	
		target = GameObject.FindGameObjectWithTag("Leader");
	}


	void Update () {
	
		// Look at the Leaders position
		look = (target.transform.position - transform.position);
		transform.rotation = Quaternion.LookRotation(look);
	}
}