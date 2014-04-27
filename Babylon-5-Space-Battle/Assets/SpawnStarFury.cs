using UnityEngine;
using System.Collections;

public class SpawnStarFury : MonoBehaviour {

	//new Vector3(-16, -106f, 1094)
	//new Vector3(-100, 35f, 911)
	Vector3[] pos = {new Vector3(-12.5f, -70, 706), new Vector3(-12.5f, -70, 721), new Vector3(-12.5f, -70, 736),
					 new Vector3(101, -40f, 819),	new Vector3(101, -40f, 834),	new Vector3(101, -40f, 849),
					 new Vector3(168f, -8.5f, 691), new Vector3(168f, -8.5f, 706), new Vector3(168f, -8.5f, 721),
					 new Vector3(-16, -106f, 1094), new Vector3(-16, -106f, 1109), new Vector3(-16, -106f, 1124), 
					 new Vector3(-100, 35f, 920), new Vector3(-100, 35f, 935), new Vector3(-100, 35f, 950)};

	public GameObject starFury;
	int i = 0;

	void spawnShips()
	{
		foreach(Vector3 p in pos)
		{
			if(i <= 15)
				Instantiate(starFury, p, Quaternion.identity);
				
			else
				Destroy(gameObject);
			
			i++;
		}

	}
	
	// Update is called once per frame
	void Update () {
	
		Invoke("spawnShips", 22.5f);
	}
}
