using UnityEngine;
using System.Collections;

public class Explode : MonoBehaviour {

	public GameObject explosion;
	int i = 0;

	void Start () {
	

	}


	void Update () {
	

	}

	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.name.Contains("LaserShort"))
		{
			i++;
			print (i);

			if(i>50)
			{
				
			}
		}



	}
}
