using UnityEngine;
using System.Collections;

public class Wander : MonoBehaviour {

	float maxVelocity;
	Vector3 velocity, desiredVelocity, steering, targetLocation;
	float wanderDistance, wanderRadius;
	Vector3 wanderTargetPos;
	float maxSpeed;

	// Use this for initialization
	void Start () {

		maxVelocity = 10f;
		maxSpeed = 50f;
		velocity = Vector3.zero;
		steering = Vector3.zero;
		targetLocation = Vector3.zero;
		wanderDistance = 10;
		wanderRadius = 2.5f;
	}



	
	
	
	Vector3 wander()
	{
		float jitterTimeSlice = 20 * Time.deltaTime;
		
		Vector3 toAdd = UnityEngine.Random.insideUnitSphere * jitterTimeSlice;
		wanderTargetPos += toAdd;
		wanderTargetPos.Normalize();
		wanderTargetPos *= 10;
		
		Vector3 localTarget = wanderTargetPos + Vector3.forward * 20;
		Vector3 worldTarget = transform.TransformPoint(localTarget);

		return (worldTarget - transform.position);
	}



	// Update is called once per frame
	void Update () {

		Vector3 acceleration = wander();
		
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
