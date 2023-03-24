/**
*Counts up all coins that collide into it and writes display to the screen
*/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndCoinScript : MonoBehaviour 
{
	AudioSource audioSrc;
	int count;

	public Text coinCount;//shows player the coin score
	// Use this for initialization
	void Start () 
	{
		audioSrc = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		coinCount.text = count.ToString ();
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.name.Contains ("coinPref")) 
		{
			count++;
			Destroy(col.gameObject);
			audioSrc.Play ();
		}
	}
}
