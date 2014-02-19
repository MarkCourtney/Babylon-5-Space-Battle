using UnityEngine;
using System.Collections;

public class TimeKeeper : MonoBehaviour {

	float totalTime;

	public float TotalTime {
		get {
			return totalTime;
		}
	}

	void Start () {
	
		totalTime = 0.0f;
	}

	void Update () {
	
		totalTime += Time.deltaTime;
		//print (totalTime);
	}
}