/**
*Handles collision between arrow and blue piece
*/
using UnityEngine;
using System.Collections;

public class ProcessorTurnStyle : MonoBehaviour 
{
	
	public GameObject bttn1;
	ButtonBehav bttnScript;
	public SpriteRenderer wireFromBttn;

	public GameObject bttn2;
	ButtonBehav bttnScript2;
	public SpriteRenderer wireFromBttn2;

	public GameObject endWall;
	DestroyWall wallScript;

	public SpriteRenderer bentWireOff;
	public Sprite bentWireOn;

	public SpriteRenderer straightWireOff;
	public Sprite straightWireOn;

	Sprite defaultWire1;
	Sprite defaultWire2;

	public SpriteRenderer wireToTeleport;

	bool oneBttnHeld;
	bool twoBttnHeld;
	bool BothBttnsHeld;

	bool firstPartSolved;

	// Use this for initialization
	void Start () 
	{
		bttnScript = bttn1.GetComponent<ButtonBehav> ();
		bttnScript2 = bttn2.GetComponent<ButtonBehav> ();

		defaultWire1 =bentWireOff.sprite;
		defaultWire2 = straightWireOff.sprite;

		wallScript = endWall.GetComponent<DestroyWall> ();

	}
	
	// Update is called once per frame
	void Update () 
	{
		if ( ( (bttnScript.player1BesideButton && Input.GetButton ("Tet1")) || (bttnScript.player2BesideButton && Input.GetButton ("Tet2")) ) && !BothBttnsHeld && firstPartSolved) 
		{
			oneBttnHeld = true;
			wireFromBttn.sprite=bentWireOn;
		}
		if ( ( (bttnScript.player1BesideButton && Input.GetButtonUp ("Tet1")) || (bttnScript.player2BesideButton && Input.GetButtonUp ("Tet2")) ) && !BothBttnsHeld) 
		{
			oneBttnHeld = false;
			wireFromBttn.sprite = defaultWire1;
		}

		if ( ( (bttnScript2.player1BesideButton && Input.GetButton ("Tet1")) || (bttnScript2.player2BesideButton && Input.GetButton ("Tet2")) ) && !BothBttnsHeld && firstPartSolved) 
		{
			twoBttnHeld = true;
			wireFromBttn2.sprite=bentWireOn;
		}
		if ( ( (bttnScript2.player1BesideButton && Input.GetButtonUp ("Tet1")) || (bttnScript2.player2BesideButton && Input.GetButtonUp ("Tet2")) ) && !BothBttnsHeld) 
		{
			twoBttnHeld = false;
			wireFromBttn2.sprite = defaultWire1;

		}

		if (oneBttnHeld && twoBttnHeld && firstPartSolved)
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

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.name == "turnStyle") 
		{
			firstPartSolved = true;
			GetComponent<SpriteRenderer> ().enabled = false;
			bentWireOff.sprite = bentWireOn;
			straightWireOff.sprite = straightWireOn;
			bttn1.GetComponent<ButtonBehav> ().enabled = true;
			bttn2.GetComponent<ButtonBehav> ().enabled = true;
		}		
			
	}
}
