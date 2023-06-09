/**
*Moves platform based on datablocks inputted by player
*/
using UnityEngine;
using System.Collections;

public class PuzzleMovement : MonoBehaviour 
{
	public GameObject moveablePlatform;

	public GameObject button;
	ButtonBehav buttonScript;

	bool inputStarted;

	string[] moveArray;
	int i; //count how many blocks are there
	int maxInput; //how many blocks players can use
	Vector3 defaultPlatPos;
	Vector2 transport;
	//Rigidbody2D rb;

	public GameObject pipeLight;

	float orginalXPos;
	public float force= 3.0f;
	bool cantMove;
	bool moveDown;
	bool moveUp;
	bool moveLeft;
	bool moveRight;
	bool platformMoving;
	bool returnToOrgPos;
	bool aboutToMoveBackToOrg;


	public float amountToMove;
	float timeToReturnToOrg;

	public GameObject inputUp;
	public GameObject inputDown;
	public GameObject inputLeft;
	public GameObject inputRight;
	int blockInputtedByPlayer;//count how many blocks are inputtedArray
	public GameObject[] emptySpaceForInput;//boxes that show input
	public GameObject[] bckEmptySpaceForInput;

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


		audiosound = GetComponent<AudioSource> ();
		movingPlatSound = moveablePlatform.GetComponent<AudioSource> ();

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
			if (!movingPlatSound.isPlaying) 
			{
				movingPlatSound.clip = movingDown;
				movingPlatSound.Play ();
			}
			transport = new Vector2 (moveablePlatform.transform.position.x, moveablePlatform.transform.position.y - amountToMove);
			//rb.MovePosition (transport);
			moveablePlatform.transform.position = Vector3.Lerp (moveablePlatform.transform.position, transport, Time.deltaTime);
		}
		if (moveUp)
		{
			if (!movingPlatSound.isPlaying) 
			{
				movingPlatSound.clip = movingUp;
				movingPlatSound.Play ();
			}
			transport = new Vector2 (moveablePlatform.transform.position.x, moveablePlatform.transform.position.y + amountToMove);
			//rb.MovePosition (transport);
			moveablePlatform.transform.position = Vector3.Lerp (moveablePlatform.transform.position, transport, Time.deltaTime);
		}
		if (moveRight)
		{
			if (!movingPlatSound.isPlaying) 
			{
				movingPlatSound.clip = moving;
				movingPlatSound.Play ();
			}
			transport = new Vector2 (moveablePlatform.transform.position.x + amountToMove, moveablePlatform.transform.position.y);
			//rb.MovePosition (transport);
			moveablePlatform.transform.position = Vector3.Lerp (moveablePlatform.transform.position, transport, Time.deltaTime);
		}
		if (moveLeft)
		{
			if (!movingPlatSound.isPlaying) 
			{
				movingPlatSound.clip = moving;
				movingPlatSound.Play ();
			}
			transport = new Vector2 (moveablePlatform.transform.position.x - amountToMove, moveablePlatform.transform.position.y);
			//rb.MovePosition (transport);
			moveablePlatform.transform.position = Vector3.Lerp (moveablePlatform.transform.position, transport, Time.deltaTime);
		}

		if (cantMove == true)
		{
			
			moveablePlatform.transform.position = new Vector3 ((orginalXPos + ((float)Mathf.Sin (Time.deltaTime) * force)), moveablePlatform.transform.position.y, moveablePlatform.transform.position.z);
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

		if (returnToOrgPos)
		{
			if (!movingPlatSound.isPlaying) 
			{
				movingPlatSound.clip = moving;
				movingPlatSound.Play ();
			}
			transport = defaultPlatPos;
			//rb.MovePosition (transport);
			moveablePlatform.transform.position = Vector3.Lerp (moveablePlatform.transform.position, transport, Time.deltaTime);
		}

		if (i == maxInput)//resets so array never gets to big
		{
			i=0;
		}

		if (blockInputtedByPlayer == maxInput)
		{
			blockInputtedByPlayer =0;
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

		if (inputStarted && !platformMoving && transform.parent.gameObject.name != "firstcodeArray") 
		{
			bckEmptySpaceForInput[i].GetComponent<SpriteRenderer>().color=new Color(1,0.92f,0.016f,1);//highlight colour
			if (i != 0) 
			{
				bckEmptySpaceForInput [i - 1].GetComponent<SpriteRenderer> ().color = new Color (1,1, 1, 1);//normal colour
			}
			else if (i == 0) 
			{
				bckEmptySpaceForInput [maxInput-1].GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 1);
			}

		}
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (!platformMoving) //cant put blocks in while platform moving
		{
			if (coll.gameObject.name.Contains ("Arrow"))
			{
				blockScript = coll.gameObject.GetComponent<CodeBlock> ();
				if (coll.gameObject.name.Contains("downArrow")  && !blockScript.beingHeld) 
				{
					audiosound.clip = blockEnter;
					audiosound.Play ();
					moveArray[i]="d";
					SpawnInputCode(inputDown);
					Destroy (coll.gameObject);

				}
				else if (coll.gameObject.name.Contains("upArrow")  && !blockScript.beingHeld) 
				{
					audiosound.clip = blockEnter;
					audiosound.Play ();
					moveArray[i]="u";
					SpawnInputCode(inputUp);
					Destroy (coll.gameObject);
				}
				else if (coll.gameObject.name.Contains("leftArrow") && !blockScript.beingHeld) 
				{
					audiosound.clip = blockEnter;
					audiosound.Play ();
					moveArray[i]="l";
					SpawnInputCode(inputLeft);
					Destroy (coll.gameObject);
				}
				else if (coll.gameObject.name.Contains("rightArrow")  && !blockScript.beingHeld ) 
				{
					audiosound.clip = blockEnter;
					audiosound.Play ();
					moveArray[i]="r";
					SpawnInputCode(inputRight);
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

		if (moveArray [0] != "") //there is input there
		{
			platformMoving = true;
			pipeLight.SetActive (false);
			GetComponent<SpriteRenderer> ().color = pipeOffColour;

			for (int j = 0; j < maxInput; j++) 
			{
				spriteFromInputtedCode = emptySpaceForInput [j].GetComponent<SpriteRenderer> ();
				defaultSpriteImage = spriteFromInputtedCode.sprite;

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

				}//END OF Y PLAT

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
				}//END OF X PLAT

				if (moveablePlatform.gameObject.tag == "xyPlatform") 
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
						moveUp = true;
						spriteFromInputtedCode.sprite = correctUp;
					}
					else if (moveArray [j] == "d") 
					{
						moveDown= true;
						spriteFromInputtedCode.sprite = correctDown;
					}
				}//END OF XY PLAT

				yield return new WaitForSeconds (2f);
				moveDown = false;
				moveUp = false;
				moveLeft = false;
				moveRight = false;
				cantMove = false;
				moveArray [j] = "";
				emptySpaceForInput [j].GetComponent<SpriteRenderer> ().sprite = defaultSpriteImage;
				orginalXPos = moveablePlatform.transform.position.x;

			}//end of for

			movingPlatSound.clip = null;
			yield return new WaitForSeconds (.5f);
			for (int z = 0; z <= emptySpaceForInput.Length - 1; z++) 
			{
				emptySpaceForInput [z].GetComponent<SpriteRenderer> ().sprite = defaultNoInputSprite;
			}//end of for for getting rid of blocks afer execution

			if (transform.parent.gameObject.name != "firstcodeArray") 
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

			platformMoving = false;
			pipeLight.SetActive (true);



		}//end of if machine has input/
			
		movingPlatSound.Stop();


	}


	public void SpawnInputCode(GameObject inputtedBlock)
	{
		if (blockInputtedByPlayer < maxInput) 
		{

			if (inputtedBlock==inputDown)
			{
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
