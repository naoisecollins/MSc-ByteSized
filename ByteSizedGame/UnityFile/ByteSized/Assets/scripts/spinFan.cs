/**
*Rotates fans
*/
using UnityEngine;
using System.Collections;

public class spinFan : MonoBehaviour 
{

	public float Spin = 10f;
	// Use this for initialization
	void Start ()
	{}
	
	// Update is called once per frame
	void Update ()
	{
		{
			transform.Rotate (Vector3.forward* Spin);
		}

	}
}
	

