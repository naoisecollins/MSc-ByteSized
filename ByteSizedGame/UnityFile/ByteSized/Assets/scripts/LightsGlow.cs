/**
*Controls lighting around game fades it in and out
*/
using UnityEngine;
using System.Collections;

public class LightsGlow : MonoBehaviour
{
	Light lt;

	public float duration = 1.0F;
	float fadeSpeed = 0.8f;
	float highIntensity = 3f;
	float lowIntensity = 0.0f;
	float changeMarg = 0.2f;
	float targetIntensity;

	void Start()
	{
		lt = GetComponent<Light>();
	}

	void Update()
	{

		lt.intensity = Mathf.Lerp (lt.intensity,targetIntensity, fadeSpeed * Time.deltaTime);
		CheckIntensity ();
		lt.color = Color.white;
	}

	/*chanegs target intensity for light*/
	void CheckIntensity()
	{
		if (Mathf.Abs (targetIntensity - lt.intensity) < changeMarg) 
		{
			if (targetIntensity == highIntensity) 
			{
				targetIntensity = lowIntensity;
			}
			else 
			{
				targetIntensity = highIntensity;
			}
		}
	}
}