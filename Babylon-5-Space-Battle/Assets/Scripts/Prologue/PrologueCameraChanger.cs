using UnityEngine;
using System.Collections;

public class PrologueCameraChanger : MonoBehaviour {

	public Camera followCam, stationaryCam;
	float time;
	Vector3[] cameraPos = {new Vector3(-30.9f, 66.6f, -350), new Vector3(0,20,-150)};


	FadeInOut fIO;
	FollowLeader fL;
	FollowLeaderAndMove fLAM;

	void Start () {
 	
		fIO = GetComponent<FadeInOut>();
		fL = GetComponent<FollowLeader>();
		fLAM = GetComponent<FollowLeaderAndMove>();

		turnOnFollowCamera();

		time = 0;
	}


	void turnOnStationaryCamera()
	{
		stationaryCam.enabled = true;
		followCam.enabled = false;
	}

	void turnOnFollowCamera()
	{
		stationaryCam.enabled = false;
		followCam.enabled = true;
	}

	
	// Update is called once per frame
	void Update () {
	
		time += Time.deltaTime;

		if(time > 4)
		{
			turnOnStationaryCamera();
			stationaryCam.transform.position = cameraPos[0];
		}
		if(time > 10)
		{
			stationaryCam.transform.position = cameraPos[1];
		}
	}
}

