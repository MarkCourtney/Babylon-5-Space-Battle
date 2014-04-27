using UnityEngine;
using System.Collections;

public class FollowLeaderAndMove : MonoBehaviour {

	public GameObject target;
	Vector3 look;
	public float amount;


	void Start () {
		
		target = GameObject.FindGameObjectWithTag("Leader");
	}


	void Update () {

		// Look at the Leaders position
		look = target.transform.position - transform.position;
		transform.rotation = Quaternion.LookRotation(look);

		// Move in a certain direction a defined amount
		transform.position -= target.transform.forward * amount * Time.deltaTime;
	}
}