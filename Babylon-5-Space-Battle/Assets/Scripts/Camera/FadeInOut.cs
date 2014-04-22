using UnityEngine;
using System.Collections;

public class FadeInOut : MonoBehaviour {

	public Texture2D backgroundColor;
	public float alpha;
	public bool fadeIn, fadeOut;
	string level;
	float time;
	
	void Start () {

		fadeIn = true;
		fadeOut = false;

		alpha = 1.0f;

		time = 0.0f;
	}

	
	public void loadLevel(string levelName)
	{
		fadeIn = false;
		fadeOut = true;
		level = levelName;
	}

	public void startLevel(string levelName)
	{
		fadeIn = true;
	}
	
	void Update()	
	{
		time += Time.deltaTime;
	}
	
	void drawTexture()
	{
		GUI.color = new Color(0, 0, 0, alpha);
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), backgroundColor);
	}


	void OnGUI()
	{
		if(time > 20 && Application.loadedLevel == 0)
		{
			loadLevel("Battle");
			alpha += 0.0025f;
			drawTexture();
			
			if(alpha > 0.95f)
			{
				Application.LoadLevel(level);
			}
		}
		else if(Application.loadedLevel == 0 && time < 20)
		{
			alpha -= 0.0030f;
			drawTexture();

			if(alpha < 0)
				alpha = 0;
		}

		if(Application.loadedLevel == 1 && alpha >= 0)
		{
			alpha -= 0.0030f;
			drawTexture();

			if(alpha <= 0)
			{
				Destroy(GetComponent("FadeInOut"));
			}
		}
	}
}
