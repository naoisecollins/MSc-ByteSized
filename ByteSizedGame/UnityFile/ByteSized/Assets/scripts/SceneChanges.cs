/**
*Changes scenes, sends analytics and handles fade outs for scenes
*/
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Analytics;

public class SceneChanges : MonoBehaviour 
{
	Scene scene;//level currently in
	string lvl;//level player is going to

	/*Analytics*/
	bool quitGame;
	public bool startedGame;


	/*FADE OUT STUFF*/
	public Texture2D fadeOutTexture; //black fade out scrn
	public float fadeSpeed;//speed fade out happens
	int drawDepth = -1000;//draw order
	float alpha = 1.0f;
	int fadeDirection = -1;


	GameMang gameMangScript; 
	GameObject gameMangObject;//constant object through game

	// Use this for initialization
	void Start ()
	{
		scene = SceneManager.GetActiveScene ();

		gameMangObject= GameObject.FindGameObjectWithTag ("GameController");
		gameMangScript = gameMangObject.GetComponent<GameMang> ();
	} 
	
	// Update is called once per frame
	void Update ()
	{

	}

	/*Brings player back to title screen*/
	public void ReturnToStart()
	{
		quitGame = true;

		UnityEngine.Analytics.Analytics.CustomEvent("GameExited", new Dictionary<string, object>
			{
				{ "quitTIme", quitGame},
			});
		gameMangScript.cameFromLvlSelectScreen = false;
		gameMangScript.coinScore = 0;
		SceneManager.LoadScene ("Title");
	}

	/*For pc build allows player to quit game*/
	public void QuitGame()
	{
		Application.Quit ();
	}

	/*hanges scene to scene name mathcing string param*/
	public void StartGameButton(string bttn)
	{

		if (bttn == "lvl1") 
		{
			lvl = "Sequencing";
		}
		if (bttn == "lvl2") 
		{
			lvl = "Loops";
		}
		if (bttn == "lvl3")
		{
			lvl = "Functions";
		}
		if (bttn == "start") 
		{
			lvl = "Character Selection";
		}
		if (bttn == "lvlSelect")
		{
			lvl = "startScreen";
		}
		if (bttn == "endGame") 
		{
			lvl ="EndScreen";
		}

		startedGame = true;
		StartCoroutine (BeginFade(1, lvl));

	}

	/*Fade out script, based of tutorial on Unity site*/
	void OnGUI()
	{
		alpha = alpha + (fadeDirection * fadeSpeed * Time.deltaTime);
		alpha = Mathf.Clamp01 (alpha);

		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
		GUI.depth = drawDepth;
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), fadeOutTexture);

	}

	/*changes scene to string param*/
	IEnumerator BeginFade(int dir, string lvl)
	{
		fadeDirection = dir;
		yield return new WaitForSeconds (fadeSpeed);
		SceneManager.LoadScene (lvl);
	}
		
	void OnLevelWasLoaded()
	{
		fadeDirection = -1;
	}
}
