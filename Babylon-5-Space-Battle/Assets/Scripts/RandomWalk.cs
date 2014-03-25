using UnityEngine;
using System.Collections;

public class RandomWalk : MonoBehaviour {

	Vector3 randomWalkTarget;
	float maxSpeed, maxForce;
	Vector3 velocity;
	public Vector3 startBound, endBound;

	// Use this for initialization
	void Start () {
	
		maxSpeed = 50;
		maxForce = 10;

		randomWalkTarget.x = returnRandomPosition().x;
		randomWalkTarget.y = returnRandomPosition().y;
		randomWalkTarget.z = returnRandomPosition().z;
	}


	Vector3 seek(Vector3 target)
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

	Vector3 returnRandomPosition()
	{
		int x, y, z;

		x = (int) Random.Range(startBound.x, endBound.x);
		y = (int) Random.Range(startBound.y, endBound.y);
		z = (int) Random.Range(startBound.z, endBound.z);

		return new Vector3(x,y,z);;
	}

	Vector3 randomWalk()
	{
		float dist = (transform.position - randomWalkTarget).magnitude;

		if (dist < 50)
		{
			randomWalkTarget.x = returnRandomPosition().x;
			randomWalkTarget.y = returnRandomPosition().y;
			randomWalkTarget.z = returnRandomPosition().z;
		}
		return seek(randomWalkTarget);
	}

	
	// Update is called once per frame
	void Update () {

		Vector3 acceleration = randomWalk();
		
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
