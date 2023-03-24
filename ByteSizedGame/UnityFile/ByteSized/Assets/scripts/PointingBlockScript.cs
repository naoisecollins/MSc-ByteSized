/**
*Destorys pointing blocs once players complete tasks
*/
using UnityEngine;
using System.Collections;

public class PointingBlockScript : MonoBehaviour
{
	public GameObject platDrone;
	public GameObject block1;
	int count=0;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.name.Contains ("Arrow")) 
		{
			if (gameObject.name == "pointingBlock1") 
			{
				platDrone.SetActive (true);//platform drones doesn't appear until input in the pipe
				Destroy (gameObject);
			}
			if (gameObject.name == "pointingBlock2") 
			{
				if (count == 0) 
				{
					count++;
					Destroy (block1);//destroys first block once players fill first input but leaves second one intact 
				}
				else if (count > 0) 
				{
					count++;
					Destroy (gameObject);
				}
			}


		}
	}
}
