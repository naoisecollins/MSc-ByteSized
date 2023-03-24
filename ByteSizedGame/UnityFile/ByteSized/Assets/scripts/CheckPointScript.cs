/**
*Sets last checpoint for CharManger and sends analytics of players progress
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Analytics;

public class CheckPointScript : MonoBehaviour 
{
	public Sprite passedCheck;//changes sprite to green to indicate passed
	SpriteRenderer sr;

	public GameObject cam;
	CharManager gameManager;

	public bool passedPoint;

	AudioSource audioSrc;
	// Use this for initialization
	void Start () 
	{
		gameManager = cam.GetComponent<CharManager> ();
		sr = GetComponent<SpriteRenderer> ();
		audioSrc = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () 
	{}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.name.Contains ("Player"))
		{
			sr.sprite = passedCheck;
			if (!passedPoint)//so checpoint doesn't repeat noise after passsing
			{
				audioSrc.Play ();
			}
			passedPoint = true;
			gameManager.lastCheckPoint = transform.position;

			UnityEngine.Analytics.Analytics.CustomEvent("checkPoint", new Dictionary<string, object>
				{
					{ gameObject.name, passedPoint },
				});
		}
	}
}
