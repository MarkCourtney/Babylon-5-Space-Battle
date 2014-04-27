using UnityEngine;
using System.Collections;

public class TurnOffPortal : MonoBehaviour {

	float timer;

	void Update () {
	
		timer += Time.deltaTime;

		// Stop the portal emitting after 4 seconds
		if(timer > 4)
		{
			gameObject.particleEmitter.emit = false;
		}
		// Destroy the object after 13.5 seconds
		if(timer > 13.5f)
		{
			Destroy(gameObject);
		}
	}
}