﻿/**
*Players controls and enemy collisions for player2(dog - player1 to players)
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class CharacterMovement2 : MonoBehaviour 
{
	/*Jump stuff*/
	bool grounded = false;
	bool canJumpAgain;
	public float speed = 10f;
	public float jumpf;
	public Transform groundCheck;
	float groundRadius = .2f;
	bool facingRight = true;
	private Rigidbody2D rigid;

	public LayerMask whatIsGround;

	Animator anim;
	public RuntimeAnimatorController cat;
	public RuntimeAnimatorController dog;

	/*AUDIO STUFF*/
	AudioSource audioSrc;
	public AudioClip Jump;
	public AudioClip playerHit;

	/*ENEMY STUFF*/
	public bool hitEnemy; //collided with enemy
	public bool respawning;

	public int enemyHitCount; //amount of times player hit by enemy

	GameMang gameMangScript;
	GameObject gameMangObject;

	private void Awake()
	{
		rigid = GetComponent<Rigidbody2D> ();
	}
		// Use this for initialization
	void Start ()
	{

		audioSrc = GetComponent<AudioSource> ();

		anim = GetComponent<Animator> ();

		gameMangObject= GameObject.FindGameObjectWithTag ("GameController");
		gameMangScript = gameMangObject.GetComponent<GameMang> ();

		/*sets animator based on player character select*/
		if (gameMangScript.catChar)
		{
			anim.runtimeAnimatorController = cat;
		}
		else
		{
			anim.runtimeAnimatorController = dog;
		}

	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		float move = Input.GetAxis ("Horizontal2");
		anim.SetFloat("Speed", Mathf.Abs(move));
		rigid.velocity = new Vector2 (move * speed, rigid.velocity.y);

		if (move > 0 && !facingRight) 
		{
			Flip ();
		}
		else if (move < 0 && facingRight) 
		{
			Flip ();
		}
				
	}
	void Update()
	{

		if (Input.GetButtonDown ("Jump2")) 
		{
			if (grounded)
			{
				rigid.velocity = new Vector2(rigid.velocity.x, 0);
				rigid.AddForce (new Vector2 (0, jumpf));
				audioSrc.clip = Jump;
				audioSrc.Play ();
				canJumpAgain = true;
			}
			else if (canJumpAgain) //double jump
			{
				rigid.velocity = new Vector2(rigid.velocity.x, 0);
				rigid.AddForce (new Vector2 (0, jumpf));
				audioSrc.clip = Jump;
				audioSrc.Play ();
				canJumpAgain = false;
			}
		}
			
				
	}	
	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void OnCollisionEnter2D(Collision2D col)
	{

		if (col.gameObject.name.Contains("moveablePlatform")) //attached them to platform so they move along with it
		{
			gameObject.transform.parent = col.transform;
		}
		if (col.gameObject.tag == "enemy")
		{
			audioSrc.clip = playerHit;
			audioSrc.Play ();
			EnemyMove enemyScript=col.gameObject.GetComponent<EnemyMove> ();
			if(!enemyScript.isBeingDestroyed)
			{	
				
				hitEnemy = true;
			}
			else //not affected by enemy that's destroying
			{
				Physics2D.IgnoreCollision (col.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
			}
		}

	}

	void OnCollisionExit2D(Collision2D col)
	{
		if (col.gameObject.name.Contains("moveablePlatform")) 
		{
			gameObject.transform.parent = null;
		}

	}
}
