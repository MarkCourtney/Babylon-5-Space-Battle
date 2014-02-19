using UnityEngine;
using System.Collections;

public class Flee : MonoBehaviour {

	public GameObject enemy;
	float maxForce, maxSpeed, mass;
	Vector3 velocity, desiredVelocity, steering;
	
	
	void Start () {
		
		enemy = GameObject.FindGameObjectWithTag("Leader");
		maxForce = 10;
		maxSpeed = 50;
		mass = .1f;
	}
	
	
	Vector3 flee()
	{
		Vector3 desired = transform.position - enemy.transform.position;
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
		
		Vector3 acceleration = flee() / mass;

		velocity = velocity + acceleration * Time.deltaTime;
		
		if (velocity.magnitude > maxSpeed)
		{
			velocity = Vector3.Normalize(velocity) * maxSpeed;
		}
		
		
		transform.position += velocity * Time.deltaTime;
		
		transform.rotation = Quaternion.LookRotation(velocity);
	}
}