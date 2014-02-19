using UnityEngine;
using System.Collections;

public class FollowLeaderAndMove : MonoBehaviour {

	GameObject target;
	Vector3 look;
	public float amount;
	Vector3 currentPos;


	void Start () {
		
		target = GameObject.FindGameObjectWithTag("Leader");
		currentPos = target.transform.position;
	}


	void Update () {
		
		look = (target.transform.position - transform.position);
		transform.rotation = Quaternion.LookRotation(look);
		
		transform.position -= target.transform.forward * amount * Time.deltaTime;
	}
}