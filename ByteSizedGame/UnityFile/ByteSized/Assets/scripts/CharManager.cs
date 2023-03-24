/**
*Managees player poisitions, keeps track of checkpoints hit and activates warning light when strayed to far. Deals with gameMang stuff within the scene
*lang, coins
*/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharManager : MonoBehaviour 
{
	public GameObject player1;
	CharacterMovement1 charMove1;

	public GameObject player2;
	CharacterMovement2 charMove2;

	bool bothPlayersHit;

	Vector3 camStartPos;
	Vector3 player1StartPos;
	Vector3 player2StartPos;

	public Vector3 lastCheckPoint; //position of last check point passed

	/*TIMER STUFF*/
	Light warningLight; //light that flashes
	float countDown; //starts when players seperate
	public GameObject timerBox; //shows countdown timer to player

	public GameObject p1Timer; //shows countdown for respawn timer to player
	public GameObject p2Timer;

	float fadeSpeed =5.0f;
	float highIntensity = 4.0f;//highest light will flash
	float lowIntensity = 0.0f;//lowest light will flash
	float changeMarg = 0.2f;
	float targetIntensity;//whether the light is growing brighter or not
	bool warningLightOn;

	Color collideColor = new Color(255,0,0,255); //red hit colour
	Color normalColor = new Color(255f,255f,255f,255f); //players normal sprite colour
	public ParticleSystem particleEffect;

	public AudioSource audioSrc;
	public AudioClip playerHit;

	/*CAM SHAKE STUFF*/
	bool shakeCam;
	public float shakeDuration;
	public float shakeAmnt = .7f;
	public float decreaseFactor =1.0f;
	Vector2 orgCamPos;

	/*GAME MANGING STUFF*/
	GameMang gameMangScript; 
	GameObject gameMangObject;

	public bool englishSelected;
	public GameObject openingSciIrish;
	public GameObject openingSciEnglish;
	public GameObject label;//label of lang dropdown menu
	public GameObject dropDownMenu; //dropdown menu that hold the lang options

	public int coinCount;
	public GameObject coinScore;//object that shows player their coinscore


	// Use this for initialization
	void Start ()
	{
		Vector3 camStartPos=transform.position;
		Vector3 player1StartPos=player1.transform.position;
		Vector3 player2StartPos=player2.transform.position;
		lastCheckPoint = player1StartPos;

		charMove1 = player1.GetComponent<CharacterMovement1> ();
		charMove2 = player2.GetComponent<CharacterMovement2> ();

		warningLight = GetComponent<Light> ();
		targetIntensity = highIntensity;
		countDown = 10.0f;

		gameMangObject= GameObject.FindGameObjectWithTag ("GameController");
		gameMangScript = gameMangObject.GetComponent<GameMang> ();

		/*sets the value of the lang dropdown menu in the pause screen based on the players lang choice*/
		if (!gameMangScript.engSelected) //irish
		{
			openingSciIrish.SetActive (true);
			englishSelected = false;
			dropDownMenu.GetComponent<Dropdown> ().value = 1;
		}
		else
		{
			openingSciEnglish.SetActive (true);
			englishSelected = true;
			dropDownMenu.GetComponent<Dropdown> ().value = 0;
		}
		coinCount = gameMangScript.coinScore;//coincount will 

	}
	
	// Update is called once per frame
	void Update ()
	{
		gameMangScript.coinScore = coinCount;
		coinScore.GetComponent<Text>().text = coinCount.ToString ();//Displays coin count

		if (shakeCam) 
		{
			Vector2 randPos2D = orgCamPos + Random.insideUnitCircle * shakeAmnt;//moves cam to rand pos inside of a set circle
			Vector3 randPos = new Vector3 (randPos2D.x, randPos2D.y, -1);
			GetComponent<Transform> ().localPosition = randPos;
		}
		if (charMove1.hitEnemy && charMove2.hitEnemy) //both players hit
		{
			ReturnPlayersToCheckPoint ();
		}
		if (charMove1.hitEnemy && !charMove1.respawning) //player1 hit
		{
			player1.SetActive (false);
			StartCoroutine(RevivePlayer (player1));
		}
		if (charMove2.hitEnemy&& !charMove2.respawning) //player2 hit
		{
			player2.SetActive (false);
			StartCoroutine(RevivePlayer (player2));
		}

		float playerDist = Vector3.Distance (player1.transform.position,player2.transform.position);

		if (playerDist > 19.0f) //far apart
		{ 
			//start count down
			timerBox.SetActive (true);
			countDown -= Time.deltaTime;
			timerBox.GetComponent<Text> ().text = (Mathf.Round (countDown)).ToString ();

			warningLight.intensity = Mathf.Lerp (warningLight.intensity,targetIntensity, fadeSpeed * Time.deltaTime);
			CheckIntensity ();

			if (countDown <= 0) 
			{
				ReturnPlayersToCheckPoint ();
			}
		}
		else if(playerDist<19.0f)
		{
			countDown = 10.0f;//makes sure countdown starts at ten each time
			timerBox.SetActive (false);
			warningLight.intensity = Mathf.Lerp (warningLight.intensity, 0f, fadeSpeed * Time.deltaTime);
		}
			
	}

	/*Respawns player after set time, flashes the player as they respawn*/
	IEnumerator RevivePlayer(GameObject player)
	{
		if (!audioSrc.isPlaying)
		{
			audioSrc.clip = playerHit;
			audioSrc.Play ();
		}
		orgCamPos = GetComponent<Transform> ().localPosition;//sets the cams pos at the time
		shakeCam = true;
		Instantiate (particleEffect, player.transform.position, transform.rotation);


		for (int i = 3; i > 0; i--) //respawn timer
		{
			if (player == player1) 
			{
				p2Timer.SetActive (true);
				p2Timer.transform.GetChild (0).GetComponent<Text> ().text = i.ToString ();//shows what i currently is as the seconds till player respawns
			}
			else if (player == player2) 
			{
				p1Timer.SetActive (true);
				p1Timer.transform.GetChild (0).GetComponent<Text> ().text = i.ToString ();//shows what i currently is as the seconds till player respawns
			}
			yield return new WaitForSeconds(1f);
		}

		shakeCam = false;
		player.SetActive (true); //player respawned

		/*FLASHES THE PLAYERS SPRITE COLOUR*/
		if (player == player1) 
		{
			p2Timer.SetActive (false);
			charMove1.respawning = true; //set so they cant get hit by enemies as they're respawning
			charMove1.hitEnemy = false;

			for (int i = 0; i < 2; i++) 
			{
				player.GetComponent<SpriteRenderer> ().color = collideColor;
				yield return new WaitForSeconds (0.1f);
				player.GetComponent<SpriteRenderer> ().color = normalColor;
				yield return new WaitForSeconds (0.1f);
			}

			charMove1.respawning = false;
		}
		else if (player == player2)
		{
			p1Timer.SetActive (false);
			charMove2.respawning = true;
			charMove2.hitEnemy = false;

			for (int i = 0; i < 2; i++) 
			{
				player.GetComponent<SpriteRenderer> ().color = collideColor;
				yield return new WaitForSeconds (0.1f);
				player.GetComponent<SpriteRenderer> ().color = normalColor;
				yield return new WaitForSeconds (0.1f);
			}

			charMove2.respawning = false;
		}

			
	}

	/*brings players back to the last checpoint they passed*/
	void ReturnPlayersToCheckPoint()
	{
		
		player1.transform.position = lastCheckPoint;
		player2.transform.position = lastCheckPoint;
		transform.position = new Vector3 (lastCheckPoint.x, lastCheckPoint.y, -10);
	}

	/*changes target intensity for warning light*/
	void CheckIntensity()
	{
		if (Mathf.Abs (targetIntensity - warningLight.intensity) < changeMarg) 
		{
			if (targetIntensity == highIntensity) 
			{
				targetIntensity = lowIntensity;
			}
			else 
			{
				targetIntensity = highIntensity;
			}
		}
	}

	/*changes lang of game in scene and on game mang object based off option in pause menu*/
	public void ChangeLang()
	{
		if (label.GetComponent<Text> ().text.Contains ("English"))
		{
			gameMangScript.engSelected = true;
			englishSelected = true;
		}
		else
		{
			gameMangScript.engSelected = false;
			englishSelected = false;
		}
	}
		
}
