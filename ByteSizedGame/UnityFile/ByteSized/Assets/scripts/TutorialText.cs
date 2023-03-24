/**
*Controls speed and way which letters are typed out onto the speech bubble
*/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialText: MonoBehaviour
{

	public float letterPause;
	public GameObject textBox;//speech bubble

	string message;//text that needs to go in speech bubble

	// Use this for initialization
	void Start ()
	{
		message = GetComponent<Text> ().text;
		GetComponent<Text> ().text="";//empties textbox
		StartCoroutine(TypeText ());
	}

	IEnumerator TypeText () 
	{
		
		for (int i = 0; i < message.Length; i++)
		{
			if (GetComponent<Text> ().text.Length>92)//when box runs out of space;
			{
				yield return new WaitForSeconds (0.5f);
				GetComponent<Text> ().text = "";
			}
				
			char letter = message [i];
			GetComponent<Text> ().text += letter;

			yield return new WaitForSeconds (letterPause);
		}
			
		yield return new WaitForSeconds (1f);
		Destroy (textBox.gameObject);
	}
}