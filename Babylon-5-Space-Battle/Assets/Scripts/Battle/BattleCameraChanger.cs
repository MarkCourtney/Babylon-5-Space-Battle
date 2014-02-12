using UnityEngine;
using System.Collections;


public class BattleCameraChanger : TimeKeeper {

	// Setting bits
	// int mask = 1;
	// for(i = 0; i < set; i++)
	// mask = 1 << 5			// Left shift 5 bytes from 1

	public Camera followCam;
	public Camera[] stationaryCams;
	public Camera stationaryCam;
	Vector3[] cameraPos = {new Vector3(10f, 7f, 0), new Vector3(0, 0, 400), new Vector3(169, -20, 660)};
	Vector3[] cameraRot = {Vector3.zero, new Vector3(0, 0, 335), new Vector3(340, -20, 660)};
	public GameObject leader;
	GameObject timeHolder;

	FadeInOut fIO;
	FollowEnemy fE;
	FollowLeader fL;

	TimeKeeper tk;

	bool changeC;


	void Start () {

		timeHolder = GameObject.FindGameObjectWithTag("Time");
		tk = timeHolder.GetComponent<TimeKeeper>();

		foreach(Camera c in stationaryCams)
		{
			c.gameObject.SetActive(false);
		}
		followCam.gameObject.SetActive(false);
		stationaryCams[0].gameObject.SetActive(true);

		changeC = false;
	}


	void turnOnStationaryCamera()
	{
		stationaryCam.gameObject.SetActive(true);
		followCam.gameObject.SetActive(false);
	}
	
	void turnOnFollowCamera(int currentCamera)
	{
		stationaryCams[currentCamera].enabled = false;
		followCam.enabled = true;
	}


	void changeCam(int currentCamera)
	{
		Destroy(stationaryCams[currentCamera-1].gameObject);
		stationaryCams[currentCamera].gameObject.SetActive(true);
//		Destroy(stationaryCams[i - 1]);
//		stationaryCamFL.enabled = false;
//		stationaryCamFE.enabled = true;
//		turnOnStationaryCamera();
//		stationaryCam.transform.position = cameraPos[1];
//		stationaryCam.transform.rotation = Quaternion.Euler(0, 0, 330);
//		stationaryCam.fieldOfView = 50f;
	}


	void changeCameraTransform(Vector3 pos, Vector3 rot)
	{
		stationaryCam.transform.position = pos;
		stationaryCam.transform.rotation = Quaternion.Euler(rot);
	}

	void Update () {
	
		if(tk.Timer > 10 && changeC)
		{	
			turnOnStationaryCamera();
			changeCameraTransform(cameraPos[2], cameraRot[2]);
			changeC = true;
		}
		else if(tk.Timer > 7.5f && !changeC)
		{
			turnOnStationaryCamera();
			changeCameraTransform(cameraPos[1], cameraRot[1]);
			changeC = true;
		}
	}
}
