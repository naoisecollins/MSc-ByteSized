/**
*Creates function blocks based on datablocks inputted by player
*/
using UnityEngine;
using System.Collections;

public class FunctionBlockCreator : MonoBehaviour 
{

	string[] moveArray;
	int i; //count how many blocks are there
	int maxInput; //how many blocks players can use

	public GameObject button;
	ButtonBehav buttonScript;

	public GameObject inputUp;
	public GameObject inputDown;
	public GameObject inputLeft;
	public GameObject inputRight;
	public GameObject inputLoop;
	int blockInputtedByPlayer;//count how many blocks are inputtedArray
	public GameObject[] emptySpaceForInput;

	public Sprite upBox;
	public Sprite downBox;
	public Sprite leftBox;
	public Sprite rightBox;
	public Sprite loopBox;

	Sprite defaultNoInputSprite;

	CodeBlock blockScript;

	public GameObject functionBlockPref;
	GameObject functionBlock;//new block being created
	FunctionCodeBlock functionBlockScript;//script that will be linked to new functon block

	float throwForce=2f;//force it's shot out of machine

	public bool firstBlockMade;//at least one block made

	void Start () 
	{
		i = 0;
		blockInputtedByPlayer = 0;
		maxInput = emptySpaceForInput.Length;
		moveArray = new string[maxInput];

		buttonScript = button.GetComponent<ButtonBehav> ();

		defaultNoInputSprite = emptySpaceForInput [0].GetComponent<SpriteRenderer> ().sprite;

		firstBlockMade = false;

	}
	// Update is called once per frame
	void Update () 
	{
		if ( ( (buttonScript.player1BesideButton && Input.GetButtonDown ("Tet1")) || (buttonScript.player2BesideButton && Input.GetButtonDown("Tet2")) ) && functionBlockScript!=true) 
		{
			CreateFunctionBlock ();
		}
		if (i == maxInput)//resets so array never gets to big
		{
			i=0;
		}

		if (blockInputtedByPlayer == maxInput)
		{
			blockInputtedByPlayer =0;
		}

	
			
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.name.Contains ("Arrow"))
		{
			blockScript = coll.gameObject.GetComponent<CodeBlock> ();
			if (coll.gameObject.name.Contains ("downArrow") && !blockScript.beingHeld) 
			{
				moveArray [i] = "d";
				SpawnInputCode (inputDown);
				Destroy (coll.gameObject);

			}
			else if (coll.gameObject.name.Contains ("upArrow") && !blockScript.beingHeld) 
			{
				moveArray [i] = "u";
				Debug.Log ("u");
				SpawnInputCode (inputUp);
				Destroy (coll.gameObject);
			}
			else if (coll.gameObject.name.Contains ("leftArrow") && !blockScript.beingHeld)
			{
				moveArray [i] = "l";
				SpawnInputCode (inputLeft);
				Destroy (coll.gameObject);
			}
			else if (coll.gameObject.name.Contains ("rightArrow") && !blockScript.beingHeld)
			{
				moveArray [i] = "r";
				SpawnInputCode (inputRight);
				Destroy (coll.gameObject);
			}
			else if (coll.gameObject.name.Contains ("loopsArrows") && !blockScript.beingHeld) 
			{
				moveArray[i] = "p";
				SpawnInputCode (inputLoop);
				Destroy (coll.gameObject);
			}
			i++;
		}
	}

	/*Creates function block and attaches moveArray to it*/
	public void CreateFunctionBlock()
	{
		
		Vector3 blockPos = new Vector3 (transform.position.x+2f, transform.position.y, transform.position.z);
		functionBlock = (Instantiate (functionBlockPref, blockPos, transform.rotation)) as GameObject;
		functionBlock.GetComponent<Rigidbody2D> ().velocity = new Vector2 (transform.localScale.x, 1) * throwForce;

		functionBlockScript = functionBlock.GetComponent<FunctionCodeBlock> ();
		functionBlockScript.function = moveArray;
		firstBlockMade = true;
	}


	public void SpawnInputCode(GameObject inputtedBlock)
	{
		if (blockInputtedByPlayer < maxInput) 
		{

			if (inputtedBlock==inputDown)
			{
				Debug.Log ("changed sprite");
				emptySpaceForInput [blockInputtedByPlayer].GetComponent<SpriteRenderer> ().sprite = downBox;
			}
			if (inputtedBlock==inputUp)
			{
				emptySpaceForInput [blockInputtedByPlayer].GetComponent<SpriteRenderer> ().sprite = upBox;
			}
			if (inputtedBlock==inputLeft)
			{
				emptySpaceForInput [blockInputtedByPlayer].GetComponent<SpriteRenderer> ().sprite = leftBox;
			}
			if (inputtedBlock==inputRight)
			{
				emptySpaceForInput [blockInputtedByPlayer].GetComponent<SpriteRenderer> ().sprite = rightBox;
			}
			if(inputtedBlock==inputLoop)
			{
				emptySpaceForInput [blockInputtedByPlayer].GetComponent<SpriteRenderer> ().sprite =loopBox;
			}
			blockInputtedByPlayer++;

		} 
	}
}
