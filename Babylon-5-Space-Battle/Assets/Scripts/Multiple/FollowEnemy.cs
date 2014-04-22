using UnityEngine;
using System.Collections;

public class FollowEnemy : MonoBehaviour {

	void Update () {
	
		transform.position += transform.forward;
		transform.Rotate(Vector3.forward * 0.5f);
	}
}
