using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {

	GameObject ambientNoise, backgroundMusic;
	TimeKeeper tk;
	GameObject timeHolder;


	void Start () {
	
		DontDestroyOnLoad(transform.gameObject);
		ambientNoise = GameObject.FindGameObjectWithTag("Ambience");
		backgroundMusic = GameObject.FindGameObjectWithTag("Music");
		timeHolder = GameObject.FindGameObjectWithTag("Time");
		tk = timeHolder.GetComponent<TimeKeeper>();
	}


	void Update () {
	
		// Turn down the volume as the scene is ending
		if(tk.TotalTime > 75)
		{
			ambientNoise.audio.volume -= 0.0045f;
			backgroundMusic.audio.volume -= 0.0045f;
		}
	}
}
