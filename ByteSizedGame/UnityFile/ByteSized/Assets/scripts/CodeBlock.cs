/**
*Destorys codeBlock after time depending on whether object is held or not
*/
using UnityEngine;
using System.Collections;

public class CodeBlock : MonoBehaviour 
{
	public float destroyTimer=10f;
	public bool beingHeld;
	bool blockHeldAfterTimer;//restarts destroy timer

	public ParticleSystem particleEffect;

	// Use this for initialization
	void Start () 
	{
		if (gameObject.name != "antiVirus") 
		{
			StartCoroutine ("DestroyBlock");
		}
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (blockHeldAfterTimer)
		{
			if (!beingHeld) //players thrown block
			{
				StartCoroutine ("DestroyBlock");
			}
		}
	}

	IEnumerator DestroyBlock()
	{
		
		yield return new WaitForSeconds (destroyTimer);
		if (!beingHeld)//if not picked up
		{
			Instantiate (particleEffect, transform.position, transform.rotation);
			Destroy (gameObject);
		}
		else
		{
			blockHeldAfterTimer = true;
		}
	}
}
