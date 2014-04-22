using UnityEngine;
using System.Collections;

public class BackgroundAudio : MonoBehaviour {

	public AudioClip clip;
	public float min, max;

	void Awake() {
	
		DontDestroyOnLoad(transform.gameObject);
	}


	void Start()
	{
		audio.clip = clip;
		audio.Play();
	}


//	void turnOff()
//	{
//		if(audio.volume > 0)
//			audio.volume -= 0.005f;
//	}
//
//	void turnOn()
//	{
//		if(audio.volume < 1)
//			audio.volume += 0.005f;
//	}
//
//	void Update() {
//
//	
//		Invoke("turnOff", 20);
//
//		Invoke("turnOn", 23);
//	}
}
