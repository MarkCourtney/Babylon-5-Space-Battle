using UnityEngine;
using System.Collections;

//http://vimeo.com/9304844

public class CollisionAvoidance : MonoBehaviour {

	GameObject target, enemy;
	Vector3 ahead, halfAhead;
	float maxForce, maxSpeed, mass;
	Vector3 velocity, desiredVelocity, steering;

	Vector3 avoidForce;

	Vector3 slope, maxAhead;
	RaycastHit hit;


	void Start () {

		target = GameObject.FindGameObjectWithTag("Target");
		enemy = GameObject.FindGameObjectWithTag("Enemy");

		maxForce = 10;
		maxSpeed = 50;
		mass = .1f;
		maxAhead = Vector3.forward * 10;

		//slope = Mathf. new Vector3(transform.position - target.transform.position;

//		The slope of the line is s = (Ay - By)/(Ax - Bx).
//
//		If -h/2 <= s * w/2 <= h/2 then the line intersects:
//		The right edge if Ax > Bx
//		The left edge if Ax < Bx.
//		If -w/2 <= (h/2)/s <= w/2 then the line intersects:
//		The top edge if Ay > By
//		The bottom edge if Ay < By.
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


	void Update () {
	

		maxAhead = transform.forward * 50;


		Vector3 hitNormal = Vector3.zero;

		if(Physics.Raycast(transform.position, velocity, out hit, 50.0f))
		{
			Debug.DrawRay(transform.position, hit.point, Color.red);
			print (hit.transform);
			if(hit.transform != transform)
			{
				hitNormal += hit.normal * 50;
			}
			
		}

		Vector3 acceleration = (Seek(target.transform.position) + hitNormal) / mass;
		
		velocity = velocity + acceleration * Time.deltaTime;
		
		if (velocity.magnitude > maxSpeed)
		{
			velocity = Vector3.Normalize(velocity) * maxSpeed;
		}
		
		transform.position += velocity * Time.deltaTime;
		
		Quaternion a = Quaternion.LookRotation(velocity);
		transform.rotation = Quaternion.Slerp(transform.rotation, a, Time.smoothDeltaTime);
	}
}
