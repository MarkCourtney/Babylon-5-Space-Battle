using UnityEngine;
using System.Collections;

public class TurnOnPortal : MonoBehaviour {

	float timer;


	void Update () {
	
		timer += Time.deltaTime;

		if(timer > 5)
		{
			particleEmitter.emit = true;
		}
	}
}
