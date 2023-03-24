/**
*Destorys end wall of lvl3
*/
using UnityEngine;
using System.Collections;

public class RAMScript : MonoBehaviour 
{
	public GameObject bttn1;
	ButtonBehav bttnScript;
	public SpriteRenderer wireFromBttn;

	public GameObject bttn2;
	ButtonBehav bttnScript2;
	public SpriteRenderer wireFromBttn2;

	public GameObject endWall;
	DestroyWall wallScript;

	public Sprite bentWireOn;
	public Sprite straightWireOn;

	public Sprite defaultWire1;
	public Sprite defaultWire2;

	public SpriteRenderer wireToTeleport;

	bool oneBttnHeld;
	bool twoBttnHeld;
	bool BothBttnsHeld;
	// Use this for initialization
	void Start ()
	{
		bttnScript = bttn1.GetComponent<ButtonBehav> ();
		bttnScript2 = bttn2.GetComponent<ButtonBehav> ();

		wallScript = endWall.GetComponent<DestroyWall> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if ( ( (bttnScript.player1BesideButton && Input.GetButton ("Tet1")) || (bttnScript.player2BesideButton && Input.GetButton ("Tet2")) ) && !BothBttnsHeld) //holding button
		{
			oneBttnHeld = true;
			wireFromBttn.sprite=bentWireOn;
		}
		if ( ( (bttnScript.player1BesideButton && Input.GetButtonUp ("Tet1")) || (bttnScript.player2BesideButton && Input.GetButtonUp ("Tet2")) ) && !BothBttnsHeld) //not holding button
		{
			oneBttnHeld = false;
			wireFromBttn.sprite = defaultWire1;
		}

		if ( ( (bttnScript2.player1BesideButton && Input.GetButton ("Tet1")) || (bttnScript2.player2BesideButton && Input.GetButton ("Tet2")) ) && !BothBttnsHeld) //holding button 
		{
			twoBttnHeld = true;
			wireFromBttn2.sprite=bentWireOn;
		}
		if ( ( (bttnScript2.player1BesideButton && Input.GetButtonUp ("Tet1")) || (bttnScript2.player2BesideButton && Input.GetButtonUp ("Tet2")) ) && !BothBttnsHeld) //not holding button
		{
			twoBttnHeld = false;
			wireFromBttn2.sprite = defaultWire1;
		}

		if (oneBttnHeld && twoBttnHeld)
		{
			wallScript.puzzleSolved = true;
			BothBttnsHeld = true;

			foreach (SpriteRenderer child in wireToTeleport.GetComponentsInChildren<SpriteRenderer>())
			{
				if (child.sprite == defaultWire1)//bentwire 
				{
					child.sprite = bentWireOn;
				}
				else if(child.sprite == defaultWire2)//straight wire 
				{
					child.sprite = straightWireOn;
				}
			}

		}
	
	}
}
