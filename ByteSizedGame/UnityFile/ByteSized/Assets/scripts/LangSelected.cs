/**
*Lang selection for start menu
*/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LangSelected : MonoBehaviour
{

	GameMang gameMangScript; 
	GameObject gameMangObject;//constant object through game

	public GameObject label;//label of lang dropdown menu

	// Use this for initialization
	void Start ()
	{
		gameMangObject= GameObject.FindGameObjectWithTag ("GameController");
		gameMangScript = gameMangObject.GetComponent<GameMang> ();
		gameMangScript.engSelected = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void ChangeLang()
	{
		if (label.GetComponent<Text> ().text.Contains ("English"))
		{
			gameMangScript.engSelected = true;
		}
		else
		{
			gameMangScript.engSelected = false;
		}
	}
}
