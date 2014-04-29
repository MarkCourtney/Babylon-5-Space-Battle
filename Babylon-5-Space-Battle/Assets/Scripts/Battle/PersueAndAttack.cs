using UnityEngine;
using System.Collections;

public class PersueAndAttack : Behaviours {

	GameObject[] targetObjects;
	GameObject targetToKill;
	public GameObject laserShort;
	Vector3 target;
	float smoothRate, speed;
	TimeKeeper tk;
	GameObject timeHolder;


	Vector3 globalUp, accelUp, bankUp, tempUp;


	int count;
	float time;
	
	
	void Start () {

		time = 0;
		targetObjects = GameObject.FindGameObjectsWithTag("Target");
		targetToKill = targetObjects[Random.Range(0, targetObjects.Length)];

		velocity = Vector3.zero;

		maxForce = 10;
		maxSpeed = 50;
		mass = 0.1f;

		timeHolder = GameObject.FindGameObjectWithTag("Time");
		tk = timeHolder.GetComponent<TimeKeeper>();
	}

	
	
	void Update () {


		// If within a distance of 100 then shoot lasers at the target
		if(Vector3.Distance(transform.position, targetToKill.transform.position) < 100 && tk.TotalTime < 65)
		{
			if(count < 5)
			{
				Instantiate(laserShort, transform.position + new Vector3(0.5f, 0.4f, 4.5f), transform.rotation);
				Instantiate(laserShort, transform.position + new Vector3(-0.5f, 0.4f, 4.5f), transform.rotation);
				count++;
			}

			if(count > 4)
			{
				time += Time.deltaTime;
				
				if(time > 4)
				{
					count = 0;
					time = 0.0f;
				}
			}
		}



		Vector3 acceleration = Arrive(targetToKill.transform.position) / mass;
		
		velocity = velocity + acceleration * Time.deltaTime;
		
		if (velocity.magnitude > maxSpeed)
		{
			velocity = Vector3.Normalize(velocity) * maxSpeed;
		}
		
		transform.position += velocity * Time.deltaTime;


		// Apply banking to the ship
		globalUp = new Vector3(0, 0.2f, 0);
		accelUp = acceleration * 0.02f;
		bankUp = accelUp + globalUp;
		smoothRate = Time.deltaTime * 3.0f;
		tempUp = transform.up;
		Utilities.BlendIntoAccumulator(smoothRate, bankUp, ref tempUp);
		
		speed = velocity.magnitude;
		if (speed > 0.0001f)
		{
			transform.forward = velocity;
			transform.forward.Normalize();
			transform.LookAt(transform.position + transform.forward, tempUp);
			
			velocity *= 0.99f;
		}
	}
}
