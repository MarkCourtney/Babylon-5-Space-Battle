using UnityEngine;
using System.Collections;

public class RotateCannon : TimeKeeper {

	public GameObject target;
	Quaternion currentRot, targetRot;
	Vector3 look, currentRotVec, targetRotVec;
	float dist;
	float targetY, prevY;
	float height;
	Ray ray;
	RaycastHit hit;
	public float shootLaserTime;
	public GameObject laserLong;
	bool shot;


	GameObject timeHolder;
	TimeKeeper tk;

	void Start()
	{
		timeHolder = GameObject.FindGameObjectWithTag("Time");
		tk = timeHolder.GetComponent<TimeKeeper>();

		shot = false;
	}
	
	void Update () {
	
		//Debug.DrawRay(transform.position + new Vector3(-0.75f, 0.5f, 0), (transform.forward + randomVector) * 500, Color.green);


		if(tk.TotalTime > shootLaserTime && !shot)
		{
			Instantiate(laserLong, transform.position + new Vector3(-0.75f, 0.5f, 0), transform.rotation);
			shot = true;
		}

//		ray = new Ray(transform.position + new Vector3(-0.75f, 0.5f, 0), transform.forward + randomVector);
//		
//		if(Physics.Raycast(ray, out hit, 10000))
//		{
//			//print (hit.transform.name);
//		}

		transform.LookAt(target.transform.position);
	}
}