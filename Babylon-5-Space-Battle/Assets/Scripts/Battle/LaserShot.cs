using UnityEngine;
using System.Collections;

public class LaserShot : MonoBehaviour {

	public GameObject explosion;
	float lifeDuration;

	// Update is called once per frame
	void Update () {
	
		lifeDuration += Time.deltaTime;

		transform.position += transform.forward * 100 * Time.deltaTime;

		if(lifeDuration > 5)
		{
			Destroy(gameObject);
		}
	}


	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.name == "Omega-X" || col.gameObject.name.Contains("LaserLong"))
		{
			Instantiate(explosion, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}