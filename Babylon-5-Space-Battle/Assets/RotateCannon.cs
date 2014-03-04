using UnityEngine;
using System.Collections;

public class RotateCannon : MonoBehaviour {

	public GameObject target;
	Quaternion currentRot, targetRot;
	Vector3 look, currentRotVec, targetRotVec;

	void Start () {
	
	}

	
	void Update () {
	
		//look = new Vector3(transform.position.x - target.transform.position.x, transform.position.y - target.transform.position.y, transform.position.z - target.transform.position.z);
		look = transform.position - target.transform.position;

//		currentRot = transform.rotation;
//		currentRotVec = new Vector3(currentRot.eulerAngles.x, 0, currentRot.eulerAngles.z);
//
//		targetRot = Quaternion.LookRotation(look);
//		targetRotVec = new Vector3(targetRot.eulerAngles.x, 0, 0);

		//transform.rotation = Quaternion.Slerp(Quaternion.Euler(currentRotVec), Quaternion.Euler(look), Time.deltaTime);
		transform.LookAt(-target.transform.position);


		Quaternion newRotation = Quaternion.LookRotation(transform.position - target.transform.position, Vector3.forward);
		
		newRotation.x = 0.0f;
		
		newRotation.y = 0.0f;
		
		//transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime);
	}
}