using UnityEngine;
using System.Collections;

public class Wander : MonoBehaviour {

	float maxVelocity;
	Vector3 velocity, desiredVelocity, steering, targetLocation;
	float wanderDistance, wanderRadius;

	// Use this for initialization
	void Start () {

		maxVelocity = 0.1f;
		velocity = Vector3.zero;
		steering = Vector3.zero;
		targetLocation = Vector3.zero;
		wanderDistance = 10;
		wanderRadius = 2.5f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
