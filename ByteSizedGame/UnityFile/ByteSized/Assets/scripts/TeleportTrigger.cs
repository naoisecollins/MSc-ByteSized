/**
*Trigger scene change when players enter teleport && sends analytics
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class TeleportTrigger : MonoBehaviour 
{

	public GameObject p1;
	CharacterMovement1 charScript;
	public GameObject p2;
	CharacterMovement2 charScript2;

	bool player1AtTeleporter;
	bool player2AtTeleporter;

	public GameObject camWSceneChanges;
	SceneChanges sceneScript;

	public GameObject collidersOfTeleport;

	Scene scene;
	bool lvl1Finished;
	bool lvl2Finished;
	bool lvl3Finished;

	AudioSource audioSrc;

	public bool isTeleporting;
	Color teleportColor = new Color(0,0,0,0);
	Color stndrdColor;

	// Use this for initialization
	void Start () 
	{
		stndrdColor = p1.GetComponent<SpriteRenderer> ().color;

		sceneScript = camWSceneChanges.GetComponent<SceneChanges> ();
		scene = SceneManager.GetActiveScene ();

		charScript = p1.GetComponent<CharacterMovement1> ();
		charScript2 = p2.GetComponent<CharacterMovement2> ();

		audioSrc = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (player1AtTeleporter && player2AtTeleporter)//only teleports when both players in teleporter
		{
			StartCoroutine ("Teleport");
		}
	}

	IEnumerator Teleport ()
	{
		isTeleporting = true;
		collidersOfTeleport.SetActive(true);//locks players in teleporter
		if (!audioSrc.isPlaying) 
		{
			audioSrc.Play ();
		}

		for (int i = 0; i < 5; i++) //flashes players
		{
			p1.GetComponent<SpriteRenderer> ().color = teleportColor;
			p2.GetComponent<SpriteRenderer> ().color = teleportColor;
			yield return new WaitForSeconds (0.1f);
			p1.GetComponent<SpriteRenderer> ().color = stndrdColor;
			p2.GetComponent<SpriteRenderer> ().color = stndrdColor;
			yield return new WaitForSeconds (0.1f);
		}
		//yield return new WaitForSeconds (2f);


		if (scene.name == "Sequencing") 
		{
			string levelName = scene.name + " player1HitCount";
			string levelName2 = scene.name + " player2HitCount";
			UnityEngine.Analytics.Analytics.CustomEvent("EnemyHits", new Dictionary<string, object>
				{
					{ levelName, charScript.enemyHitCount },
					{ levelName2, charScript2.enemyHitCount}
				});

			UnityEngine.Analytics.Analytics.CustomEvent("lvlFinished", new Dictionary<string, object>
				{
					{scene.name, sceneScript.startedGame },
				});

			sceneScript.StartGameButton ("lvl2");
		}

		if (scene.name == "Loops") 
		{

			string levelName = scene.name + " player1HitCount";
			string levelName2 = scene.name + " player2HitCount";
			UnityEngine.Analytics.Analytics.CustomEvent("EnemyHits", new Dictionary<string, object>
				{
					{ levelName, charScript.enemyHitCount },
					{ levelName2, charScript2.enemyHitCount}
				});

			UnityEngine.Analytics.Analytics.CustomEvent("lvlFinished", new Dictionary<string, object>
				{
					{scene.name, sceneScript.startedGame },
				});

			sceneScript.StartGameButton ("lvl3");
		}

		if(scene.name == "Functions")
		{
			string levelName = scene.name + " player1HitCount";
			string levelName2 = scene.name + " player2HitCount";
			UnityEngine.Analytics.Analytics.CustomEvent("EnemyHits", new Dictionary<string, object>
				{
					{ levelName, charScript.enemyHitCount },
					{ levelName2, charScript2.enemyHitCount}
				});

			UnityEngine.Analytics.Analytics.CustomEvent("lvlFinished", new Dictionary<string, object>
				{
					{scene.name, sceneScript.startedGame },
				});

			sceneScript.StartGameButton ("endGame");
		}


	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.name == "Player1") 
		{
			Debug.Log ("p1");
			player1AtTeleporter = true;

		}
		if (col.gameObject.name == "Player2") 
		{
			Debug.Log ("p2");
			player2AtTeleporter = true;

		}
	}
	void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Player") 
		{
			Debug.Log ("p1 left");
			player1AtTeleporter = false;
		}
		if (coll.gameObject.name == "Player2") 
		{
			player2AtTeleporter = false;
		}
	}
}

