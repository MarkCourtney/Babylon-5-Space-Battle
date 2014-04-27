using UnityEngine;
using System.Collections;

public class TimeKeeper : MonoBehaviour {

	float totalTime;

	// Allow other classes to access the totalTime variable
	public float TotalTime {
		get {
			return totalTime;
		}
	}

	void Start () {
	
		totalTime = 0.0f;
	}

	// Provides a fixed time delta that other classes use to perform functionality
	void FixedUpdate () {
	
		totalTime += Time.fixedDeltaTime;
	}
}