using UnityEngine;
using System.Collections;

public class PersueAndAttack : MonoBehaviour {

	GameObject[] targetObjects;
	GameObject targetToKill;
	public GameObject laserShort;
	Vector3 target, velocity, desiredVelocity;
	float distance, maxSpeed, lookAhead, maxForce, mass;
	TimeKeeper tk;
	GameObject timeHolder;


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
	
	
	
	Vector3 Arrive()
	{	
		distance = (targetToKill.transform.position - transform.position).magnitude;
		lookAhead = distance / maxSpeed;
		target = targetToKill.transform.position + (lookAhead * targetToKill.transform.forward);
		return Seek(target);
	}
	
	
	void Update () {


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



		Vector3 acceleration = Arrive() / mass;
		
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

			velocity *= 0.99f;
		}
		
	}
}
