/*
 *Spawns just right blocks prefabs of data blocks for datablock machine
*/
using UnityEngine;
using System.Collections;

public class RightBlockMachineScript : MonoBehaviour
{


	public GameObject bttn;
	ButtonBehav bttnScript;

	public GameObject rightArrowPref;

	public AudioSource audiosound;
	public AudioClip BlockSpawn;
	// Use this for initialization
	void Start ()
	{
		AudioSource audio = GetComponent<AudioSource> ();
		bttnScript = bttn.GetComponent<ButtonBehav> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (  (bttnScript.player1BesideButton && Input.GetButtonDown("Tet1") ) || ( bttnScript.player2BesideButton && Input.GetButtonDown("Tet2") ) ) 
		{
			audiosound.clip = BlockSpawn;
			audiosound.Play ();
			SpawnBlock ();
		}
	}

	/*spawns prefab based on what arrow is showiing*/
	void SpawnBlock()
	{
		GameObject codeBlock;
		Vector3 blockPos = new Vector3 (transform.position.x, transform.position.y-.97f, transform.position.z );
		{
			codeBlock = (Instantiate (rightArrowPref, blockPos, transform.rotation)) as GameObject;
			Debug.Log ("onRightArrow");
		}
	}
		
}
