/*
 *Spawns prefabs of data blocks for datablock machine 
*/
using UnityEngine;
using System.Collections;

public class BlockSpawning : MonoBehaviour
{
	/*which button showing*/
	bool onUpButt;
	bool onDownButt;
	bool onLftButt;
	bool onRghtButt;

	/*data block prefabs*/
	public GameObject leftArrowPref;
	public GameObject rightArrowPref;
	public GameObject upArrowPref;
	public GameObject downArrowPref;

	public SpriteRenderer sr;//screen that shows which arrow player is on

	public Sprite up;
	public Sprite down;
	public Sprite right;
	public Sprite left;

	public Sprite[] spritesArray; 
	int arrayCount;

	AudioSource audioSrc;
	public AudioClip [] Cycle ;
	public AudioClip BlockSpawn;

	public GameObject bttn;
	ButtonBehav bttnScript;

	public GameObject lever;
	ButtonBehav bttnScript1;

	// Use this for initialization
	void Start ()
	{
		arrayCount = 0;
		audioSrc = GetComponent<AudioSource> ();

		bttnScript = bttn.GetComponent<ButtonBehav> ();
		bttnScript1 = lever.GetComponent<ButtonBehav> ();
		//sr.sprite = up;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if ( (bttnScript1.player1BesideButton && Input.GetButtonDown("Tet1") ) || ( bttnScript1.player2BesideButton && Input.GetButtonDown("Tet2") ) ) 
		{
			
			audioSrc.clip = Cycle[Random.Range(0, Cycle.Length)];
			audioSrc.Play ();
			ChangeBlock ();
		}

		if ( (bttnScript.player1BesideButton && Input.GetButtonDown("Tet1") ) || ( bttnScript.player2BesideButton && Input.GetButtonDown("Tet2") ) )
		{
			audioSrc.clip = BlockSpawn;
			audioSrc.Play ();
			SpawnBlock ();
		}



	}


	/*Changes screen that shows arrow to player*/
	void ChangeBlock()
	{

		sr.sprite = spritesArray[arrayCount];
		if (sr.sprite == up)//on up sprite
		{
			onUpButt = true;
			onDownButt = false;
			onRghtButt = false;
			onLftButt = false;
		}
		else if (sr.sprite == down)//on down sprite
		{
			onUpButt = false;
			onDownButt = true;
			onRghtButt = false;
			onLftButt = false;
		}
		else if (sr.sprite == right)//on right sprite
		{
			onUpButt = false;
			onDownButt = false;
			onRghtButt = true;
			onLftButt = false;
		}
		else if (sr.sprite == left)//on left sprite
		{
			onUpButt = false;
			onDownButt = false;
			onRghtButt = false;
			onLftButt = true;

		}

		//reset count
		if (arrayCount == spritesArray.Length - 1) 
		{
			arrayCount = 0;
		} 
		else
		{
			arrayCount++;
		}

	}
		

	/*spawns prefab based on what arrow is showiing*/
	void SpawnBlock()
	{
		GameObject codeBlock;//block that will be spawned
		Vector3 blockPos = new Vector3 (transform.position.x, transform.position.y-.97f, transform.position.z );
		if (onDownButt) 
		{
			codeBlock = (Instantiate (downArrowPref, blockPos, transform.rotation)) as GameObject;

		} 
		else if (onUpButt) 
		{
			codeBlock = (Instantiate (upArrowPref, blockPos, transform.rotation)) as GameObject;

		}
		else if (onLftButt) 
		{
			codeBlock = (Instantiate (leftArrowPref, blockPos, transform.rotation)) as GameObject;

		} 
		else if (onRghtButt) 
		{
			codeBlock = (Instantiate (rightArrowPref, blockPos, transform.rotation)) as GameObject;

		}
			
	}
		

}


