using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{

	//private float lightPosition;

	//private int lightBool;

	public float battery;

	private Light theLight;

	public bool flashBool;

	public float lightPower;

	private void Awake()
	{
		theLight = GetComponent<Light>();
		battery = 100.0f;
		flashBool = true;
		theLight.intensity = lightPower;

	}


	void Update()
    {
		FlashOnOff();
	}

	private void FlashOnOff()
	{

		if(battery > 0)
		{
			if (Input.GetKeyDown(KeyCode.F))
			{
				if (flashBool == true)
				{
					theLight.intensity = 0;
					flashBool = false;
				}
				else
				{
					theLight.intensity = lightPower;
					flashBool = true;
				}
			}

			if (flashBool == true)
			{
				battery -= 0.5f * Time.deltaTime;
			}
		}
		else
		{
			battery = 0;
			theLight.intensity = 0;
			flashBool = false;
		}
	}
	IEnumerator SetFlash()
	{
		while (true)
		{
			//if() //라이트 재구축!S
			yield return null;
		}
	}
}
