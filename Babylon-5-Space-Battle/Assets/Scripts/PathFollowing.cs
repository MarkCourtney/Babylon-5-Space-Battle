using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PathFollowing : TimeKeeper {

	//new Vector3(50,-130,1200), new Vector3(130,-60,1100)};
	//Vector3[] pathPositions = {new Vector3(50, 0, 500), new Vector3(0,-100,700), new Vector3(50,-130,1200), new Vector3(130,-60,1100)};
	//new Vector3(240, 10, 510)
	Vector3[] pathPositions = {new Vector3(245, 10, 510), new Vector3(200,-160,685), new Vector3(-110, -90, 1280), new Vector3(-100, 40, 1400), new Vector3(-100, 40, 1150), new Vector3(20, -10, 1050), new Vector3(123, 0, 890)};
	GameObject target;
	float maxForce, maxSpeed, mass;
	Vector3 desiredVelocity, steering, acceleration;
	Vector3 velocity;
	int currentPathPos;
	public GameObject laserShort, laserLong;
	float time;
	int count;
	TimeKeeper tk;
	GameObject timeHolder;
	bool shootLaser;

	void Start () {

		maxForce = 10;
		maxSpeed = 50;
		mass = .1f;
		currentPathPos = 0;

		timeHolder = GameObject.FindGameObjectWithTag("Time");
		tk = timeHolder.GetComponent<TimeKeeper>();

		shootLaser = false;
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




	Vector3 ObstacleAvoidance()
	{
		Vector3 force = Vector3.zero;
		//makeFeelers();
		List<GameObject> tagged = new List<GameObject>();
		float minBoxLength = 100.0f;
		float boxLength = minBoxLength + ((velocity.magnitude / maxSpeed) * minBoxLength * 2.0f);
		
		if (float.IsNaN(boxLength))
		{
			System.Console.WriteLine("NAN");
		}
		// Matt Bucklands Obstacle avoidance
		// First tag obstacles in range
		GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Enemy");
	
		if (obstacles.Length == 0)
		{
			return Vector3.zero;
		}
		foreach (GameObject obstacle in obstacles)
		{
			Vector3 toCentre = transform.position - obstacle.transform.position;

			float dist = toCentre.magnitude;
			if (dist < 200)
			{
				tagged.Add(obstacle);
			}
		}


		
		float distToClosestIP = float.MaxValue;
		GameObject closestIntersectingObstacle = null;
		Vector3 localPosOfClosestObstacle = Vector3.zero;
		Vector3 intersection = Vector3.zero;

		print (tagged.Count);
		foreach (GameObject o in tagged)
		{
			Vector3 localPos = transform.InverseTransformPoint(o.transform.position);
			
			// If the local position has a positive Z value then it must lay
			// behind the agent. (in which case it can be ignored)
			if (localPos.z >= 0)
			{
				// If the distance from the x axis to the object's position is less
				// than its radius + half the width of the detection box then there
				// is a potential intersection.
				
				float obstacleRadius = o.transform.localScale.x;
				float expandedRadius = 10 + obstacleRadius;
				if ((Mathf.Abs(localPos.y) < expandedRadius) && (Mathf.Abs(localPos.x) < expandedRadius))
				{
					// Now to do a ray/sphere intersection test. The center of the				
					// Create a temp Entity to hold the sphere in local space
					Sphere tempSphere = new Sphere(expandedRadius, localPos);
					
					// Create a ray
					Ray ray = new Ray(new Vector3(0, 0, 0), Vector3.forward);
					
					// Find the point of intersection
					if (tempSphere.closestRayIntersects(ray, Vector3.zero, ref intersection) == false)
					{
						return Vector3.zero;
					}
					
					// Now see if its the closest, there may be other intersecting spheres
					float dist = intersection.magnitude;
					if (dist < distToClosestIP)
					{
						dist = distToClosestIP;
						closestIntersectingObstacle = o;
						localPosOfClosestObstacle = localPos;
					}
				}

			}
			if (closestIntersectingObstacle != null)
			{
				// Now calculate the force
				// Calculate Z Axis braking  force
				float multiplier = 200 * (1.0f + (boxLength - localPosOfClosestObstacle.z) / boxLength);
				
				//calculate the lateral force
				float obstacleRadius = closestIntersectingObstacle.GetComponent<Renderer>().bounds.extents.magnitude;
				float expandedRadius = 10 + obstacleRadius;
				force.x = (expandedRadius - Mathf.Abs(localPosOfClosestObstacle.x)) * multiplier;
				force.y = (expandedRadius - -Mathf.Abs(localPosOfClosestObstacle.y)) * multiplier;
				
				if (localPosOfClosestObstacle.x > 0)
				{
					force.x = -force.x;
				}
				
				if (localPosOfClosestObstacle.y > 0)
				{
					force.y = -force.y;
				}

				//apply a braking force proportional to the obstacle's distance from
				//the vehicle.
				const float brakingWeight = 40.0f;
				force.z = (obstacleRadius -
				           localPosOfClosestObstacle.z) *
					brakingWeight;
				
				//finally, convert the steering vector from local to world space
				force = transform.TransformPoint(force);
			}
		}

		
		return force;
	}




	void Update () {
	
		time += Time.deltaTime;

		if((tk.TotalTime > 25 && tk.TotalTime < 29) || tk.TotalTime > 41 && tk.TotalTime < 44 || tk.TotalTime > 48 && tk.TotalTime < 51)
		{
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



		if(tk.TotalTime > 26 && !shootLaser)
		{
			Instantiate(laserLong, transform.position + new Vector3(0, 0, 5), transform.rotation);
			shootLaser = true;
		}

		checkClose();

//
//		acceleration = ObstacleAvoidance() / mass;
//
//		if(acceleration == Vector3.zero)
//		{
//			acceleration = seek() / mass;
//		}

		Vector3 maxAhead = transform.forward * 100;
		RaycastHit hit;
		
		Vector3 hitNormal = Vector3.zero;

		Debug.DrawRay(transform.position, transform.forward * 100, Color.red);

		if(Physics.Raycast(transform.position, transform.forward, out hit, 150.0f))
		{
			if(!shootLaser)
			{
				Instantiate(laserLong, transform.position + new Vector3(0, 0, 5), transform.rotation);
				shootLaser = true;
			}
			Debug.DrawRay(transform.position, hit.point, Color.red);

			if(hit.transform != transform)
			{
				hitNormal += hit.normal * 10;
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
