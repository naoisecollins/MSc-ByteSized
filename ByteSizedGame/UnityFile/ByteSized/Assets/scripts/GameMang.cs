/**
*Constant object throughout players entire game
*/
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameMang : MonoBehaviour
{

	/*CHAR CHOICE*/
	public bool boyChar;
	public bool girlChar;
	public bool catChar;
	public bool dogChar;

	/*LANG CHOICE*/
	public bool engSelected;

	/*FROM LVLSELECT*/
	public string lvlSelected;
	public bool cameFromLvlSelectScreen;

	/*COIN*/
	public int coinScore;

	GameObject gameMangObj;//old audio
	public AudioSource backGrndAudio;//current scene audio


	void Awake()
	{
		/*so music doesn't repeat, code modified from http://answers.unity3d.com/questions/878382/audio-or-music-to-continue-playing-between-scene.html*/
		gameMangObj = GameObject.Find("gameController");
		if(gameMangObj==null) //first time in scene audio hasn't started yet
		{
			gameMangObj = this.gameObject;
			gameMangObj.name = "gameController";
			backGrndAudio.Play ();
		}
		else//already a game mang object
		{
			if(gameObject.name!="gameController")
			{
				Destroy(gameObject);
			}
		}
		DontDestroyOnLoad (this);
	}

	// Use this for initialization
	void Start () 
	{
		
	}
	// Update is called once per frame
	void Update ()
	{

	}

	void OnLevelWasLoaded()//checks which scene players in on each level load
	{
		if (SceneManager.GetActiveScene ().name == "Sequencing") 
		{
			coinScore = 0;//resets coin score
			Debug.Log ("coin score recounted");
		}
	}
		
}
