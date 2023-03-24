/**
*triggers drones to show speech bubbles when players are near
*/
using UnityEngine;
using System.Collections;



public class TriggerScript : MonoBehaviour 
{
	
	bool shownTutForP1;
	bool shownTutForP2;

	int buttonCount=0; //counts the time buttons pressed

	public bool controllersInputted;

	public GameObject TutCompBttn;
	public GameObject tutControllerBttn;

	public GameObject pipeLight;
	// Use this for initialization
	void Start ()
	{
		
	}

	// Update is called once per frame
	void Update () 
	{
		
		if (gameObject.name.Contains ("machineTut")) 
		{  //code block machine, keeps button flashing for 3 presses

			if ((Input.GetButtonDown ("Tet1") && shownTutForP1) || (Input.GetButtonDown ("Tet2") && shownTutForP2)) 
			{ //pressed button
				buttonCount++;
			}
			if (buttonCount > 2) 
			{
				if (controllersInputted)
				{
					tutControllerBttn.SetActive (false);
				}
				else if (!controllersInputted) 
				{
					TutCompBttn.SetActive (false);
				}
			}
		}	


		if (gameObject.name.Contains ("JumpButton")) 
		{
			if ( (Input.GetButtonDown ("Jump1") && shownTutForP1) || (Input.GetButtonDown ("Jump2") && shownTutForP2) ) //pressed button
			{  
				if (controllersInputted)
				{
					tutControllerBttn.SetActive (false);
				}
				else if (!controllersInputted) 
				{
					TutCompBttn.SetActive (false);
				}
			}
		} //end of jump button

		if (gameObject.name.Contains ("Interact")) 
		{
			if ((Input.GetButtonDown ("Tet1") && shownTutForP1) || (Input.GetButtonDown ("Tet2") && shownTutForP2)) 
			{  
				if (controllersInputted)
				{
					tutControllerBttn.SetActive (false);
				}
				else if (!controllersInputted) 
				{
					TutCompBttn.SetActive (false);
				}
			}
		}//end of interact button

		if (gameObject.name.Contains ("pickUpTut")) 
		{
			if ( (Input.GetButtonDown ("Primary1") && shownTutForP1) || (Input.GetButtonDown ("Primary2") && shownTutForP2) ) 
			{  

				pipeLight.SetActive (true);

				buttonCount++;
			}
			if (buttonCount > 2) 
			{
				if (controllersInputted)
				{
					tutControllerBttn.SetActive (false);
				}
				else if (!controllersInputted) 
				{
					TutCompBttn.SetActive (false);
				}
			}
		}//end of pick up


	}


	void OnTriggerEnter2D(Collider2D col)
	{
		
		//check of theres controllers present
		if (Input.GetJoystickNames().Length > 0) 
		{
			controllersInputted = true;
			Debug.Log ("controllers");
		}
		else
		{
			controllersInputted = false;
		}


		if (col.gameObject.name == "Player1") 
		{

			shownTutForP1=true;
			if (controllersInputted)
			{
				tutControllerBttn.SetActive (true);
			}
			else if (!controllersInputted) 
			{
				TutCompBttn.SetActive (true);
			}
		}
		if (col.gameObject.name == "Player2") 
		{
			
			shownTutForP2=true;
			if (controllersInputted)
			{
				tutControllerBttn.SetActive (true);
			}
			else if (!controllersInputted) 
			{
				TutCompBttn.SetActive (true);
			}
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.name == "Player1" && !shownTutForP2) 
		{
			shownTutForP1 = false;
			if (controllersInputted) 
			{
				tutControllerBttn.SetActive (false);
			}
			else if (!controllersInputted) 
			{
				TutCompBttn.SetActive (false);
			}
		}
		if (col.gameObject.name == "Player2" && !shownTutForP1) 
		{
			shownTutForP2 = false;
			if (controllersInputted) 
			{
				tutControllerBttn.SetActive (false);
			}
			else if (!controllersInputted) 
			{
				TutCompBttn.SetActive (false);
			}
		}
	}
}
