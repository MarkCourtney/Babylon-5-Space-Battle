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
	Vector3[] cameraPos = {new Vector3(10f, 7f, 0), new Vector3(0, 0, 420), new Vector3(275, -107, 540), 	new Vector3(205, 18, 580), 	new Vector3(147, -25, 1245), 	new Vector3(-40, 50, 1150), new Vector3(-400, 100, 950),	new Vector3(130, -40, 820)};
	Vector3[] cameraRot = {new Vector3(0, 0, 0), 	new Vector3(0, 0, 335), Vector3.zero,  					new Vector3(10, 330, 320), 	new Vector3(27, 227, 0), 		Vector3.zero,				new Vector3(30, 90, 0), 		Vector3.zero};
	public GameObject leader;
	GameObject timeHolder;
	
	FadeInOut fIO;
	FollowEnemy sCamfE;
	FollowLeader sCamfL, fCamfL;
	FollowLeaderAndMove sCamfLAM, fCamfLAM;
	
	TimeKeeper tk;
	
	bool changeC;
	

	void Start () {

		fIO = stationaryCam.GetComponent<FadeInOut>();
		fIO.fadeIn = true;
		sCamfL = stationaryCam.GetComponent<FollowLeader>();
		sCamfE = stationaryCam.GetComponent<FollowEnemy>();
		sCamfLAM = stationaryCam.GetComponent<FollowLeaderAndMove>();
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


		if(tk.TotalTime > 73)
		{
			turnOnFollowCamera();
			sCamfL.enabled = false;
			fCamfLAM.enabled = false;
			fCamfL.enabled = true;
			changeCameraTransform(followCam, leader.transform.position + new Vector3(15,10,15), Vector3.zero);
		}
		else if(tk.TotalTime > 59 && tk.TotalTime < 73 && changeC)
		{
			sCamfLAM.enabled = false;
			sCamfL.enabled = true;
			changeCameraTransform(stationaryCam, cameraPos[7], cameraRot[7]);
			changeC = false;
		}
		else if(tk.TotalTime > 46f && tk.TotalTime < 59 && !changeC)
		{
			sCamfLAM.target = GameObject.FindGameObjectWithTag("FollowSeek");
			sCamfLAM.enabled = true;
			sCamfL.enabled = false;
			changeCameraTransform(stationaryCam, cameraPos[6], cameraRot[6]);
			changeC = true;
		}
		else if(tk.TotalTime > 34 && tk.TotalTime < 46f && changeC)
		{
			sCamfL.enabled = true;
			changeCameraTransform(stationaryCam, cameraPos[5], cameraRot[5]);
			changeC = false;
		}
		else if(tk.TotalTime > 26 && tk.TotalTime < 34 &&  !changeC)
		{	
			changeCameraTransform(stationaryCam, cameraPos[4], cameraRot[4]);
			
			sCamfE.enabled = false;
			changeC = true;
		}
		else if(tk.TotalTime > 23 && tk.TotalTime < 26 && changeC)
		{	
			sCamfE.enabled = true;
			sCamfLAM.enabled = false;
			sCamfL.enabled = false;
			changeCameraTransform(stationaryCam, cameraPos[3], cameraRot[3]);
			changeC = false;
		}
		else if(tk.TotalTime > 15 && tk.TotalTime < 23 && !changeC)
		{	

			turnOnStationaryCamera();
			sCamfLAM.enabled = true;
			sCamfE.enabled = false;
			sCamfL.enabled = false;
			changeCameraTransform(stationaryCam, cameraPos[2], cameraRot[2]);
			changeC = true;
		}
		else if(tk.TotalTime > 10 && tk.TotalTime < 15 && changeC)
		{
			turnOnFollowCamera();
			fCamfL.enabled = false;
			fCamfLAM.enabled = true;
			changeCameraTransform(followCam, leader.transform.position + new Vector3(15,0,15), Vector3.zero);
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
