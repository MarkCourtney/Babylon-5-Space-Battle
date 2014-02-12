using UnityEngine;
using System.Collections;

public class PersueAndEvade : MonoBehaviour {

	GameObject targetObject;
	Vector3 target, velocity, desiredVelocity, steering;
	float distance, maxSpeed, lookAhead, maxForce, mass;


	void Start () {
		
		targetObject = GameObject.FindGameObjectWithTag("Target");

		velocity = Vector3.zero;

		steering = Vector3.zero;
		maxForce = 10;
		maxSpeed = 50;
		mass = 0.1f;
	}
	
	Vector3 Seek(Vector3 target)
	{
		Vector3 desired = target - transform.position;
		desired.Normalize();
		desired *= maxSpeed;
		
		Vector3 force = desired - velocity;
		
		if (force.magnitude > maxForce)
		{
			return Vector3.Normalize(force) * maxForce;
		}

		return force;
	}



	Vector3 Arrive()
	{	
		distance = (targetObject.transform.position - transform.position).magnitude;
		lookAhead = distance / maxSpeed;
		target = targetObject.transform.position + (lookAhead * targetObject.transform.forward);
		return Seek(target);
	}

	
	void Update () {

		//Debug.DrawRay(transform.position, Arrive(), Color.blue);


		Vector3 acceleration = Arrive() / mass;
		
		velocity = velocity + acceleration * Time.deltaTime;
		
		if (velocity.magnitude > maxSpeed)
		{
			velocity = Vector3.Normalize(velocity) * maxSpeed;
		}
		
		transform.position += velocity * Time.deltaTime;
		
		transform.rotation = Quaternion.LookRotation(velocity);

	}
}
