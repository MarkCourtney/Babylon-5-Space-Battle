using UnityEngine;
using System.Collections;

public class Arrive : MonoBehaviour {

	Vector3 targetOffSet, desiredVelocity, steering, desired, velocity;
	float distance, slowingDistance, rampedSpeed, clippedSpeed;

	// Seek and arrive to a specific position
	public Vector3 arrive(Vector3 target, Vector3 position, Vector3 velocity, float maxSpeed)
	{
		targetOffSet = target - position;

		distance = targetOffSet.magnitude;
		
		rampedSpeed = maxSpeed * (distance / slowingDistance);
		
		clippedSpeed = Mathf.Min(maxSpeed, rampedSpeed);
		
		desired = (clippedSpeed / distance) * targetOffSet;
		
		return (desired - velocity);
	}
}
