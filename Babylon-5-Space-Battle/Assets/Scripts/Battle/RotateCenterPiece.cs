using UnityEngine;
using System.Collections;

public class RotateCenterPiece : MonoBehaviour {

	public float rotSpeed;	
	
	void Update () {
	
		// Rotate the core of the Omega-X
		transform.RotateAround(transform.position, transform.right, rotSpeed);
	}
}
