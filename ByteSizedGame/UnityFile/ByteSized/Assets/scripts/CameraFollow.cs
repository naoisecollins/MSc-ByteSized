/*
*Controls cam movement
*/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraFollow : MonoBehaviour 
{
	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;
	public float midX;
	public float midY;
	public Transform target1;
	public Transform target2;
	public Vector3 midpoint;
	public float distBetweenPlayers;

	public float zoomTime; //time it takes to zoom out
	bool camZoomingOut; //cam is zooming
	bool camMoving; //being controlled by player;
	bool camShouldMove; //controlled by players distance
	float playersPreviousDist;//distance of players in last frame
	float camSize;

	Camera cam;

	// Use this for initialization
	void Start () 
	{
		cam = GetComponent<Camera> ();
	}

	// Update is called once per frame
	void Update ()
	{


		distBetweenPlayers = Vector3.Distance (target1.position, target2.position);

		//midpoint between players
		midX = (target2.position.x + target1.position.x) / 2; 
		midY = (target2.position.y + target1.position.y) / 2;

		midpoint = new Vector3 (midX, midY+2f, -1f);

		transform.position = Vector3.SmoothDamp (transform.position, midpoint, ref velocity, dampTime);

		/*cam never goes below size 4f or 7f*/
		if (cam.orthographicSize > 7f) 
		{
			cam.orthographicSize = 7f;
		}
		else if (cam.orthographicSize < 4f) 
		{
			cam.orthographicSize = 4f;
		}

		/*Camera expands as players moves away from each other and minimizes as players get closer by .1 of a decimal*/
		if (System.Math.Round(playersPreviousDist, 1) != System.Math.Round(distBetweenPlayers, 1)) 
		{
			if(playersPreviousDist<distBetweenPlayers) //players have moved further apart
			{
				cam.orthographicSize = cam.orthographicSize +zoomTime * Time.deltaTime;
			}
			else if(playersPreviousDist>distBetweenPlayers) //players have moved closer together
			{
				cam.orthographicSize = cam.orthographicSize -zoomTime * Time.deltaTime;
			}
		}


		playersPreviousDist = distBetweenPlayers;

	}


}