/**
*starts animation fors scientist on trigger enter
*/
using UnityEngine;
using System.Collections;

public class ScientistAnimationScript : MonoBehaviour 
{
	Animator anim; 
	// Use this for initialization
	void Start () 
	{
		anim = GetComponentInChildren<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.tag == "Player") 
		{
			anim.SetTrigger ("RTeleport");

		}

	}
}
