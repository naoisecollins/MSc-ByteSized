/**
*Makes sure start button goes to scene player selected OR start if none selected
*/
using UnityEngine;
using System.Collections;

public class LvlSelectScript : MonoBehaviour 
{
	GameMang gameMangScript;
	GameObject gameMangObject;

	SceneChanges sceneScript;

	// Use this for initialization
	void Start () 
	{
		gameMangObject= GameObject.FindGameObjectWithTag ("GameController");
		gameMangScript = gameMangObject.GetComponent<GameMang> ();

		sceneScript = GetComponent<SceneChanges> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	/*called in level select screen, stores the level they wanna to go to*/
	public void LevelSelected(string lvlSelected)
	{
			gameMangScript.lvlSelected = lvlSelected; //level selected by player on screen
			gameMangScript.cameFromLvlSelectScreen = true;//shows player has come from level selecet
	}

	/*start button from character select*/
	public void startGameFromLvlSelect()
	{
		if (gameMangScript.cameFromLvlSelectScreen) //if they came from the level select screen, go to the level they wanted
		{
			sceneScript.StartGameButton (gameMangScript.lvlSelected);
		}
		else //they clicked start from the main menu
		{
			sceneScript.StartGameButton ("lvl1");
		}
	}
		
}
