using UnityEngine;
using System.Collections;

public class OffSetPursuit : TimeKeeper {

	GameObject leader;
	Vector3 target, targetOffSet;
	float lookAhead, maxSpeed, maxForce;
	public Vector3 offSet;
	float dist;
	Vector3 velocity;
	float mass;
	float timeHit, time;

	float distance, slowingDistance, rampedSpeed, clippedSpeed;
	Vector3 desiredVelocity, steering;
	Vector3 desired;
	int count;
	public LaserShot laserShort;
	Seek seek;
	public bool stayFollowing;

	
	TimeKeeper tk;
	GameObject timeHolder;


	void Start () {
	
		seek = GetComponent<Seek>();
		leader = GameObject.FindGameObjectWithTag("Leader");
		maxForce = 10f;
		maxSpeed = 50f;
		mass = .1f;
		slowingDistance = 20f;

		timeHolder = GameObject.FindGameObjectWithTag("Time");
		tk = timeHolder.GetComponent<TimeKeeper>();
	}


	Vector3 Arrive(Vector3 target)
	{
		targetOffSet = target - transform.position;
		
		distance = targetOffSet.magnitude;
		
		rampedSpeed = maxSpeed * (distance / slowingDistance);
		
		clippedSpeed = Mathf.Min(maxSpeed, rampedSpeed);
		
		desired = (clippedSpeed / distance) * targetOffSet;
		
		return (desired - velocity);
	}


	Vector3 OffSetArrive() {


		target = offSet + leader.transform.position;

		dist = (target - transform.position).magnitude;
		lookAhead = dist / maxSpeed;
		target = target + (lookAhead * leader.transform.forward);
		return Arrive(target);
	}

	void Update () {

		time += Time.deltaTime;

		if((tk.TotalTime > 25 && tk.TotalTime < 29) || tk.TotalTime > 41 && tk.TotalTime < 44 || tk.TotalTime > 48 && tk.TotalTime < 51)
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

		if(tk.TotalTime > 37 && !stayFollowing)
		{
			// turn on other script
			// remove this script
			seek.enabled = true;
			Destroy(gameObject.GetComponent("OffSetPursuit"));
		}

		if(tk.TotalTime > 41 && tk.TotalTime < 44)
		{
			print ("ASD");
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


		Vector3 acceleration = OffSetArrive() / mass;
		
		velocity = velocity + acceleration * Time.deltaTime;


		if (velocity.magnitude > maxSpeed)
		{
			velocity = Vector3.Normalize(velocity) * maxSpeed;
		}
		transform.position += velocity * Time.deltaTime;


		//transform.rotation = Quaternion.LookRotation(velocity);
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
		//transform.rotation = Quaternion.LookRotation(velocity);
	}


	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.name == "Omega-X")
		{
			Destroy(gameObject);
		}
	}
}
