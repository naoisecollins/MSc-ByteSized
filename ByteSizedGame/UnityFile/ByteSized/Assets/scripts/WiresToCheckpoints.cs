/**
*Changes wires from off to on
*/
using UnityEngine;
using System.Collections;

public class WiresToCheckpoints : MonoBehaviour 
{
	bool hitDest;
	public Sprite straightwire;
	public Sprite curvedWire1;
	public Sprite curvedWire2;
	public Sprite curvedWire3;

	public Sprite straightwireOff;
	public Sprite curvedWireOff1;
	public Sprite curvedWireOff2;
	public Sprite curvedWireOff3;

	AudioSource audiosound;

	// Use this for initialization
	void Start () 
	{
		audiosound = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (hitDest)
		{
			foreach (SpriteRenderer child in GetComponentsInChildren<SpriteRenderer>())
			{
				ChangeWireSprite (child);

			}
		}
	}

	public void ChangeWireSprite(SpriteRenderer offWire)
	{
		if (offWire.sprite == straightwireOff)
		{
			offWire.sprite = straightwire;
		}
		else if (offWire.sprite == curvedWireOff1)
		{
			offWire.sprite = curvedWire1;
		}	
		else if (offWire.sprite == curvedWireOff2)
		{
			offWire.sprite = curvedWire2;
		}	
		else if (offWire.sprite == curvedWireOff3)
		{
			offWire.sprite = curvedWire3;
		}	
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.name.Contains ("moveablePlatform"))
		{
			hitDest = true;
			audiosound.Play ();
		}
	}
}
