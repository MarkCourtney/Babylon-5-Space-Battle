using UnityEngine;
using System.Collections;

public class RandomWalk : Behaviours {

	Vector3 randomWalkTarget, oldTarget;
	public Vector3 startBound, endBound;
	TimeKeeper tk;
	GameObject timeHolder;


	void Start () {
	
		mass = 0.175f;
		maxSpeed = 60;
		maxForce = 15;

		randomWalkTarget = returnRandomPosition();

		timeHolder = GameObject.FindGameObjectWithTag("Time");
		tk = timeHolder.GetComponent<TimeKeeper>();
	}
	

	Vector3 returnRandomPosition()
	{
		int x, y, z;
		Vector3 newTarget;

		x = (int) Random.Range(startBound.x, endBound.x);
		y = (int) Random.Range(startBound.y, endBound.y);
		z = (int) Random.Range(startBound.z, endBound.z);

		newTarget = new Vector3(x,y, z);

		return newTarget;
		
	}

	Vector3 randomWalk()
	{
		float dist = (transform.position - randomWalkTarget).magnitude;

		if (dist < 50)
		{
			oldTarget = randomWalkTarget;
			randomWalkTarget = returnRandomPosition();

			if(Vector3.Distance(oldTarget, randomWalkTarget) < 100)
			{
				randomWalkTarget = returnRandomPosition();
			}
		}
		return Seek(randomWalkTarget);
	}


	void Update () {

		// Keep the ships stationary until 22.5 seconds have passed
		// Then move forward for 3 seconds
		// After this apply the randomWalk() method
		if(tk.TotalTime > 25.5f)
		{
			Vector3 acceleration = randomWalk() / mass;
			
			velocity = velocity + acceleration * Time.deltaTime;
			
			if (velocity.magnitude > maxSpeed)
			{
				velocity = Vector3.Normalize(velocity) * maxSpeed;
			}

		}
		if(tk.TotalTime > 22.5f && tk.TotalTime < 25.5f)
		{
			velocity = Vector3.back * 30;
		}
		
		transform.position += velocity * Time.deltaTime;
		transform.up += velocity;
		
		transform.rotation = Quaternion.LookRotation(velocity);
	}
}