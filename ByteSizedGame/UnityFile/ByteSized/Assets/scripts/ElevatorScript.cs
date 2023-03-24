/**
*Elavoters for high puzzles
*/
using UnityEngine;
using System.Collections;

public class ElevatorScript : MonoBehaviour 
{
	public GameObject elevator;//platform

	public GameObject button;
	ButtonBehav buttonScript;

	bool platformMoving;
	bool returnToOrgPos;
	bool movePlat;

	public float amountToMove;

	Vector3 transport;
	// Use this for initialization
	void Start () 
	{
		buttonScript = button.GetComponent<ButtonBehav> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!platformMoving && ( (buttonScript.player1BesideButton && Input.GetButtonDown("Tet1")) || (buttonScript.player2BesideButton && Input.GetButtonDown("Tet2")) ) ) 
		{
			StartCoroutine ("MoveElevator");
		}
		if (movePlat) 
		{
			transport = new Vector2 (transform.position.x, transform.position.y + amountToMove);
			transform.position = Vector3.Lerp (transform.position, transport, Time.deltaTime);
		}
		if (returnToOrgPos)
		{
			transport = new Vector2 (transform.position.x, transform.position.y - amountToMove);
			transform.position = Vector3.Lerp (transform.position, transport, Time.deltaTime);
		}
		if (platformMoving) 
		{
			transform.GetComponent<SpriteRenderer> ().color = new Color (0, 1, 0, 1);
			elevator.transform.GetComponent<SpriteRenderer> ().color = new Color (0, 1, 0, 1);
		}
		else if (!platformMoving) 
		{
			transform.GetComponent<SpriteRenderer> ().color = new Color (1, 0.92f, 0.016f, 1);
			elevator.transform.GetComponent<SpriteRenderer> ().color = new Color (1, 0.92f, 0.016f, 1);

		}
	}

	IEnumerator MoveElevator()
	{
		platformMoving = true;
		movePlat = true;
		yield return new WaitForSeconds (2f);
		movePlat = false;

		yield return new WaitForSeconds (4f);
		returnToOrgPos = true;
		yield return new WaitForSeconds (2f);
		returnToOrgPos = false;

		platformMoving = false;
	}
}
