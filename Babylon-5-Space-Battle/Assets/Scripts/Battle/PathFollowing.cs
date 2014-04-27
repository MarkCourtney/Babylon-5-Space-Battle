using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PathFollowing : TimeKeeper {

	//new Vector3(50,-130,1200), new Vector3(130,-60,1100)};
	//Vector3[] pathPositions = {new Vector3(50, 0, 500), new Vector3(0,-100,700), new Vector3(50,-130,1200), new Vector3(130,-60,1100)};
	//new Vector3(240, 10, 510)
	Vector3[] pathPositions = {new Vector3(245, 10, 510), new Vector3(200,-160,685), new Vector3(-50, -90, 1280), new Vector3(-100, 40, 1400), new Vector3(-100, 40, 1225), new Vector3(20, -10, 1050), new Vector3(165, 280, 825), new Vector3(165, 120, 825), new Vector3(270, 0, 825), new Vector3(230, -100, 825), new Vector3(60, -30, 975),  new Vector3(60, -30, 3000)};
	GameObject target;
	float maxForce, maxSpeed, mass;
	Vector3 desiredVelocity, steering, acceleration;
	Vector3 velocity;
	int currentPathPos;
	public GameObject laserShort;
	float time;
	int count;
	TimeKeeper tk;
	GameObject timeHolder;

	void Start () {

		maxForce = 10;
		maxSpeed = 50;
		mass = .1f;
		currentPathPos = 0;

		timeHolder = GameObject.FindGameObjectWithTag("Time");
		tk = timeHolder.GetComponent<TimeKeeper>();
	}



	void checkClose()
	{
		if(Vector3.Distance(transform.position, pathPositions[currentPathPos]) < 5)
		{
			currentPathPos++;
		}
	}


	Vector3 seek()
	{
		Vector3 desired = pathPositions[currentPathPos] - transform.position;
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
	
		if(tk.TotalTime > 95)
		{
			print (tk.TotalTime);
			Application.Quit();
		}


		if((tk.TotalTime > 25 && tk.TotalTime < 29) || tk.TotalTime > 41 && tk.TotalTime < 44 || tk.TotalTime > 59 && tk.TotalTime < 62 || tk.TotalTime > 69f && tk.TotalTime < 71)
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



		checkClose();


		RaycastHit hit;
		
		Vector3 hitNormal = Vector3.zero;

		if(Physics.Raycast(transform.position, transform.forward, out hit, 150.0f))
		{
			if(hit.transform != transform)
			{
				hitNormal += hit.normal * 15;
			}
		}

		acceleration = (seek() + hitNormal) / mass;
		
		velocity = velocity + acceleration * Time.deltaTime;
		
		if (velocity.magnitude > maxSpeed)
		{
			velocity = Vector3.Normalize(velocity) * maxSpeed;
		}
		
		transform.position += velocity * Time.deltaTime;

		// the length of this global-upward-pointing vector controls the vehicle's
		// tendency to right itself as it is rolled over from turning acceleration
		Vector3 globalUp = new Vector3(0, 0.2f, 0);
		// acceleration points toward the center of local path curvature, the
		// length determines how much the vehicle will roll while turning
		Vector3 accelUp = acceleration * 0.02f;
		// combined banking, sum of UP due to turning and global UP
		Vector3 bankUp = accelUp + globalUp;
		// blend bankUp into vehicle's UP basis vector
		float smoothRate = Time.deltaTime * 3.0f;
		Vector3 tempUp = transform.up;
		Utilities.BlendIntoAccumulator(smoothRate, bankUp, ref tempUp);
		
		float speed = velocity.magnitude;
		if (speed > 0.0001f)
		{
			transform.forward = velocity;
			transform.forward.Normalize();
			transform.LookAt(transform.position + transform.forward, tempUp);
			
			velocity *= 0.99f;
		}
	}
}
