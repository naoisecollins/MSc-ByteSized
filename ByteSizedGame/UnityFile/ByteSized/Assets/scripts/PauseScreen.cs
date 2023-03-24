/**
*Activates and deactivates pause screens canvas and controls slider in pause screen
*/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PauseScreen : MonoBehaviour 
{
	bool isPaused;
	public GameObject pauseMenu;//contains pause screen options


	/*AUDIO STUFF*/
	public AudioMixer masterMix; //master mixer
	const float minVol=-80f;
	const float maxVol=0f;
	public GameObject volSldr;//will higher or lower vol
	float val;

	// Use this for initialization
	void Start () 
	{
		val = volSldr.GetComponent<Slider> ().value;
		volSldr.GetComponent<Slider> ().minValue=minVol;
		volSldr.GetComponent<Slider> ().maxValue = maxVol;
		masterMix.SetFloat ("Master", val);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButtonDown ("Pause1")||Input.GetButtonDown ("Pause2")) 
		{
			isPaused = !isPaused;
			pauseMenu.SetActive (isPaused);

		}

		if (isPaused) 
		{
			Time.timeScale = 0; //freezes game no movement happens
			Cursor.visible = true;
		}
		else if (isPaused == false) 
		{
			Time.timeScale = 1;
			Cursor.visible = false;
		}
	
	}

	public void changeVol(float val)//attached to sound slider will control master audiomixer
	{
		val = volSldr.GetComponent<Slider> ().value;
		masterMix.SetFloat ("Master", val);
	}


}
