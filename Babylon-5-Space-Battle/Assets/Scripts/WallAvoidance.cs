using UnityEngine;
using System.Collections;

public class WallAvoidance : MonoBehaviour {

	Vector3 target;
	float maxForce, maxSpeed, mass;
	Vector3 force;
	Vector3 desiredVelocity, steering;
	Vector3 velocity;

	Ray leftRay, forwardRay, rightRay;
	RaycastHit forwardRayHit, leftRayHit, rightRayHit;

	Vector3 additionalForce;

	void Start () {
	
		forwardRay = new Ray(transform.position, transform.forward);
		leftRay = new Ray(transform.position, transform.forward + transform.forward * 0.1f);
		rightRay = new Ray(transform.position, transform.forward + transform.forward * -0.1f);

		maxForce = 10;
		maxSpeed = 50;
		target = new Vector3(-120, 0, 0);
		force = Vector3.zero;
		velocity = Vector3.zero;
		mass = 1;
	}


	Vector3 Seek()
	{

		Vector3 desired = target - transform.position;

		desired.Normalize();
		desired *= maxSpeed;

		force = desired - velocity;


		if (force.magnitude > maxForce)
		{
			return Vector3.Normalize(force) * maxForce;
		}


		return force;
	}


	Vector3 dist;
	void Update () {
	
		if(Physics.Raycast(transform.position + Vector3.right * 5, velocity, out forwardRayHit, 100) || Physics.Raycast(transform.position - Vector3.right * 5, velocity, out forwardRayHit, 100))
		{
			if(Physics.Raycast(transform.position, transform.TransformDirection(velocity.x * 2, velocity.y, velocity.z), out forwardRayHit, 100))
			{
				print ("A");
				additionalForce = transform.TransformDirection(velocity.x * maxSpeed, velocity.y, velocity.z);

			}
			else if(Physics.Raycast(transform.position, transform.TransformDirection(-velocity.x * 2, velocity.y, velocity.z), out forwardRayHit, 100))
			{
				print ("B");
				additionalForce = transform.TransformDirection(velocity.x * maxSpeed, velocity.y, velocity.z);
			}
			else 
			{
				additionalForce += transform.up * maxSpeed * 30;
			}

			if (additionalForce.magnitude > maxForce)
			{
				additionalForce = Vector3.Normalize(additionalForce) * maxForce;
			}

		}
		else
		{
			print ("Nada");

			additionalForce = Vector3.zero;
		}

//		if(Physics.Raycast(forwardRay, velocity, out hit, 50.0f))
//		{
//			Debug.DrawRay(transform.position, hit.point, Color.red);
//			print (hit.transform);
//			if(hit.transform != transform)
//			{
//				hitNormal += hit.normal * 50;
//			}
//			
//		}

		Debug.DrawRay(transform.position, transform.forward * 100, Color.red);
		Debug.DrawRay(transform.position, transform.TransformDirection(velocity.x * 5, velocity.y, velocity.z) * 100, Color.yellow);
		Debug.DrawRay(transform.position, transform.TransformDirection(-velocity.x * 5, velocity.y, velocity.z) * 100, Color.blue);
		//Debug.DrawRay(transform.position, transform.TransformDirection(velocity.x * 2, velocity.y, velocity.z * 2), Color.green);




		Vector3 acceleration = Seek() / mass;
		
		velocity = velocity + acceleration * Time.deltaTime;
		
		velocity = velocity + additionalForce * Time.deltaTime;

		if (velocity.magnitude > maxSpeed)
		{
			velocity = Vector3.Normalize(velocity) * maxSpeed;
		}


		transform.position += velocity * Time.deltaTime;
		
		transform.rotation = Quaternion.LookRotation(velocity);
	}
}
