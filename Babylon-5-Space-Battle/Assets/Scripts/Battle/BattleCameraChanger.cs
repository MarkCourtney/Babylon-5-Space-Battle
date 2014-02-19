using UnityEngine;
using System.Collections;


public class BattleCameraChanger : TimeKeeper {
	
	// Setting bits
	// int mask = 1;
	// for(i = 0; i < set; i++)
	// mask = 1 << 5			// Left shift 5 bytes from 1
	
	
	//new Vector3(175, -20, 650)
	public Camera followCam;
	public Camera stationaryCam;
	Vector3[] cameraPos = {new Vector3(10f, 7f, 0), new Vector3(0, 0, 420), new Vector3(186, 13, 605), new Vector3(186, 13, 605)};
	Vector3[] cameraRot = {new Vector3(0, 0, 0), new Vector3(0, 0, 335), new Vector3(10, 330, 1), new Vector3(186, 13, 605)};
	public GameObject leader;
	GameObject timeHolder;
	
	FadeInOut fIO;
	FollowEnemy sCamfE;
	FollowLeader sCamfL, fCamfL;
	FollowLeaderAndMove fCamfLAM;
	
	TimeKeeper tk;
	
	bool changeC;
	

	void Start () {

		fIO = stationaryCam.GetComponent<FadeInOut>();
		sCamfL = stationaryCam.GetComponent<FollowLeader>();
		sCamfE = stationaryCam.GetComponent<FollowEnemy>();
		fCamfL = followCam.GetComponent<FollowLeader>();
		fCamfLAM = followCam.GetComponent<FollowLeaderAndMove>();
		
		timeHolder = GameObject.FindGameObjectWithTag("Time");
		tk = timeHolder.GetComponent<TimeKeeper>();

		
		followCam.gameObject.SetActive(false);
		
		changeC = false;
	}
	
	
	void turnOnStationaryCamera()
	{
		stationaryCam.gameObject.SetActive(true);
		followCam.gameObject.SetActive(false);
	}
	
	void turnOnFollowCamera()
	{
		followCam.gameObject.SetActive(true);
		stationaryCam.gameObject.SetActive(false);
	}
	

	
	
	void changeCameraTransform(Camera cam, Vector3 pos, Vector3 rot)
	{
		cam.transform.position = pos;
		cam.transform.rotation = Quaternion.Euler(rot);
	}
	
	void Update () {

		//print (tk.Timer);
		if(tk.TotalTime > 30 && changeC)
		{	
			changeCameraTransform(stationaryCam, cameraPos[2], cameraRot[2]);
			changeC = false;
		}
		else if(tk.TotalTime > 22 && tk.TotalTime < 30 && !changeC)
		{	
			turnOnStationaryCamera();
			changeCameraTransform(stationaryCam, cameraPos[2], cameraRot[2]);
			changeC = true;
		}
		else if(tk.TotalTime > 10 && tk.TotalTime < 22 && changeC)
		{
			turnOnFollowCamera();
			fCamfL.enabled = false;
			fCamfLAM.enabled = true;
			changeCameraTransform(followCam, leader.transform.position + new Vector3(5,0,15), Vector3.zero);
			changeC = false;
		}
		else if(tk.TotalTime > 7.5f && tk.TotalTime < 10 && !changeC)
		{
			sCamfL.enabled = false;
			sCamfE.enabled = true;
			turnOnStationaryCamera();
			changeCameraTransform(stationaryCam, cameraPos[1], cameraRot[1]);
			
			changeC = true;
		}
	}
}
