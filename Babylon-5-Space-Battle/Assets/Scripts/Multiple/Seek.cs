using UnityEngine;
using System.Collections;

public class Seek : MonoBehaviour {

	Vector3 desired;
	Vector3 force;

	// Seek in a desired direction
	public Vector3 seek(Vector3 target, Vector3 position, Vector3 velocity, float maxSpeed, float maxForce)
	{
		desired = target - position;
		desired.Normalize();
		desired *= maxSpeed;
		
		force = desired - velocity;
		
		if (force.magnitude > maxForce)
		{
			return Vector3.Normalize(force) * maxForce;
		}
		
		return force;
	}
}
