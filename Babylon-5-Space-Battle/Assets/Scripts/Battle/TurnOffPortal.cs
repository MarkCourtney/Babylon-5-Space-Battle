using UnityEngine;
using System.Collections;

public class TurnOffPortal : MonoBehaviour {

	float timer;

	void Update () {
	
		timer += Time.deltaTime;

		if(timer > 4)
		{
			gameObject.particleEmitter.emit = false;
		}
		if(timer > 13.5f)
		{
			Destroy(gameObject);
		}
	}
}