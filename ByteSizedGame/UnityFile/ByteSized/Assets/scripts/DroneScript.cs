/**
*Moves drones up and down
*/
using UnityEngine;
using System.Collections;

public class DroneScript : MonoBehaviour
{
	protected Vector3 velocity;
	public float distance = 2f;
	public float speed = 1f;
	public float distFromStart;
	Vector3 originalPosition;

	bool isGoingUp = false;
	// Use this for initialization
	void Start () 
	{
		velocity = new Vector3(0,speed,0);
		transform.Translate (0, velocity.y * Time.deltaTime, 0);
		originalPosition = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
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

	void SwitchDirection()
	{
		isGoingUp = !isGoingUp;
	}
}
