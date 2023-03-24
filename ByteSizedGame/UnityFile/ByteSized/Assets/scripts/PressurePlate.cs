/**
*Handles collisions ith the pressure plates 
*/
using UnityEngine;
using System.Collections;

public class PressurePlate : MonoBehaviour 
{
	public GameObject pressurepad; //case arount buttn
	public bool onPad; // player standing on pad
	Vector3 defaultBttnPos;

	public GameObject otherPlate; //ither pressure plate
	PressurePlate otherPlatesScript;//pressure plate script attached to other player

	bool bothPlayersOnPad;

	public SpriteRenderer wire;
	public GameObject itemWWireChange;
	WiresToCheckpoints wireChangeScript;
	Sprite offWire;

	AudioSource audioSrc;


	// Use this for initialization
	void Start () 
	{
		Physics2D.IgnoreCollision (pressurepad.GetComponent<Collider2D> (), GetComponent<Collider2D> ());

		otherPlatesScript = otherPlate.GetComponent<PressurePlate> ();

		wireChangeScript = itemWWireChange.GetComponent<WiresToCheckpoints> ();
		offWire = wire.sprite;

		defaultBttnPos = transform.position;

		audioSrc = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (onPad) 
		{
			wireChangeScript.ChangeWireSprite (wire);
		}
		else if (!onPad && !bothPlayersOnPad)//so it doesnt revert back to switched off once the puzzles finished 
		{
			wire.sprite = offWire;
		}

		if (otherPlatesScript.onPad && onPad) 
		{
			bothPlayersOnPad = true;//will stay true onc both have stood on plate
		}
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Player") 
		{
			transform.position = new Vector2(transform.position.x, transform.position.y - 0.03f);
			onPad = true;
			audioSrc.Play ();

		}
	}
	void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Player") 
		{
			transform.position = defaultBttnPos;
			onPad = false;
		}
	}
}
