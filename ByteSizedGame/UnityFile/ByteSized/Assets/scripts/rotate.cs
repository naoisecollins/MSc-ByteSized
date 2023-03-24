/**
*Rotates object and on collision fires it into the air
*/
using UnityEngine;
using System.Collections;

public class rotate : MonoBehaviour 
{
	public float spin = 5f;
	public float amountToMove = 0f;

	public AudioSource mainSfx;
	public AudioClip coinSound;
	public AudioClip animalSound;

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		MoveUp();
	}
	IEnumerator OnTriggerEnter2D(Collider2D coll) 
	{
		if(coll.gameObject.tag=="Player")
		{
			if (gameObject.name.Contains ("coin")) 
			{
				mainSfx.clip = coinSound;
			}
			else 
			{
				mainSfx.clip = animalSound;
			}
			mainSfx.Play ();
			spin = 20f;
			amountToMove = 5f;
			yield return new WaitForSeconds (1f);
			Destroy (gameObject);
		}
	}
	void MoveUp() 
	{
		transform.Rotate(Vector3.down * -spin);
		Vector2 transport = new Vector2(transform.position.x, transform.position.y + amountToMove);
		transform.position = Vector3.Lerp(transform.position, transport, Time.deltaTime);
	}
}

