/**
*Destorys walls after puzzles are solved
*/
using UnityEngine;
using System.Collections;

public class DestroyWall : MonoBehaviour 
{
	Animator anim; 

	/*pressure plates players stand on*/
	public GameObject pressurePad1;
	public GameObject pressurePad2;
	PressurePlate pressureScript1;
	PressurePlate pressureScript2;

	public bool puzzleSolved;//pressure plates both stood on OR both buttons held down

	public SpriteRenderer wire;//wire from plates to wall
	public SpriteRenderer wireWBttns;//wires attached to plates individually
	public GameObject itemWWireChange;//changes wire colours
	WiresToCheckpoints wireChangeScript;

	AudioSource audiosound;

	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator> ();
		audiosound = GetComponent<AudioSource> ();

		pressureScript1 = pressurePad1.GetComponent<PressurePlate> ();
		pressureScript2 = pressurePad2.GetComponent<PressurePlate> ();
		wireChangeScript = itemWWireChange.GetComponent<WiresToCheckpoints> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(pressureScript1.onPad && pressureScript2.onPad && !gameObject.name.Contains("2Pers") && !gameObject.name.Contains("endWall"))//stood on both plates and not a puzzle that relies on buttons
		{
			puzzleSolved = true;
		}

		if (puzzleSolved) 
		{
			/*turns wires on*/
			foreach (SpriteRenderer child in wire.GetComponentsInChildren<SpriteRenderer>())
			{
				wireChangeScript.ChangeWireSprite (child);
			}
			foreach (SpriteRenderer child in wireWBttns.GetComponentsInChildren<SpriteRenderer>())
			{
				wireChangeScript.ChangeWireSprite (child);
			}
			foreach (Animator child in GetComponentsInChildren<Animator>()) //animates all the walls
			{
				child.SetTrigger ("Destory");
			}
			anim.SetTrigger ("Destory");
			Destroy (gameObject,1);
			audiosound.Play ();
		}

	}
		
}
