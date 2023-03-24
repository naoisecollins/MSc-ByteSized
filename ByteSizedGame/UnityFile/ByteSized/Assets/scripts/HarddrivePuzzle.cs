/**
*Controls arrow that moves around harddrive
*/
using UnityEngine;
using System.Collections;

public class HarddrivePuzzle : MonoBehaviour 
{
	public GameObject turnstyle; //used for moving up and down within circle

	public GameObject pipeLight;

	public GameObject button;
	ButtonBehav buttonScript;

	string[] moveArray;
	int i; //count how many blocks are there
	int maxInput; //how many blocks players can use

	Vector3 defaultArrowPos;


	float orginalXPos;
	public float force= 3.0f;

	bool returnToOrgPos;
	bool moveDown;
	bool moveUp;
	bool moveLeft;
	bool moveRight;

	public float amountToMove; //amount to rotate
	public float amountToMoveXY; // amount to move up, right, left and down

	public GameObject inputUp;
	public GameObject inputDown;
	public GameObject inputLeft;
	public GameObject inputRight;
	int blockInputtedByPlayer;//count how many blocks are inputtedArray
	public GameObject[] emptySpaceForInput;//boxes that sow input
	public GameObject[] bckEmptySpaceForInput;//bckgrnd of boxes

	public Sprite upBox;
	public Sprite downBox;
	public Sprite leftBox;
	public Sprite rightBox;

	public Sprite correctUp;
	public Sprite correctDown;
	public Sprite correctRight;
	public Sprite correctLeft;
	Sprite defaultNoInputSprite;

	CodeBlock blockScript;
	GameObject codeBlock;

	Vector3 transport;

	public SpriteRenderer wire;
	public Sprite wireOn;

	bool arrowMoving;
	bool inputStarted;

	Color platformStndrdColour;
	Color platformMovigColour;

	void Start () 
	{
		i = 0;
		blockInputtedByPlayer = 0;

		buttonScript = button.GetComponent<ButtonBehav> ();

		maxInput = emptySpaceForInput.Length;
		moveArray = new string[maxInput];

		defaultNoInputSprite = emptySpaceForInput [0].GetComponent<SpriteRenderer> ().sprite;

		defaultArrowPos = turnstyle.transform.position;

		platformStndrdColour = new Color (1, 0.92f, 0.016f, 1);
		platformMovigColour = new Color (0, 1, 0, 1);
	}
	// Update is called once per frame
	void Update () 
	{
		if (( (buttonScript.player1BesideButton && Input.GetButtonDown("Tet1")) || (buttonScript.player2BesideButton && Input.GetButtonDown("Tet2")) ) && !arrowMoving )
		{
			StartCoroutine ("ExecuteCode");
			for (int z = 0; z < maxInput; z++) 
			{
				bckEmptySpaceForInput [z].GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 1);
			}
			blockInputtedByPlayer = 0;
			i = 0;
		}//end of if for button e
			
		if (moveDown)
		{
			transport = new Vector2 (turnstyle.transform.position.x, turnstyle.transform.position.y - amountToMoveXY);
			//rb.MovePosition (transport);
			turnstyle.transform.position = Vector3.Lerp (turnstyle.transform.position, transport, Time.deltaTime);
		}
		if (moveUp)
		{
			transport = new Vector2 (turnstyle.transform.position.x, turnstyle.transform.position.y + amountToMoveXY);
			//rb.MovePosition (transport);
			turnstyle.transform.position = Vector3.Lerp (turnstyle.transform.position, transport, Time.deltaTime);
		}
		if (moveRight)
		{
			transport = new Vector2 (turnstyle.transform.position.x + amountToMoveXY, turnstyle.transform.position.y);
			//rb.MovePosition (transport);
			turnstyle.transform.position = Vector3.Lerp (turnstyle.transform.position, transport, Time.deltaTime);

		}
		if (moveLeft)
		{
			transport = new Vector2 (turnstyle.transform.position.x - amountToMoveXY, turnstyle.transform.position.y);
			//rb.MovePosition (transport);
			turnstyle.transform.position = Vector3.Lerp (turnstyle.transform.position, transport, Time.deltaTime);
		}

		if (returnToOrgPos)
		{
			Vector3 transport2 = defaultArrowPos;
			turnstyle.transform.position = Vector3.Lerp (turnstyle.transform.position, transport2, Time.deltaTime);
		}

		if (arrowMoving) 
		{
			turnstyle.GetComponent<SpriteRenderer> ().color = platformMovigColour;
		}
		else 
		{
			turnstyle.GetComponent<SpriteRenderer> ().color = platformStndrdColour;
		}

		if (i == maxInput)//resets so array never gets to big
		{
			i=0;
		}

		if (blockInputtedByPlayer == maxInput)
		{
			blockInputtedByPlayer = 0;
		}

		if (emptySpaceForInput [0].GetComponent<SpriteRenderer> ().sprite != defaultNoInputSprite) 
		{
			wire.sprite = wireOn;
			inputStarted=true;
		}
		else 
		{
			inputStarted=false;
		}

		if (inputStarted && !arrowMoving) 
		{
			bckEmptySpaceForInput[i].GetComponent<SpriteRenderer>().color=new Color(1,0.92f,0.016f,1);//highlight colour
			if (i != 0) 
			{
				bckEmptySpaceForInput [i - 1].GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 1);
			}
			else if (i == 0) 
			{
				bckEmptySpaceForInput [maxInput-1].GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 1);
			}

		}
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (!arrowMoving)//cant throw in blocks while puzzle is moving
		{
			if (coll.gameObject.name.Contains ("clockWise") || coll.gameObject.name.Contains ("antiClockWise") || coll.gameObject.name.Contains ("Arrow"))
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
				i++;
			}
		}
			
	}

	public IEnumerator ExecuteCode ()
	{
		arrowMoving = true;
		pipeLight.SetActive(false);
		Sprite defaultSpriteImage;
		SpriteRenderer spriteFromInputtedCode;

		if (moveArray [0] != "") //there is input there
		{
			for (int j = 0; j < maxInput; j++) 
			{
				spriteFromInputtedCode = emptySpaceForInput [j].GetComponent<SpriteRenderer> ();
				defaultSpriteImage = spriteFromInputtedCode.sprite;

				if (moveArray [j] == "d") 
				{
					moveDown = true;
					spriteFromInputtedCode.sprite = correctDown;
				}
				else if (moveArray [j] == "u") 
				{
					moveUp = true;
					spriteFromInputtedCode.sprite = correctUp;
				}
				else if (moveArray [j] == "l") 
				{
					moveLeft = true;
					spriteFromInputtedCode.sprite = correctLeft;
					Debug.Log ("should shake");
				}
				else if (moveArray [j] == "r")
				{
					moveRight = true;
					spriteFromInputtedCode.sprite = correctRight;
					Debug.Log ("should shake");
				}

				yield return new WaitForSeconds (1f);
				moveDown = false;
				moveUp = false;
				moveLeft = false;
				moveRight = false;
				moveArray [j] = "";
				emptySpaceForInput [j].GetComponent<SpriteRenderer> ().sprite = defaultSpriteImage;
			}//end of for

			yield return new WaitForSeconds (.5f);
			for (int z = 0; z <= emptySpaceForInput.Length - 1; z++) 
			{
				emptySpaceForInput [z].GetComponent<SpriteRenderer> ().sprite = defaultNoInputSprite;
			}//end of for for getting rid of blocks afer execution
		}//end of for

		yield return new WaitForSeconds (3f);
		returnToOrgPos = true;
		yield return new WaitForSeconds (12f);
		returnToOrgPos = false;;

		arrowMoving = false;
		pipeLight.SetActive (true);
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

			blockInputtedByPlayer++;
		} 
	}
}
