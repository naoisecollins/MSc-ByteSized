/**
*Opens up speech bubble and scientist when trigger entered
*/
using UnityEngine;
using System.Collections;

public class SpeechTrigger : MonoBehaviour
{
	public GameObject textBox;
	public GameObject irishTextBox;
	Animator anim; 

	public GameObject mainCam;
	CharManager charMangScript;


	// Use this for initialization
	void Start () 
	{
		anim = GetComponentInParent<Animator> ();
		charMangScript =mainCam.GetComponent<CharManager> ();

	}
	
	// Update is called once per frame
	void Update ()
	{}


	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player") 
		{
			if (charMangScript.englishSelected) 
			{
				textBox.SetActive (true);
			}
			else if (!charMangScript.englishSelected) 
			{
				irishTextBox.SetActive(true);
			}
			anim.SetBool ("Teleport1", true );
		}
	}
	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player") 
		{
			if (gameObject.tag == "tutorialLight")
			{
				if (charMangScript.englishSelected) 
				{
					textBox.SetActive (false);
				}
				else if (!charMangScript.englishSelected) 
				{
					irishTextBox.SetActive(false);
				}
			}

		}
	}
}
