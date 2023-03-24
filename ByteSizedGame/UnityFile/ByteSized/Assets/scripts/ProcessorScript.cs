/**
*Controls processor arrow based on player inputs
*/
using UnityEngine;
using System.Collections;

public class ProcessorScript : MonoBehaviour 
{

	public GameObject grabberArrow;
	public GameObject pipeLight;

	public GameObject button;
	ButtonBehav buttonScript;
	string[] moveArray;
	int i; //count how many blocks are there
	int maxInput; //how many blocks players can use

	Vector3 defaultObjPos;
	Vector2 transport;


	public float force= 3.0f;

	bool returnToOrgPos;
	bool aboutToMoveBackToOrg;
	bool moveDown;
	bool moveUp;
	bool moveLeft;
	bool moveRight;

	public float amountToMove; 
	float timeToReturnToOrg;

	public GameObject inputUp;
	public GameObject inputDown;
	public GameObject inputLeft;
	public GameObject inputRight;
	public GameObject inputLoop; 

	public Sprite upBox;
	public Sprite downBox;
	public Sprite leftBox;
	public Sprite rightBox;
	public Sprite loopBox;

	public Sprite correctUp;
	public Sprite correctDown;
	public Sprite correctRight;
	public Sprite correctLeft;
	public Sprite correctLoop;
	Sprite defaultNoInputSprite;

	int blockInputtedByPlayer;//count how many blocks are inputtedArray
	public GameObject[] emptySpaceForInput;//boxes that shpw input
	public GameObject[] bckEmptySpaceForInput;//bckgrnd of boxes

	CodeBlock blockScript;
	bool platformMoving;

	public SpriteRenderer wire;
	public Sprite wireOn;

	bool inputStarted;

	Color platformStndrdColour;
	Color platformMovigColour;
	Color platColour;

	void Start () 
	{
		i = 0;
		blockInputtedByPlayer = 0;
		buttonScript = button.GetComponent<ButtonBehav> ();

		maxInput = emptySpaceForInput.Length;
		moveArray = new string[maxInput];

		defaultNoInputSprite = emptySpaceForInput [0].GetComponent<SpriteRenderer> ().sprite;
		defaultObjPos = grabberArrow.transform.position;
		timeToReturnToOrg = maxInput * 2.0f;

		platformStndrdColour = new Color (1, 0.92f, 0.016f, 1);
		platformMovigColour = new Color (0, 1, 0, 1);
	}
	// Update is called once per frame
	void Update () 
	{
		if ((buttonScript.player1BesideButton && Input.GetButtonDown("Tet1")) || (buttonScript.player2BesideButton && Input.GetButtonDown("Tet2")) )
		{
			StartCoroutine ("ExecuteCode");
			blockInputtedByPlayer = 0;
			i = 0;
		}//end of if for button e

		if (moveDown)
		{
			transport = new Vector2 (grabberArrow.transform.position.x, grabberArrow.transform.position.y - amountToMove);
			//rb.MovePosition (transport);
			grabberArrow.transform.position = Vector3.Lerp (grabberArrow.transform.position, transport, Time.deltaTime);
		}
		if (moveUp)
		{
			transport = new Vector2 (grabberArrow.transform.position.x, grabberArrow.transform.position.y + amountToMove);
			//rb.MovePosition (transport);
			grabberArrow.transform.position = Vector3.Lerp (grabberArrow.transform.position, transport, Time.deltaTime);
		}
		if (moveRight)
		{
			transport = new Vector2 (grabberArrow.transform.position.x + amountToMove, grabberArrow.transform.position.y);
			//rb.MovePosition (transport);
			grabberArrow.transform.position = Vector3.Lerp (grabberArrow.transform.position, transport, Time.deltaTime);
		}
		if (moveLeft)
		{
			transport = new Vector2 (grabberArrow.transform.position.x - amountToMove, grabberArrow.transform.position.y);
			//rb.MovePosition (transport);
			grabberArrow.transform.position = Vector3.Lerp (grabberArrow.transform.position, transport, Time.deltaTime);
		}

		if (returnToOrgPos)
		{
			transport = defaultObjPos;
			//rb.MovePosition (transport);
			grabberArrow.transform.position = Vector3.Lerp (grabberArrow.transform.position, transport, Time.deltaTime);
		}
			
		if (platformMoving) 
		{

			if (aboutToMoveBackToOrg)//should flash wehn about to move back
			{

				platColour = grabberArrow.GetComponent<SpriteRenderer> ().color;
				if (platColour == platformMovigColour) 
				{
					grabberArrow.GetComponent<SpriteRenderer> ().color = platformStndrdColour;
				}
				else 
				{
					grabberArrow.GetComponent<SpriteRenderer> ().color = platformMovigColour;
				}

			}
			else //its moving to its position
			{
				grabberArrow.GetComponent<SpriteRenderer> ().color = platformMovigColour;
			}
		}
		else if (!platformMoving) 
		{
			grabberArrow.GetComponent<SpriteRenderer> ().color = new Color (1, 0.92f, 0.016f, 1);
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


		if (inputStarted && !platformMoving) 
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
				moveArray [i] = "lp";
				SpawnInputCode (inputLoop);
				Destroy (coll.gameObject);
			}
			i++;
		}

	}

	public IEnumerator ExecuteCode ()
	{
		Sprite defaultSpriteImage;
		SpriteRenderer spriteFromInputtedCode;

		platformMoving = true;
		pipeLight.SetActive (false);

		string[] inputsGoneThrough = new string[maxInput];//for loop

		if (moveArray [0] != "") //there is input there
		{
			for (int j = 0; j < maxInput; j++) 
			{
				Debug.Log (moveArray [j]);
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
				else if (moveArray [j] == "lp") 
				{
					spriteFromInputtedCode.sprite = correctLoop;
					Debug.Log ("loop");
					for (int z = 0; z < j; z++) 
					{
						SpriteRenderer spriteForInput = emptySpaceForInput [z].GetComponent<SpriteRenderer> ();
						Sprite defaultImageForInput = spriteForInput.sprite;

						if (inputsGoneThrough [z] == "d") 
						{
							moveDown = true;
							spriteForInput.sprite = correctDown;
						}
						else if (inputsGoneThrough [z] == "u")
						{
							moveUp = true;
							spriteForInput.sprite = correctUp;
						}
						else if (inputsGoneThrough [z] == "l")
						{
							moveLeft = true;
							spriteForInput.sprite = correctLeft;
						}
						else if (inputsGoneThrough [z] == "r") 
						{
							moveRight = true;
							spriteForInput.sprite = correctRight;
						}

						yield return new WaitForSeconds (2f);
						moveDown = false;
						moveUp = false;
						moveLeft = false;
						moveRight = false;
						spriteForInput.sprite = defaultImageForInput;
					}//end of for loop for lp
				}//end of lp if statement


				yield return new WaitForSeconds (2f);
				moveDown = false;
				moveUp = false;
				moveLeft = false;
				moveRight = false;
				inputsGoneThrough [j] = moveArray [j];
				moveArray [j] = "";
				emptySpaceForInput [j].GetComponent<SpriteRenderer> ().sprite = defaultSpriteImage;
			}//end of for

			yield return new WaitForSeconds (.5f);
			for (int z = 0; z <= emptySpaceForInput.Length - 1; z++) 
			{
				emptySpaceForInput [z].GetComponent<SpriteRenderer> ().sprite = defaultNoInputSprite;
			}//end of for for getting rid of blocks afer execution
		}

		if (transform.parent.gameObject.name != "firstcodeArray") //returns to orginal position after timer
		{
			yield return new WaitForSeconds (3f);
			aboutToMoveBackToOrg = true;
			yield return new WaitForSeconds (1f);
			aboutToMoveBackToOrg = false;
			returnToOrgPos = true;
			yield return new WaitForSeconds (timeToReturnToOrg);
			aboutToMoveBackToOrg = false;
			returnToOrgPos = false;;
		}
			
		platformMoving = false;
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
			if(inputtedBlock==inputLoop)
			{
				emptySpaceForInput [blockInputtedByPlayer].GetComponent<SpriteRenderer> ().sprite =loopBox;
			}

			blockInputtedByPlayer++;
		} 
	}
}
