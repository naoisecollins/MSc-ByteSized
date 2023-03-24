/**
*Creates amount of coins players have collected throughout game and fires them into the air
*/
using UnityEngine;
using System.Collections;

public class endGameScript : MonoBehaviour 
{
	public GameObject p1;
	public GameObject p2;

	public AudioSource teleportAudioSrc;
	Color teleportColor = new Color(0,0,0,0);
	Color stndrdColor;

	public RuntimeAnimatorController girl;
	public RuntimeAnimatorController boy;
	Animator p1anim; 

	public RuntimeAnimatorController dog;
	public RuntimeAnimatorController cat;
	Animator p2anim; 

	public GameObject coinPref;
	public float speed=20f;

	GameMang gameMangScript; 
	GameObject gameMangObject;

	SceneChanges sceneChangeScript;
	// Use this for initialization
	void Start () 
	{
		stndrdColor = p1.GetComponent<SpriteRenderer> ().color;

		gameMangObject= GameObject.FindGameObjectWithTag ("GameController");
		gameMangScript = gameMangObject.GetComponent<GameMang> ();

		sceneChangeScript = GetComponent<SceneChanges> ();

		p1anim = p1.GetComponent<Animator> ();
		if (gameMangScript.girlChar)
		{
			p1anim.runtimeAnimatorController = girl;
		}
		else
		{
			p1anim.runtimeAnimatorController = boy;
		}

		p2anim = p2.GetComponent<Animator> ();
		if (gameMangScript.catChar)
		{
			p2anim.runtimeAnimatorController = cat;
		}
		else
		{
			p2anim.runtimeAnimatorController = dog;
		}

		teleportAudioSrc.Play ();
		StartCoroutine (Teleport ());
	}
	
	// Update is called once per frame
	void Update () {}

	IEnumerator Teleport()
	{
		
		for (int i = 0; i < 5; i++) //flashes players as they teleport onto the scene
		{
			p1.GetComponent<SpriteRenderer> ().color = teleportColor;
			p2.GetComponent<SpriteRenderer> ().color = teleportColor;
			yield return new WaitForSeconds (0.1f);
			p1.GetComponent<SpriteRenderer> ().color = stndrdColor;
			p2.GetComponent<SpriteRenderer> ().color = stndrdColor;
			yield return new WaitForSeconds (0.1f);
		}

		GameObject coin;

		//creates a new coin for every coin player collected throughout the game
		for (int i = 0; i < gameMangScript.coinScore; i++) 
		{
			Vector3 coinPos = new Vector3 (Random.Range(-2f, 2f), 1, 0);
			coin = Instantiate (coinPref, coinPos, transform.rotation) as GameObject;
			coin.GetComponent<Rigidbody2D> ().AddForce (transform.up*speed);
			yield return new WaitForSeconds (0.1f);
		}

		yield return new WaitForSeconds (2f);
		sceneChangeScript.ReturnToStart ();

	}
}
