using UnityEngine;
using System.Collections;

public class OffSetPursuit : Behaviours {

	GameObject leader;

	Vector3 globalUp, accelUp, bankUp, tempUp;

	float timeHit, time, smoothRate, speed;
	int count;

	public LaserShot laserShort;
	public bool stayFollowing;
	
	TimeKeeper tk;
	GameObject timeHolder;
	
	PersueAndAttack pAA;

	
	
	void Start () {

		pAA = GetComponent<PersueAndAttack>();

		leader = GameObject.FindGameObjectWithTag("Leader");
		maxSpeed = 50f;
		mass = .1f;
		slowingDistance = 20f;

		timeHolder = GameObject.FindGameObjectWithTag("Time");
		tk = timeHolder.GetComponent<TimeKeeper>();
	}

	
	void Update () {

		time += Time.deltaTime;

		// Shot double lasers between the given time deltas
		if((tk.TotalTime > 25 && tk.TotalTime < 29) || tk.TotalTime > 42 && tk.TotalTime < 44 || tk.TotalTime > 57 && tk.TotalTime < 62 || tk.TotalTime > 69 && tk.TotalTime < 72)
		{
			if(time > timeHit && count < 15)
			{
				Instantiate(laserShort, transform.position + new Vector3(0.5f, 0.4f, 4.5f), transform.rotation);
				Instantiate(laserShort, transform.position + new Vector3(-0.5f, 0.4f, 4.5f), transform.rotation);
				time = 0;
				count++;
				timeHit = Random.Range(0.5f, 1);
			}
			else if(time > 4f)
			{
				count = 0;
			}
		}


		// If stayFollowing boolean is true then remove this script
		// And turn on the PersurAndAttack script
		if(tk.TotalTime > 42 && !stayFollowing)
		{
			pAA.enabled = true;
			Destroy(gameObject.GetComponent("OffSetPursuit"));
		}

//		if(tk.TotalTime > 39.5f && tk.TotalTime < 44)
//		{
//			if(time > 1f && count < 15)
//			{
//				Instantiate(laserShort, transform.position + new Vector3(0.5f, 0.4f, 4.5f), transform.rotation);
//				Instantiate(laserShort, transform.position + new Vector3(-0.5f, 0.4f, 4.5f), transform.rotation);
//				time = 0;
//				count++;
//			}
//			else if(time > 4f)
//			{
//				count = 0;
//			}
//		}


		Vector3 acceleration = OffSetArrive(offSet, leader.transform) / mass;
		
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
