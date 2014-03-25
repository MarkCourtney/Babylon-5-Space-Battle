using UnityEngine;
using System.Collections;

public class RotateCannon : TimeKeeper {

	public GameObject target;
	Quaternion currentRot, targetRot;
	Vector3 look, currentRotVec, targetRotVec, randomVector;
	float dist;
	float targetY, prevY;
	float height;
	Ray ray;
	RaycastHit hit;
	public float shootLaserTime;


	GameObject timeHolder;
	TimeKeeper tk;

	void Start()
	{
		randomVector = new Vector3(Random.Range(0,0.04f), Random.Range(0,0.04f), Random.Range(0,0.04f));

		timeHolder = GameObject.FindGameObjectWithTag("Time");
		tk = timeHolder.GetComponent<TimeKeeper>();
	}
	
	void Update () {
	
		Debug.DrawRay(transform.position + new Vector3(-0.75f, 0.5f, 0), (transform.forward + randomVector) * 500, Color.green);

		if(tk.TotalTime > shootLaserTime && tk.TotalTime < shootLaserTime + 5)
		{	
			ray = new Ray(transform.position + new Vector3(-0.75f, 0.5f, 0), transform.forward + randomVector);

			if(Physics.Raycast(ray, out hit, 500))
			{
				
			}
		}

		transform.LookAt(target.transform.position);
	}
}