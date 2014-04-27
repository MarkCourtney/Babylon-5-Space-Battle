using UnityEngine;
using System.Collections;

public class DestroyParticleEffect : MonoBehaviour {

	float time;


	void Start () {
	
		time = 0;
	}

	void Update () {
	
		time += Time.deltaTime;

		// Stop emitting the particles after 50 seconds
		if(time > 50)
		{
			gameObject.particleEmitter.emit = false;
		}
		// Destroy the gameobject after 55 seconds
		if(time > 55)
		{
			Destroy (gameObject);
		}
	}
}
