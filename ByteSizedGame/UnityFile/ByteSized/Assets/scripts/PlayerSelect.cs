/**
*Changes bool in game mang for player to select their characters
*/
using UnityEngine;
using System.Collections;

public class PlayerSelect : MonoBehaviour 
{
	public GameObject dog;
	public GameObject cat;
	public GameObject girl;
	public GameObject boy;

	/*char select*/
	GameMang gameMangScript;
	GameObject gameMangObject;
	// Use this for initialization
	void Start () 
	{
		gameMangObject= GameObject.FindGameObjectWithTag ("GameController");
		gameMangScript = gameMangObject.GetComponent<GameMang> ();

		gameMangScript.dogChar = true;
		gameMangScript.boyChar = true;

	}

	// Update is called once per frame
	void Update ()
	{
		
		if (((Input.GetAxisRaw ("Horizontal2")== 1) && (cat.activeSelf)))  //showing dog
		{
			dog.SetActive (true);
			cat.SetActive (false);
			gameMangScript.dogChar = true;
			gameMangScript.catChar = false;
		}
		else if (((Input.GetAxisRaw ("Horizontal2")== -1) && (dog.activeSelf))) //showing cat
		{
			cat.SetActive (true);
			dog.SetActive (false);
			gameMangScript.dogChar = false;
			gameMangScript.catChar = true;
		}

		if (((Input.GetAxisRaw ("Horizontal1")== 1)&&(girl.activeSelf)))  //showing boy
		{
			boy.SetActive (true);
			girl.SetActive (false);
			gameMangScript.boyChar = true;
			gameMangScript.girlChar = false;
		}
		else if (((Input.GetAxisRaw ("Horizontal1")== -1)&& (boy.activeSelf))) //showing girl
		{
			girl.SetActive (true);
			boy.SetActive (false);
			gameMangScript.boyChar = false;
			gameMangScript.girlChar = true;
		}


	}
}

