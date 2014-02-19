using UnityEngine;
using System.Collections;

public class EnemySeek : MonoBehaviour {
	

	GameObject target;
	float maxForce, maxSpeed, mass;
	Vector3 desiredVelocity, steering, acceleration;
	Vector3 velocity;
	int currentPathPos;
	Vector3 firstPoint;

	GameObject timeHolder;
	TimeKeeper tk;

	bool hit;
	
	void Start () {

		timeHolder = GameObject.FindGameObjectWithTag("Time");
		tk = timeHolder.GetComponent<TimeKeeper>();

		target = GameObject.FindGameObjectWithTag("Leader");

		maxForce = 10;
		maxSpeed = 40;
		mass = .1f;
		currentPathPos = 0;
		firstPoint = transform.position - Vector3.forward * 100;
		hit = false;
	}


	Vector3 seek(Vector3 target)
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

	void Update () {
	
		if(tk.TotalTime > 24)
		{
			if(!hit)
			{
				acceleration = seek(firstPoint) / mass;

				if(Vector3.Distance(transform.position, firstPoint) < 3)
				{
					hit = true;
				}
			}
			else
			{
				print ("B");
				acceleration = seek(target.transform.position) / mass;
			}
		}

		//acceleration = seek() / mass;
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
		Vector3 accelUp = acceleration * 0.025f;
		// combined banking, sum of UP due to turning and global UP
		Vector3 bankUp = accelUp + globalUp;
		// blend bankUp into vehicle's UP basis vector
		float smoothRate = Time.deltaTime * 5.0f;
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
