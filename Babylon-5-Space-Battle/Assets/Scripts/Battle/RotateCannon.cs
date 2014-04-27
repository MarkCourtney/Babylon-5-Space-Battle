using UnityEngine;
using System.Collections;

public class RotateCannon : TimeKeeper {

	public GameObject target;
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

		// Produce a long laser effect at certain timestamps
		if(tk.TotalTime > shootLaserTime && !shot)
		{
			Instantiate(laserLong, transform.position + new Vector3(-0.75f, 0.5f, 0), transform.rotation);
			shot = true;
		}

		// Rotate to look at the center WhiteStar ship
		transform.LookAt(target.transform.position);
	}
}