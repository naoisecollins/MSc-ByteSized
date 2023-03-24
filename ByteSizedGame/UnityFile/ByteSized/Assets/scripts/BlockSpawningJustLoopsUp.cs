/*
 *Spawns just upp datablocks and loop prefabs of data blocks for datablock machine 
*/
using UnityEngine;
using System.Collections;

public class BlockSpawningJustLoopsUp : MonoBehaviour
{
	public GameObject bttn;
	ButtonBehav bttnScript;
	public GameObject lever;
	ButtonBehav bttnScript1;

	bool onUpButt;
	bool onLoopButt;

	public GameObject upArrowPref;
	public GameObject loopPref;

	public SpriteRenderer sr;

	public Sprite up;
	public Sprite loop;

	public Sprite[] spritesArray;
	int arrayCount;



	AudioSource audioSrc;
	public AudioClip [] Cycle ;
	public AudioClip BlockSpawn;

	// Use this for initialization
	void Start () 
	{
		arrayCount = 0;
		audioSrc = GetComponent<AudioSource> ();


		bttnScript = bttn.GetComponent<ButtonBehav> ();
		bttnScript1 = lever.GetComponent<ButtonBehav> ();
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
			onLoopButt = false;
		}
		else if (sr.sprite == loop) 
		{
			onUpButt = false;
			onLoopButt = true;
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
		GameObject codeBlock;
		Vector3 blockPos = new Vector3 (transform.position.x, transform.position.y - .97f, transform.position.z);
		if (onUpButt) 
		{
			codeBlock = (Instantiate (upArrowPref, blockPos, transform.rotation)) as GameObject;
		}
		else if (onLoopButt) 
		{
			codeBlock = (Instantiate (loopPref, blockPos, transform.rotation)) as GameObject;
		}
	}
}
