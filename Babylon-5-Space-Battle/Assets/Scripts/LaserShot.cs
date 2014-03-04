using UnityEngine;
using System.Collections;

public class LaserShot : MonoBehaviour {

	public GameObject explosion;

	// Update is called once per frame
	void Update () {
	
		transform.position += transform.forward * 100 * Time.deltaTime;
	}


	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.name == "Omega-X")
		{
			Instantiate(explosion, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}