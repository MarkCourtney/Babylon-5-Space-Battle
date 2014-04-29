using UnityEngine;
using System.Collections;

public class PrologueOffSetPursuit : Behaviours {

	GameObject leader;
	float timeHit, time;
	
	int count;

	void Start () {
	
		leader = GameObject.FindGameObjectWithTag("Leader");
		maxSpeed = 50f;
		mass = .1f;
		slowingDistance = 20f;
	}

	

	// Update is called once per frame
	void Update () {
	
		time += Time.deltaTime;

		acceleration = OffSetArrive(Vector3.zero, leader.transform) / mass;
		
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
	}
}
