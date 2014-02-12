using UnityEngine;
using System.Collections;

public class Arrival : MonoBehaviour {

	Vector3 target;
	float maxForce, maxSpeed, mass;
	Vector3 velocity, desiredVelocity, steering;
	float distance, rampedSpeed, clippedSpeed, slowingDistance;
	Vector3 desired, acceleration, targetOffSet;

	
	void Start () {
		
		//target = GameObject.FindGameObjectWithTag("Target");
		target = new Vector3(0,0,100);
		maxForce = 20;
		maxSpeed = 50;
		mass = .1f;

		targetOffSet = new Vector3(5,5,5);
		slowingDistance = 50f;
	}


	Vector3 Arrive()
	{
		targetOffSet = target - transform.position;

		distance = targetOffSet.magnitude;

		rampedSpeed = maxSpeed * (distance / slowingDistance);

		clippedSpeed = Mathf.Min(maxSpeed, rampedSpeed);

		desired = (clippedSpeed / distance) * targetOffSet;

		return (desired - velocity);
	}


	void Update () {

		
		acceleration = Arrive() / mass;
		velocity = velocity + acceleration * Time.deltaTime;

		if (velocity.magnitude > maxSpeed)
		{
			velocity = Vector3.Normalize(velocity) * maxSpeed;
		}

		transform.position += velocity * Time.deltaTime;
	}
}
