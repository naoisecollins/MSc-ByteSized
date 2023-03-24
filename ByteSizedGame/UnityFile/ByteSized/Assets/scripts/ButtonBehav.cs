/*
*Plays button/lever animation, sets bool to show when which player is beside button
*/
using UnityEngine;
using System.Collections;

public class ButtonBehav : MonoBehaviour 
{

	public bool player1BesideButton;
	public bool player2BesideButton;

	bool leverNotButtn;
	Animator anim;

	AudioSource audiosound;


	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator>();
		audiosound = GetComponent<AudioSource> ();

		/*to chose which animation to use*/
		if (gameObject.name.Contains ("lever")) 
		{
			leverNotButtn = true;
		}
		else 
		{
			leverNotButtn = false;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{

		if (player1BesideButton && Input.GetButtonDown("Tet1") || player2BesideButton && Input.GetButtonDown("Tet2"))
		{
			if (!leverNotButtn) //bttn
			{ 
				StartCoroutine (WaitForAnimationToPlay ());
				audiosound.Play ();
				anim.SetBool ("pressed", true);
			}
			else if (leverNotButtn) //lever
			{
				audiosound.Play ();
				anim.SetTrigger("pull");
			}


		}
	}

	IEnumerator WaitForAnimationToPlay()
	{
		yield return new WaitForSeconds(1);
		anim.SetBool("pressed", false);
	}


	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.name == "Player1") 
		{
			player1BesideButton = true;
		}
		if (coll.gameObject.name == "Player2") 
		{
			player2BesideButton = true;
		}
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Player") 
		{
			player1BesideButton = false;
		}
		if (coll.gameObject.name == "Player2") 
		{
			player2BesideButton = false;
		}
	}
}
