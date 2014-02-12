using UnityEngine;
using System.Collections;

public class Seperation : MonoBehaviour {


	GameObject[] neighbourObjects;
	int i;
	Vector3 dist, velocity;
	float maxSpeed;
	Vector3 force;


	void Start () {
	
		force = Vector3.zero;
		i = 0;
		maxSpeed = 50;
		//neighbourObjects = FindObjectsOfType(typeof(GameObject)) as GameObject;
	}


	Vector3 SeperationMethod()
	{
		Collider[] closeColliders = Physics.OverlapSphere(transform.position, 50);

		foreach(Collider close in closeColliders)
		{
			print ("YES");
			dist = transform.position - close.transform.position;
			float distLen = dist.magnitude;
			dist.Normalize();
			force += dist / distLen;

			close.transform.position += force * Time.deltaTime;
		}

		return force;
	}

	void Update () {

		Vector3 acceleration = SeperationMethod() / 1;
		
		velocity = velocity + acceleration * Time.deltaTime;
		
		if (velocity.magnitude > maxSpeed)
		{
			velocity = Vector3.Normalize(velocity) * maxSpeed;
		}
		
		
		transform.position += velocity * Time.deltaTime;
		
		transform.rotation = Quaternion.LookRotation(velocity);
	}
}
