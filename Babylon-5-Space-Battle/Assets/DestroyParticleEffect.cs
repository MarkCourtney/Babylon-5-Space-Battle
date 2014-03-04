using UnityEngine;
using System.Collections;

public class DestroyParticleEffect : MonoBehaviour {

	float time;

	// Use this for initialization
	void Start () {
	
		time = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
		time += Time.deltaTime;

		if(time > 1)
		{
			gameObject.particleEmitter.emit = false;
		}
		if(time > 5)
		{
			Destroy (gameObject);
		}
	}
}
