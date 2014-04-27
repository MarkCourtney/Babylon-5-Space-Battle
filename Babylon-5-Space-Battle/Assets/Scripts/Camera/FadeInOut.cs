using UnityEngine;
using System.Collections;

public class FadeInOut : MonoBehaviour {

	public Texture2D backgroundColor;
	public float alpha;
	string level;

	TimeKeeper tk;
	GameObject timeHolder;
	
	void Start () {

		timeHolder = GameObject.FindGameObjectWithTag("Time");
		tk = timeHolder.GetComponent<TimeKeeper>();
	}

	
	public void loadLevel(string levelName)
	{
		level = levelName;
	}
	

	void drawTexture()
	{
		GUI.color = new Color(0, 0, 0, alpha);
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), backgroundColor);
	}


	void OnGUI()
	{
		if(Application.loadedLevel == 0 && tk.TotalTime < 20)
		{
			alpha -= 0.0030f;
			drawTexture();
			
			if(alpha < 0)
				alpha = 0;
		}
		else if(Application.loadedLevel == 0 && tk.TotalTime > 20)
		{
			loadLevel("Battle");
			alpha += 0.0025f;
			drawTexture();
			
			if(alpha > 0.95f)
			{
				Application.LoadLevel(level);
			}
		}


		// Fade out after 75 seconds
		if(Application.loadedLevel == 1 && alpha <= 1 && tk.TotalTime >= 80)
		{
			alpha += 0.0035f;
			drawTexture();
		}
		// Fade in at the beginning of the level
		else if(Application.loadedLevel == 1 && alpha >= 0)
		{
			alpha -= 0.0035f;
			drawTexture();
			
		}
	}
}
