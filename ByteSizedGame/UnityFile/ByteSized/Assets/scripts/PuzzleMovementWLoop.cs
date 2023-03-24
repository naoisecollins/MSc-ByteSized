/**
*Moves platform based on datablocks inputted by player w loops
*/
using UnityEngine;
using System.Collections;

public class PuzzleMovementWLoop : MonoBehaviour 
{
	public GameObject moveablePlatform;
	public GameObject pipeLight;

	public GameObject button;
	ButtonBehav buttonScript;

	bool inputStarted;

	string[] moveArray;
	int i; //count how many blocks are there
	int maxInput; //how many blocks players can use

	Vector2 defaultPlatPos;
	Vector2 transport;
	//Rigidbody2D rb;


	float orginalXPos;
	public float force= 3.0f;
	bool cantMove;
	bool moveDown;
	bool moveUp;
	bool moveLeft;
	bool moveRight;
	bool returnToOrgPos;
	bool aboutToMoveBackToOrg;

	public float amountToMove; 
	float timeToReturnToOrg;

	public GameObject inputUp;
	public GameObject inputDown;
	public GameObject inputLeft;
	public GameObject inputRight;
	public GameObject inputLoop;
	int blockInputtedByPlayer;//count how many blocks are inputtedArray
	public GameObject[] emptySpaceForInput;//boxes that shpw input
	public GameObject[] bckEmptySpaceForInput;//bckgrnd of boxes

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


	CodeBlock blockScript;

	bool platformMoving;

	AudioSource audiosound; //pipe
	public AudioClip blockEnter;
	AudioSource movingPlatSound; //platform
	public AudioClip moving; 
	public AudioClip movingUp;
	public AudioClip movingDown;

	public SpriteRenderer wire;
	Sprite wireoff;
	public Sprite wireOn;
	Color platformStndrdColour;
	Color platformMovigColour;
	Color platColour;
	Color pipeOffColour;

	void Start () 
	{
		i = 0;
		blockInputtedByPlayer = 0;
		maxInput = emptySpaceForInput.Length;
		moveArray = new string[maxInput];

		buttonScript = button.GetComponent<ButtonBehav> ();

		//rb = moveablePlatform.GetComponent<Rigidbody2D> ();
		defaultNoInputSprite = emptySpaceForInput [0].GetComponent<SpriteRenderer> ().sprite;

		orginalXPos = moveablePlatform.transform.position.x;
		defaultPlatPos = moveablePlatform.transform.position;
		timeToReturnToOrg = maxInput * 2.0f;

		platformStndrdColour = new Color (1, 0.92f, 0.016f, 1);
		platformMovigColour = new Color (0, 1, 0, 1);
		pipeOffColour = moveablePlatform.GetComponent<SpriteRenderer>().color;

		moveablePlatform.GetComponent<SpriteRenderer>().color = pipeOffColour;
		moveablePlatform.transform.GetChild (0).GetComponent<SpriteRenderer> ().color = pipeOffColour;

		wireoff = wire.sprite;

		audiosound = GetComponent<AudioSource> ();
		movingPlatSound = moveablePlatform.GetComponent<AudioSource> ();

	}
	// Update is called once per frame
	void Update () 
	{

		if (!platformMoving && (buttonScript.player1BesideButton && Input.GetButtonDown("Tet1")) || (buttonScript.player2BesideButton && Input.GetButtonDown("Tet2")) ) 
		{
			StartCoroutine ("ExecuteCode");
			for (int z = 0; z < maxInput; z++) 
			{
				bckEmptySpaceForInput [z].GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 1);
			}
			blockInputtedByPlayer = 0;
			i = 0;
		}

		if (moveDown)
		{
			transport = new Vector2 (moveablePlatform.transform.position.x, moveablePlatform.transform.position.y - amountToMove);
			//rb.MovePosition (transport);
			moveablePlatform.transform.position = Vector3.Lerp (moveablePlatform.transform.position, transport, Time.deltaTime);

			if (!movingPlatSound.isPlaying) 
			{
				movingPlatSound.clip = movingDown;
				movingPlatSound.Play ();
			}
		}
		if (moveUp)
		{
			transport = new Vector2 (moveablePlatform.transform.position.x, moveablePlatform.transform.position.y + amountToMove);
			//rb.MovePosition (transport);
			moveablePlatform.transform.position = Vector3.Lerp (moveablePlatform.transform.position, transport, Time.deltaTime);

			if (!movingPlatSound.isPlaying) 
			{
				movingPlatSound.clip = movingUp;
				movingPlatSound.Play ();
			}
		}
		if (moveRight)
		{
			transport = new Vector2 (moveablePlatform.transform.position.x + amountToMove, moveablePlatform.transform.position.y);
			//rb.MovePosition (transport);
			moveablePlatform.transform.position = Vector3.Lerp (moveablePlatform.transform.position, transport, Time.deltaTime);
			if (!movingPlatSound.isPlaying) 
			{
				movingPlatSound.clip = moving;
				movingPlatSound.Play ();
			}
		}
		if (moveLeft)
		{
			transport = new Vector2 (moveablePlatform.transform.position.x - amountToMove, moveablePlatform.transform.position.y);
			//rb.MovePosition (transport);
			moveablePlatform.transform.position = Vector3.Lerp (moveablePlatform.transform.position, transport, Time.deltaTime);
			if (!movingPlatSound.isPlaying) 
			{
				movingPlatSound.clip = moving;
				movingPlatSound.Play ();
			}
		}

		if (cantMove == true)
		{
			orginalXPos = moveablePlatform.transform.position.x;
			moveablePlatform.transform.position = new Vector3 ((orginalXPos + ((float)Mathf.Sin (Time.deltaTime) * force)), moveablePlatform.transform.position.y, moveablePlatform.transform.position.z);
		}
		if (returnToOrgPos)
		{
			transport = defaultPlatPos;
			//rb.MovePosition (transport);
			moveablePlatform.transform.position = Vector3.Lerp (moveablePlatform.transform.position, transport, Time.deltaTime);
			if (!movingPlatSound.isPlaying) 
			{
				movingPlatSound.clip = moving;
				movingPlatSound.Play ();
			}
		}

		/*COLOUR*/
		if (platformMoving) 
		{

			if (aboutToMoveBackToOrg)//should flash wehn about to move back
			{

				platColour = moveablePlatform.transform.GetComponent<SpriteRenderer> ().color;
				if (platColour == platformMovigColour) 
				{
					moveablePlatform.GetComponent<SpriteRenderer> ().color = platformStndrdColour;
					moveablePlatform.transform.GetChild (0).GetComponent<SpriteRenderer> ().color = platformStndrdColour;
				}
				else 
				{
					moveablePlatform.GetComponent<SpriteRenderer> ().color = platformMovigColour;
					moveablePlatform.transform.GetChild (0).GetComponent<SpriteRenderer> ().color = platformMovigColour;
				}
				//platColour = Color.Lerp (platformStndrdColour, platformMovigColour, Time.deltaTime*2.0f);

			}
			else //its moving to its position
			{
				moveablePlatform.GetComponent<SpriteRenderer> ().color = platformMovigColour;
				moveablePlatform.transform.GetChild (0).GetComponent<SpriteRenderer> ().color = platformMovigColour;
			}
		}
		else if (!platformMoving && !inputStarted) 
		{
			moveablePlatform.GetComponent<SpriteRenderer> ().color = pipeOffColour;
			moveablePlatform.transform.GetChild (0).GetComponent<SpriteRenderer> ().color = pipeOffColour;
		}

		if (i == maxInput)//resets so array never gets to big
		{
			i=0;
			Debug.Log ("i reset");
		}

		if (blockInputtedByPlayer == maxInput)
		{
			blockInputtedByPlayer =0;
			Debug.Log ("blocks reset");
		}


		if (emptySpaceForInput [0].GetComponent<SpriteRenderer> ().sprite != defaultNoInputSprite) 
		{
			inputStarted = true;
			wire.sprite = wireOn;
			if (!platformMoving)
			{
				moveablePlatform.GetComponent<SpriteRenderer> ().color = platformStndrdColour;
				moveablePlatform.transform.GetChild (0).GetComponent<SpriteRenderer> ().color = platformStndrdColour;
			}
		}
		else 
		{
			wire.sprite = wireoff;
			inputStarted = false;
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
		if (!platformMoving)
		{
			if (coll.gameObject.name.Contains ("Arrow"))
			{
				blockScript = coll.gameObject.GetComponent<CodeBlock> ();
				Debug.Log ("collision");
				if (coll.gameObject.name.Contains ("downArrow") && !blockScript.beingHeld) 
				{
					audiosound.clip = blockEnter;
					audiosound.Play ();
					moveArray [i] = "d";
					SpawnInputCode (inputDown);
					Destroy (coll.gameObject);

				}
				else if (coll.gameObject.name.Contains ("upArrow") && !blockScript.beingHeld) 
				{
					audiosound.clip = blockEnter;
					audiosound.Play ();
					moveArray [i] = "u";
					SpawnInputCode (inputUp);
					Destroy (coll.gameObject);
				}
				else if (coll.gameObject.name.Contains ("leftArrow") && !blockScript.beingHeld)
				{
					audiosound.clip = blockEnter;
					audiosound.Play ();
					moveArray [i] = "l";
					SpawnInputCode (inputLeft);
					Destroy (coll.gameObject);
				}
				else if (coll.gameObject.name.Contains ("rightArrow") && !blockScript.beingHeld)
				{
					audiosound.clip = blockEnter;
					audiosound.Play ();
					moveArray [i] = "r";
					SpawnInputCode (inputRight);
					Destroy (coll.gameObject);
				}
				else if (coll.gameObject.name.Contains ("loopsArrows") && !blockScript.beingHeld) 
				{
					audiosound.clip = blockEnter;
					audiosound.Play ();
					moveArray[i] = "lp";
					SpawnInputCode (inputLoop);
					Destroy (coll.gameObject);
				}
				i++;
			}
		}

	}

	public IEnumerator ExecuteCode ()
	{
		Sprite defaultSpriteImage;
		SpriteRenderer spriteFromInputtedCode;




		string[] inputsGoneThrough = new string[maxInput];//for loop

		if (moveArray [0] != "") //there is input there
		{

			pipeLight.SetActive (false);
			platformMoving = true;
			GetComponent<SpriteRenderer> ().color = pipeOffColour;

			for (int j = 0; j < maxInput; j++) 
			{
				Debug.Log (moveArray [j]);
				spriteFromInputtedCode = emptySpaceForInput [j].GetComponent<SpriteRenderer> ();
				defaultSpriteImage = spriteFromInputtedCode.sprite;

				/*XY PLATFORM*/
				if (moveablePlatform.gameObject.tag == "xyPlatform") 
				{
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
					/*LOOP IF FOR XY*/
					else if (moveArray [j] == "lp")
					{
						spriteFromInputtedCode.sprite = correctLoop;
						Debug.Log ("loop");
						for (int z = 0; z < j; z++) 
						{
							SpriteRenderer spriteForInput = emptySpaceForInput[z].GetComponent<SpriteRenderer> ();
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
							cantMove = false;
							spriteForInput.sprite =  defaultImageForInput;
						}//end of for loop for lp
					}//end of loop if statement for xy

				}//END OF XY PLATFORM IF

				/*Y PLATFORM*/
				if (moveablePlatform.gameObject.tag == "yPlatform") 
				{
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
						cantMove = true;
						spriteFromInputtedCode.sprite = correctLeft;
						Debug.Log ("should shake");
					}
					else if (moveArray [j] == "r")
					{
						cantMove = true;
						spriteFromInputtedCode.sprite = correctRight;
						Debug.Log ("should shake");
					}

					/*LOOP IF FOR Y*/
					else if (moveArray [j] == "lp")
					{
						spriteFromInputtedCode.sprite = correctLoop;
						Debug.Log ("loop");
						for (int z = 0; z < j; z++) 
						{
							SpriteRenderer spriteForInput = emptySpaceForInput[z].GetComponent<SpriteRenderer> ();
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
								cantMove = true;
								spriteForInput.sprite = correctLeft;
								Debug.Log ("should shake");
							}
							else if (inputsGoneThrough [z] == "r")
							{
								cantMove = true;
								spriteForInput.sprite = correctRight;
								Debug.Log ("should shake");
							}

							yield return new WaitForSeconds (2f);
							moveDown = false;
							moveUp = false;
							moveLeft = false;
							moveRight = false;
							cantMove = false;
							spriteForInput.sprite = defaultImageForInput;
						}//end of for loop for lp
					}//end of loop if statement

				}//END OF IF FOR Y

				/*IF FOR X*/
				if (moveablePlatform.gameObject.tag == "xPlatform") 
				{
					if (moveArray [j] == "l")
					{
						moveLeft = true;
						spriteFromInputtedCode.sprite = correctLeft;
					}
					else if (moveArray [j] == "r")
					{
						moveRight = true;
						spriteFromInputtedCode.sprite = correctRight;
					}
					else if (moveArray [j] == "u") 
					{
						cantMove = true;
						spriteFromInputtedCode.sprite = correctUp;
						Debug.Log ("should shake");
					}
					else if (moveArray [j] == "d") 
					{
						cantMove = true;
						spriteFromInputtedCode.sprite = correctDown;
					}
					/*IF LOOP FOR X*/
					else if (moveArray [j] == "lp")
					{
						spriteFromInputtedCode.sprite = correctLoop;
						Debug.Log ("loop");
						for (int z = 0; z < j; z++) 
						{
							SpriteRenderer spriteForInput = emptySpaceForInput[z].GetComponent<SpriteRenderer> ();
							Sprite defaultImageForInput = spriteForInput.sprite;


							if (inputsGoneThrough [z] == "d") 
							{
								cantMove= true;
								spriteForInput.sprite = correctDown;
							}
							else if (inputsGoneThrough [z] == "u")
							{
								cantMove = true;
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
							cantMove = false;
							spriteForInput.sprite = defaultImageForInput;
						}//end of for loop for lp
					}//end of loop if statement

				}//END OF IF FOR X

				yield return new WaitForSeconds (2f);
				moveDown = false;
				moveUp = false;
				moveLeft = false;
				moveRight = false;
				cantMove = false;
				inputsGoneThrough [j] = moveArray [j];
				moveArray [j] = "";
				emptySpaceForInput [j].GetComponent<SpriteRenderer> ().sprite = defaultSpriteImage;
			}//END OF FOR LOOP FOR WHOLE INPUT

			yield return new WaitForSeconds (.5f);
			for (int z = 0; z <= emptySpaceForInput.Length - 1; z++) 
			{
				emptySpaceForInput [z].GetComponent<SpriteRenderer> ().sprite = defaultNoInputSprite;
				inputsGoneThrough [z] = "";//empty out loop array
			}//end of for for getting rid of blocks afer execution

			if (transform.parent.gameObject.name != "firstcodeArray") //returns to orginal position after timer
			{
				yield return new WaitForSeconds (3f);
				aboutToMoveBackToOrg = true;
				yield return new WaitForSeconds (1f);
				aboutToMoveBackToOrg = false;
				returnToOrgPos = true;
				yield return new WaitForSeconds (timeToReturnToOrg);
				aboutToMoveBackToOrg = false;
				returnToOrgPos = false;
				GetComponent<SpriteRenderer> ().color = platformStndrdColour;
			}

			pipeLight.SetActive (true);
			platformMoving = false;

		}//END OF IF THERES INPUT
			
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
	// Use this for initialization

}
