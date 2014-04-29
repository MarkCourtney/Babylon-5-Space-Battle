using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PathFollowing : Behaviours {

	public GameObject laserShort;
	GameObject target;
	GameObject timeHolder;
	TimeKeeper tk;

	RaycastHit hit;

	Vector3[] pathPositions = { new Vector3(245, 10, 510), new Vector3(200,-160,685), new Vector3(-50, -90, 1280), 
								new Vector3(-100, 40, 1400), new Vector3(-100, 40, 1225), new Vector3(20, -10, 1050), 
								new Vector3(165, 280, 825), new Vector3(165, 120, 825), new Vector3(270, 0, 825), 
								new Vector3(230, -100, 825), new Vector3(60, -30, 975),  new Vector3(60, -30, 3000)};

	Vector3 globalUp, accelUp, bankUp, tempUp;
	Vector3 hitNormal = Vector3.zero;

	float time, smoothRate, speed;
	int count, currentPathPos;

	
	void Start () {

		maxForce = 10;
		maxSpeed = 50;

		mass = .1f;
		currentPathPos = 0;

		timeHolder = GameObject.FindGameObjectWithTag("Time");
		tk = timeHolder.GetComponent<TimeKeeper>();
	}


	// Determine if the distance between the current position
	// And the path position is less than 5
	// Then change to the next path position
	void checkClose()
	{
		if(Vector3.Distance(transform.position, pathPositions[currentPathPos]) < 5)
		{
			currentPathPos++;
		}
	}

	

	void Update () {

		checkClose();

		// Shot double lasers between the given time deltas
		if((tk.TotalTime > 25 && tk.TotalTime < 29) || tk.TotalTime > 42 && tk.TotalTime < 44 || tk.TotalTime > 59 && tk.TotalTime < 62 || tk.TotalTime > 69f && tk.TotalTime < 71)
		{
			time += Time.deltaTime;

			if(time > 1f && count < 15)
			{
				Instantiate(laserShort, transform.position + new Vector3(0.5f, 0.4f, 4.5f), transform.rotation);
				Instantiate(laserShort, transform.position + new Vector3(-0.5f, 0.4f, 4.5f), transform.rotation);
				time = 0;
				count++;
			}
			else if(time > 4f)
			{
				count = 0;
			}
		}





		hitNormal = Vector3.zero;
		if(Physics.Raycast(transform.position, transform.forward, out hit, 150.0f))
		{
			if(hit.transform != transform)
			{
				hitNormal += hit.normal * 15;
			}
		}

		acceleration = (Seek(pathPositions[currentPathPos]) + hitNormal) / mass;
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
