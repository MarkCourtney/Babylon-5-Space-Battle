using UnityEngine;
using System.Collections;

public class Seek : MonoBehaviour {

	GameObject target;
	float maxForce, maxSpeed, mass;
	Vector3 desiredVelocity, steering;
	Vector3 velocity;

	void Start () {

		target = GameObject.FindGameObjectWithTag("Target");
		maxForce = 10;
		maxSpeed = 50;
		mass = .1f;
	}


	Vector3 seek()
	{
		Vector3 desired = target.transform.position - transform.position;
		desired.Normalize();
		desired *= maxSpeed;
		
		Vector3 force = desired - velocity;

		if (force.magnitude > maxForce)
		{
			return Vector3.Normalize(force) * maxForce;
		}

		return force;
	}


	void Update () {
		
		Vector3 acceleration = seek() / mass;

		velocity = velocity + acceleration * Time.deltaTime;

		if (velocity.magnitude > maxSpeed)
		{
			velocity = Vector3.Normalize(velocity) * maxSpeed;
		}


		transform.position += velocity * Time.deltaTime;
		transform.up += velocity;

		transform.rotation = Quaternion.LookRotation(velocity);
	}
}
