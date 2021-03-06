﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArriveToFinalPoint : Behaviours {

	Vector3[] points = {new Vector3(-112, 55, -345),
						new Vector3(-51, 55, -428),
						new Vector3(0, 0, -326),
						new Vector3(0, 0, -250),
						new Vector3(0, 0, 165)};

	Vector3 currentPoint;

	float time;
	int i;

	void Start () {



		maxForce = 10f;
		maxSpeed = 50f;
		mass = .1f;
		
		targetOffSet = new Vector3(5,5,5);
		slowingDistance = 20f;
		i = 0;
	}

	
	
	Vector3 SeekThenArrive()
	{
		currentPoint = points[i];

		if(Vector3.Distance(transform.position, currentPoint) < 20)
		{
			i++;
			if(i > 3 && time < 16.0f)
			{
				i = 3;
			}
			else if(time >= 16.0f)
			{
				i = 4;
			}
		}

		if(i != 3)
		{
			return Seek(currentPoint);
		}
		else
		{
			return Arrive(currentPoint);
		}
	}



	void Update () {

		time += Time.deltaTime;

		acceleration = SeekThenArrive() / mass;

		velocity = velocity + acceleration * Time.deltaTime;
		
		if(velocity.magnitude > maxSpeed)
		{
			velocity = Vector3.Normalize(velocity) * maxSpeed;
		}


//		float bank = 30f;
//		Quaternion r = Quaternion.LookRotation(velocity) * Quaternion.AngleAxis(bank, velocity);
//		transform.rotation = Quaternion.RotateTowards(transform.rotation, r, Time.deltaTime * 10);
//		transform.Translate(new Vector3(0,0,2) * Time.deltaTime);

		transform.position += velocity * Time.deltaTime;

		//transform.rotation = Quaternion.LookRotation(velocity);
		// the length of this global-upward-pointing vector controls the vehicle's
		// tendency to right itself as it is rolled over from turning acceleration
		Vector3 globalUp = new Vector3(0, 0.2f, 0);
		// acceleration points toward the center of local path curvature, the
		// length determines how much the vehicle will roll while turning
		Vector3 accelUp = acceleration * 0.05f;
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
		}
	}
}
