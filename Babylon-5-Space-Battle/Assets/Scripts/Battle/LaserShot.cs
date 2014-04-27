using UnityEngine;
using System.Collections;

public class LaserShot : MonoBehaviour {

	public GameObject explosion;
	float lifeDuration;

	// Update is called once per frame
	void Update () {
	
		lifeDuration += Time.deltaTime;

		transform.position += transform.forward * 100 * Time.deltaTime;

		// Destroy the gameobject after 3 seconds if it hasn't his anything
		if(lifeDuration > 5)
		{
			Destroy(gameObject);
		}
	}


	// When colliding with an object create an explosion
	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.name == "Omega-X" || col.gameObject.name.Contains("LaserLong"))
		{
			Instantiate(explosion, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}