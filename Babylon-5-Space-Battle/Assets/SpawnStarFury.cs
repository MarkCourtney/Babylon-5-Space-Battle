using UnityEngine;
using System.Collections;

public class SpawnStarFury : MonoBehaviour {

	// Set up the positions for each starfury
	Vector3[] pos = {new Vector3(-12.5f, -70, 706), new Vector3(-12.5f, -70, 721), new Vector3(-12.5f, -70, 736),
					 new Vector3(101, -40f, 819),	new Vector3(101, -40f, 834),	new Vector3(101, -40f, 849),
					 new Vector3(168f, -8.5f, 691), new Vector3(168f, -8.5f, 706), new Vector3(168f, -8.5f, 721),
					 new Vector3(-16, -106f, 1094), new Vector3(-16, -106f, 1109), new Vector3(-16, -106f, 1124), 
					 new Vector3(-100, 35f, 920), new Vector3(-100, 35f, 935), new Vector3(-100, 35f, 950)};

	public GameObject starFury;
	int i = 0;


	// Spawn 15 StarFurys at specific positions
	void spawnShips()
	{
		foreach(Vector3 p in pos)
		{
			if(i <= 15)
				Instantiate(starFury, p, Quaternion.identity);

			// Destroy the script to ensure no more enemies spawn
			else
				Destroy(gameObject);
			
			i++;
		}

	}

	void Update () {
	
		// Invoke the spawnShips method at precisely 22.5 seconds
		Invoke("spawnShips", 22.5f);
	}
}
