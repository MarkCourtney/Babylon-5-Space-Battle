using UnityEngine;
using System.Collections;

public class FollowLeaderAndMove : MonoBehaviour {

	GameObject target;
	Vector3 look;
	public Vector3 direction;
	public float amount;
	// Use this for initialization
	void Start () {
		
		target = GameObject.FindGameObjectWithTag("Leader");
	}
	
	// Update is called once per frame
	void Update () {
		
		look = (target.transform.position - transform.position);
		transform.rotation = Quaternion.LookRotation(look);
		
		transform.position += direction * amount * Time.deltaTime;
	}
}
