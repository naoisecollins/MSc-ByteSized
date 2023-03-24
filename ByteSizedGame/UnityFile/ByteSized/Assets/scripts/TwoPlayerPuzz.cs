/**
 *Destroys wall when both players hold down both buttons at the same time 
*/
using UnityEngine;
using System.Collections;

public class TwoPlayerPuzz : MonoBehaviour 
{
	public GameObject bttn1;
	ButtonBehav bttnScript1;
	public SpriteRenderer bttn1Wire1;
	public SpriteRenderer bttn1Wire2;

	public GameObject bttn2;
	ButtonBehav bttnScript2;
	public SpriteRenderer bttn2Wire1;
	public SpriteRenderer bttn2Wire2;

	public SpriteRenderer wireAttachedToWall1;
	public SpriteRenderer wireAttachedToWall2;
	public SpriteRenderer wireAttachedToWall3;

	public Sprite straightWireOn;
	public Sprite curvedWireOn1;
	public Sprite curvedWireOn3;

	public Sprite straightWireOff;
	public Sprite curvedWireOff1;
	public Sprite curvedWireOff3;

	bool bttn1held;
	bool bttn2held;
	bool bothBttnsHeld;

	public GameObject wall;
	DestroyWall wallScript;


	// Use this for initialization
	void Start () 
	{
		bttnScript1 = bttn1.GetComponent<ButtonBehav> ();
		bttnScript2 = bttn2.GetComponent<ButtonBehav> ();
		wallScript = wall.GetComponent<DestroyWall> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!bothBttnsHeld) 
		{
			/*bttn 1 on script*/
			if ( (bttnScript1.player1BesideButton && Input.GetButton ("Tet1")) || (bttnScript1.player2BesideButton && Input.GetButton ("Tet2")) ) 
			{
				bttn1Wire1.sprite = straightWireOn;
				bttn1Wire2.sprite = curvedWireOn1;
				bttn1held=true;
			}
			/*release buttton 1*/
			if ( (bttnScript1.player1BesideButton && Input.GetButtonUp ("Tet1")) || (bttnScript1.player2BesideButton && Input.GetButtonUp ("Tet2")) ) 
			{
				bttn1Wire1.sprite = straightWireOff;
				bttn1Wire2.sprite = curvedWireOff1;
				bttn1held=false;
			}

			/*bttn 2 on script*/
			if ( (bttnScript2.player1BesideButton && Input.GetButton ("Tet1")) || (bttnScript2.player2BesideButton && Input.GetButton ("Tet2")) ) 
			{
				bttn2Wire1.sprite = straightWireOn;
				bttn2Wire2.sprite = curvedWireOn1;
				bttn2held=true;
			}
			/*release buttton 2*/
			if ( (bttnScript2.player1BesideButton && Input.GetButtonUp ("Tet1")) || (bttnScript2.player2BesideButton && Input.GetButtonUp ("Tet2")) ) 
			{
				bttn2Wire1.sprite = straightWireOff;
				bttn2Wire2.sprite = curvedWireOff1;
				bttn2held=false;
			}

		}

		//both bttns held down
		if (bttn1held && bttn2held) 
		{
			bothBttnsHeld = true;
			bttn2Wire1.sprite = straightWireOn;
			bttn2Wire2.sprite = curvedWireOn1;
			bttn1Wire1.sprite = straightWireOn;
			bttn1Wire2.sprite = curvedWireOn1;
			wireAttachedToWall1.sprite = straightWireOn;
			wireAttachedToWall2.sprite = curvedWireOn3;
			wireAttachedToWall3.sprite = straightWireOn;
			wallScript.puzzleSolved = true;
		}


	}
}
