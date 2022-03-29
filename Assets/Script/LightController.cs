using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{

	private float lightPosition;

	private int lightBool;

	[SerializeField]
	private float battery;

	[SerializeField]
	private Light theLight;

	private bool flashBool;

	private void Awake()
	{
		//theLight = GetComponent<Light>();
		battery = 100.0f;
		lightBool = 1;
		lightPosition = 0.0f;
		flashBool = true;
	}


	void Update()
    {
		FlashMove();
		FlashOnOff();
	}

	private void FlashMove()
	{
		
		if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
		{
			
			if (Mathf.Abs(lightPosition) > 0.7f)
			{
				lightBool *= -1;
			}
			lightPosition += 0.07f * lightBool;
		}
		else
		{
			lightPosition *= 0.5f;
			lightBool = 1;
		}
		theLight.transform.localPosition = new Vector3(lightPosition, (Mathf.Abs(lightPosition) * -0.5f) + 0.5f, 0);
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
					theLight.intensity = 1.5f;
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

}
