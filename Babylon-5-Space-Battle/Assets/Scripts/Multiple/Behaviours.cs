using UnityEngine;
using System.Collections;

public class Behaviours : MonoBehaviour {

	public Vector3 offSet;
	protected Vector3 targetOffSet, desiredVelocity, steering, velocity, desired, force, acceleration;
	protected float distance, slowingDistance, rampedSpeed, clippedSpeed, maxSpeed, maxForce, lookAhead, dist, mass;

	// Seek in a desired direction
	public Vector3 Seek(Vector3 target)
	{
		desired = target - transform.position;
		desired.Normalize();
		desired *= maxSpeed;
		
		force = desired - velocity;
		
		if (force.magnitude > maxForce)
		{
			return Vector3.Normalize(force) * maxForce;
		}
		
		return force;
	}



	// Seek and arrive to a specific position
	public Vector3 Arrive(Vector3 target)
	{
		targetOffSet = target - transform.position;
		
		distance = targetOffSet.magnitude;
		
		rampedSpeed = maxSpeed * (distance / slowingDistance);
		
		clippedSpeed = Mathf.Min(maxSpeed, rampedSpeed);
		
		desired = (clippedSpeed / distance) * targetOffSet;
		
		return (desired - velocity);
	}


	public Vector3 OffSetArrive(Vector3 target, Transform leader) {
		
		target = offSet + leader.position;
		dist = (target - transform.position).magnitude;
		lookAhead = dist / maxSpeed;
		target = target + (lookAhead * leader.forward);
		return Arrive(target);
	}
}
