using UnityEngine;
using UnityEditor;
using System.Collections;

public class DestroyPrefab : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		PrefabUtility.DisconnectPrefabInstance(gameObject);
	}
}