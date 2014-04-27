using UnityEngine;
using System.Collections;

public class LaserLongShot : MonoBehaviour {

	public GameObject explosion;

	float lifeDuration;

	void Update () {
		
		transform.position += transform.forward * 50 * Time.deltaTime;

		lifeDuration += Time.deltaTime;

		// Destroy the gameobject after 3 seconds if it hasn't his anything
		if(lifeDuration > 3)
		{
			Destroy(gameObject);
		}
	}
	

	// When colliding with an object create an explosion
	void OnCollision(Collision col)
	{
		Destroy(gameObject);
		if(col.gameObject.name == "Omega-X" || col.gameObject.name == ("LaserLong(Clone)") )
		{
			Instantiate(explosion, transform.position + transform.forward * 300, Quaternion.identity);

		}
	}
}
