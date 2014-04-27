using UnityEngine;
using System.Collections;

public class BackgroundAudio : MonoBehaviour {

	public AudioClip clip;
	public float min, max;

	void Awake() {
	
		// Keep the audio through the changing of scenes
		DontDestroyOnLoad(transform.gameObject);
	}


	void Start()
	{
		// Play the audio clip
		audio.clip = clip;
		audio.Play();
	}
}
