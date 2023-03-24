/**
*Pick up and throw script for players
*/
using UnityEngine;
using System.Collections;

public class GrabberScript : MonoBehaviour 
{
	/*GRAB STUFF*/
	public bool grabbed;
	RaycastHit2D hit;
	public float distance =2f;
	public Transform holdPoint;
	public float throwForce;

	/*AUDIO STUFF*/
	public AudioSource audiosound;
	public AudioClip PickUp;
	public AudioClip Throw;

	GameObject blockPickedUp;//datablock being held
	CodeBlock blockScript;//script attached to datablock

	CharacterMovement1 charScript1;
	CharacterMovement2 charScript2;

	void Start () 
	{
		AudioSource audio = GetComponent<AudioSource> ();

		if (gameObject.name == "Player1") 
		{
			charScript1 = GetComponent<CharacterMovement1> ();
		}
		else if (gameObject.name == "Player2")
		{
			charScript2 = GetComponent<CharacterMovement2> ();
		}
	}
	// Update is called once per frame
	void Update ()
	{
		if ( (gameObject.name=="Player1" && Input.GetButtonDown ("Primary1") && !grabbed) ||  (gameObject.name=="Player2" && Input.GetButtonDown ("Primary2")  && !grabbed) )
		{

			Physics2D.queriesStartInColliders = false;
			hit = Physics2D.Raycast (transform.position, Vector2.right * transform.localScale.x, distance);
			if (hit.collider != null && hit.collider.tag == "grabbable") //now holding something set grabbed to true
			{
				grabbed = true;
				audiosound.clip = PickUp;
				audiosound.Play ();

				blockPickedUp = hit.collider.gameObject;
				blockScript = blockPickedUp.GetComponent<CodeBlock> ();
				blockScript.beingHeld = true;

			}

		}
		else if ( (gameObject.name=="Player1" && Input.GetButtonDown ("Primary1") && grabbed) ||  (gameObject.name=="Player2" && Input.GetButtonDown ("Primary2")  && grabbed) ) 
		{
			hit.collider.gameObject.GetComponent<Rigidbody2D> ().mass = 6;//resets mass
			hit.collider.gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (transform.localScale.x, 1) * throwForce;//throw block

			grabbed = false;
			audiosound.clip = Throw;
			audiosound.Play ();

			blockScript.beingHeld = false;
		}
		if (grabbed)
		{
			
			hit.collider.gameObject.transform.position = holdPoint.position;
			hit.collider.gameObject.GetComponent<Rigidbody2D> ().mass = 0; //sets mass to 0 so doesn't weigh down player

			if (gameObject.name == "Player1" && charScript1.hitEnemy) //if they get hit by an enemy they drop hold on block
			{
				grabbed = false;
			}
			else if (gameObject.name == "Player2" && charScript2.hitEnemy)
			{
				grabbed=false;
			}
		}


	}//end of update

	void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawLine (transform.position, transform.position + Vector3.right * transform.localScale.x * distance);
	}
}