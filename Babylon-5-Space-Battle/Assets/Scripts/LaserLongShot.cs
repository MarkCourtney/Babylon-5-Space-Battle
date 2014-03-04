using UnityEngine;
using System.Collections;

public class LaserLongShot : MonoBehaviour {

	public GameObject explosion;

	float time;
	bool startTime = false;

	void Update () {
		
		transform.position += transform.forward * 50 * Time.deltaTime;

		if(startTime)
		{
			//print (time);
			time += Time.deltaTime;
		}
		if(time > 3)
		{
			Destroy(gameObject);
		}
	}
	
	
	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.name == "Omega-X")
		{
			startTime = true;
			Instantiate(explosion, transform.position + transform.forward * 300, Quaternion.identity);
			print (col.gameObject.name);
			//Destroy(gameObject);
		}
	}
}
