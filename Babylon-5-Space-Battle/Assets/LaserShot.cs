using UnityEngine;
using System.Collections;

public class LaserShot : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		transform.position += transform.forward * 100 * Time.deltaTime;
	}
}