/**
*End pipe for harddive puzzles, counts how many blue pieces players have freed and activates buttons to destroy walls
*/
using UnityEngine;
using System.Collections;

public class EndPipeScript : MonoBehaviour 
{
	public int pieceCounter;

	public GameObject bttn;
	ButtonBehav bttnScript;

	public GameObject bttn2;
	ButtonBehav bttnScript2;

	public GameObject endWall;
	DestroyWall wallScript;

	public GameObject itemWWireChange;
	WiresToCheckpoints wireChangeScript;

	public SpriteRenderer bentWireOff;//wire from puzzle to bttn
	public Sprite bentWireOn;

	public SpriteRenderer straightWireOff;//wire from puzzle to bttn && parent to wires from puzzle to bttns
	public Sprite straightWireOn;

	public SpriteRenderer wireFromBttnToWall;

	public SpriteRenderer wireFromBttn1;
	public SpriteRenderer wireFromBttn2;
	Sprite defaultWire1;
	Sprite defaultWire2;


	bool oneBttnHeld;
	bool twoBttnHeld;
	bool BothBttnsHeld;

	// Use this for initialization
	void Start () 
	{
		bttnScript = bttn.GetComponent<ButtonBehav> ();
		bttnScript2 = bttn2.GetComponent<ButtonBehav> ();

		defaultWire1 =bentWireOff.sprite;
		defaultWire2 = straightWireOff.sprite;

		wallScript = endWall.GetComponent<DestroyWall> ();

		wireChangeScript = itemWWireChange.GetComponent<WiresToCheckpoints> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (pieceCounter == 2)
		{
			/*wires from puzzle to button switched on*/
			foreach (SpriteRenderer child in straightWireOff.GetComponentsInChildren<SpriteRenderer>())
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

		/*bttns will only work when both pieces have been collected*/
		if ( ( (bttnScript.player1BesideButton && Input.GetButton ("Tet1")) || (bttnScript.player2BesideButton && Input.GetButton ("Tet2")) ) && pieceCounter == 2 && !BothBttnsHeld) 
		{
			oneBttnHeld = true;
			wireFromBttn1.sprite = bentWireOn;
		}
		if ( ( (bttnScript.player1BesideButton && Input.GetButtonUp ("Tet1")) || (bttnScript.player2BesideButton && Input.GetButtonUp ("Tet2")) ) && pieceCounter == 2 && !BothBttnsHeld) //let go off button
		{
			oneBttnHeld = false;
			wireFromBttn1.sprite = defaultWire1;
		}

		if ( ( (bttnScript2.player1BesideButton && Input.GetButton ("Tet1")) || (bttnScript2.player2BesideButton && Input.GetButton ("Tet2")) ) && pieceCounter == 2 && !BothBttnsHeld) 
		{
			twoBttnHeld = true;
			wireFromBttn2.sprite = bentWireOn;
		}
		if ( ( (bttnScript2.player1BesideButton && Input.GetButtonUp ("Tet1")) || (bttnScript2.player2BesideButton && Input.GetButtonUp ("Tet2")) ) && pieceCounter == 2 && !BothBttnsHeld) 
		{
			twoBttnHeld = false;
			wireFromBttn2.sprite = defaultWire1;

		}

		if (oneBttnHeld && twoBttnHeld)//both buttons held
		{
			wallScript.puzzleSolved = true;//start wall destory
			BothBttnsHeld = true;

			/*switches on all wires*/
			foreach (SpriteRenderer child in wireFromBttnToWall.GetComponentsInChildren<SpriteRenderer>())
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
