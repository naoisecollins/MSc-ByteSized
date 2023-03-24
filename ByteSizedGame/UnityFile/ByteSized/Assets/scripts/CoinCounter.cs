/**
*Counts coinscore as players collide with them
*/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour 
{

	bool hitCount;//so score is only counted the once

	CharManager charMangScript; 
	public GameObject cam;




	// Use this for initialization
	void Start () 
	{
		charMangScript = cam.GetComponent<CharManager> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag == "Player")
		{
			if (!hitCount) 
			{
				hitCount = true;
				charMangScript.coinCount++; //adds to coincount in charmang
			}
				
		}


	}
}
