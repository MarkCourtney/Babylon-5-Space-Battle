using UnityEngine;
using System.Collections;

public class LaserLongShot : MonoBehaviour {

	public GameObject explosion;

	float time;

	void Update () {
		
		transform.position += transform.forward * 50 * Time.deltaTime;

		time += Time.deltaTime;

		if(time > 3)
		{
			Destroy(gameObject);
		}
	}
	
	
	void OnCollision(Collision col)
	{
		Destroy(gameObject);
		if(col.gameObject.name == "Omega-X" || col.gameObject.name == ("LaserLong(Clone)") )
		{
			print (col.gameObject.name);
			Instantiate(explosion, transform.position + transform.forward * 300, Quaternion.identity);

		}
	}
}
