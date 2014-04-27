using UnityEngine;
using System.Collections;

public class DestroyParticleEffect : MonoBehaviour {

	float time;


	void Start () {
	
		time = 0;
	}

	void Update () {
	
		time += Time.deltaTime;

		if(time > 50)
		{
			gameObject.particleEmitter.emit = false;
		}
		if(time > 55)
		{
			Destroy (gameObject);
		}
	}
}
