using UnityEngine;
using System.Collections;

public class TimeKeeper : MonoBehaviour {

	float timer;

	public float Timer {
		get {
			return timer;
		}
	}

	void Start () {
	
		timer = 0.0f;
	}

	void Update () {
	
		timer += Time.deltaTime;
	}
}
