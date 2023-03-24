/**
*Controls movement of enemies and their collisions
*/
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EnemyMove : MonoBehaviour 
{
	protected Vector3 velocity;
	public float distance = 5f;
	public float speed = 1f;
	public float distFromStart;

	Vector3 originalPosition;

	bool isGoingUp = false;
	bool isGoingLeft = false;

	Color collideColor;
	Color normalColor;

	public bool isBeingDestroyed;
	bool xMoving;
	bool yMoving;

	public ParticleSystem particleEffect;

	AudioSource audioSource;

	public void Start () 
	{
		
		collideColor = new Color(255,0,0,255);//red colour
		normalColor = new Color(255f,255f,255f,255f);//standard colour

		audioSource = GetComponent<AudioSource> ();

		originalPosition = gameObject.transform.position;

		if (gameObject.name.Contains ("Bat"))//up and down enemy
		{
			velocity = new Vector3(0,speed,0);
			transform.Translate (0, velocity.y * Time.deltaTime, 0);
			yMoving = true;
		}
		else//worm side to side enemy
		{
			velocity = new Vector3(speed,0,0);
			transform.Translate ( velocity.x*Time.deltaTime,0,0);
			xMoving = true;
		}

	}

	void Update()
	{    
		if (yMoving && !isBeingDestroyed)
		{ //enemy moves up and down freezes if being destroyed
			distFromStart = transform.position.y - originalPosition.y;   

			if (isGoingUp) 
			{ 
				if (distFromStart < -distance) 
				{
					SwitchDirection ();
				}

				transform.Translate (0, -velocity.y * Time.deltaTime, 0);
			}
			else 
			{
				if (distFromStart > distance) 
				{
					SwitchDirection ();
				}
				transform.Translate (0, velocity.y * Time.deltaTime, 0);
			}
		}
		else if (xMoving & !isBeingDestroyed)
		{
			distFromStart = transform.position.x - originalPosition.x;   

			if (isGoingLeft)
			{ 
				// If gone too far, switch direction
				if (distFromStart < -distance) 
				{
					SwitchDirection();
				}


				transform.Translate (-velocity.x * Time.deltaTime, 0, 0);
			}
			else
			{
				// If gone too far, switch direction
				if (distFromStart > distance) 
				{
					SwitchDirection();
				}


				transform.Translate (velocity.x * Time.deltaTime, 0, 0);
			}
		}

	}


	void SwitchDirection()
	{
		if (yMoving)
		{
			isGoingUp = !isGoingUp;
		}

		else if(xMoving)
		{
			isGoingLeft = !isGoingLeft;
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}

	}

	void OnCollisionEnter2D(Collision2D col)
	{
		           
		if (col.gameObject.name.Contains ("antiVirus")) 
		{
			if (!isBeingDestroyed) 
			{
				isBeingDestroyed = true;
				Destroy (col.gameObject);
				StartCoroutine(collideFlash());
			}

		}
	
	}

	IEnumerator collideFlash() 
	{
		audioSource.Play ();
		for (int j = 0; j < 6; j++) //flashes colour
		{
			GetComponent<SpriteRenderer> ().color = collideColor;
			yield return new WaitForSeconds (0.1f);
			GetComponent<SpriteRenderer> ().color = normalColor;
			yield return new WaitForSeconds (0.1f);

		} 
		Instantiate (particleEffect, transform.position, transform.rotation);
		Destroy (gameObject);
	}
}