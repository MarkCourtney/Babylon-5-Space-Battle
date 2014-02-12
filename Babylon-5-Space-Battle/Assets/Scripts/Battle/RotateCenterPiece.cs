using UnityEngine;
using System.Collections;

public class RotateCenterPiece : MonoBehaviour {

	float rotSpeed;	

	void Start() {

		rotSpeed = Random.Range(1, 5);
	}

	void Update () {
	
		transform.RotateAround(transform.position, transform.right, rotSpeed);
	}
}
