/**
*Assigns string to block which holds its list of commands
*/
using UnityEngine;
using System.Collections;

public class FunctionCodeBlock : MonoBehaviour 
{
	public string[] function; //array of strings given by the function creator
	public string method; //all the strings in function combined together
	public bool blockExist;//so only one block can exist at once
	// Use this for initialization
	void Start () 
	{
		blockExist = true;
		ConvertArrayToString ();
		Debug.Log ("this is the string: " + method);
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	/*turns array of chars into one string*/
	void ConvertArrayToString()
	{
		for (int i = 0; i < function.Length; i++)
		{
			method = method + function [i];
		}
	}
}
