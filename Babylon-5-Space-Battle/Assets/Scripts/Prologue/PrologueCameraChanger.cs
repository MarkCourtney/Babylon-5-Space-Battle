using UnityEngine;
using System.Collections;

public class PrologueCameraChanger : MonoBehaviour {

	public GameObject followCam, stationaryCam;
	float time;
	Vector3[] cameraPos = {new Vector3(-30.9f, 66.6f, -350), new Vector3(0, 15,-150)};
	

	void Start () {
 	
		turnOnFollowCamera();

		time = 0;
	}


	void turnOnStationaryCamera()
	{
		followCam.SetActive(false);
		stationaryCam.SetActive(true);
	}

	void turnOnFollowCamera()
	{
		stationaryCam.SetActive(false);
		followCam.SetActive(true);
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

