/**
*Changes gravity of blue pieces in hardrive so they fall when hit
*/
using UnityEngine;
using System.Collections;

public class TurnStlyeScript : MonoBehaviour 
{

	public GameObject endPipe;
	EndPipeScript pipeScript;

	// Use this for initialization
	void Start () 
	{
		pipeScript = endPipe.GetComponent<EndPipeScript> ();
	}

	// Update is called once per frame
	void Update ()
	{
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.name == "turnStyle") 
		{
			GetComponent<Rigidbody2D>().gravityScale=1;
			transform.parent = null;
		}		

		if (coll.gameObject.name.Contains ("endPipe"))
		{
			Destroy(gameObject);
			pipeScript.pieceCounter++;
		}
	}
}
