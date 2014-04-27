using UnityEngine;
using System.Collections;

public class RotateCenterPiece : MonoBehaviour {

	public float rotSpeed;	
	
	void Update () {
	
		transform.RotateAround(transform.position, transform.right, rotSpeed);
	}
}
