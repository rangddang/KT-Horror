using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyesMove : MonoBehaviour
{
	private LightController _light;

	public Transform taget;

	public float speed = 5f;

	private void Awake()
	{
		_light = GetComponent<LightController>();
	}

	void Update()
	{
		RotEyes();
	}

	public void RotEyes()
    {
		Vector3 direction = (taget.position - transform.position);
		Quaternion rotation = Quaternion.LookRotation(direction);
		Quaternion Rt = Quaternion.Euler(-87f,0,0);
		transform.rotation = Quaternion.Lerp(transform.rotation, rotation * Rt, speed * Time.deltaTime);
	}

	void EyeScale()
	{
		transform.localScale = new Vector3(0.7f,1,0.7f);

		transform.localScale = new Vector3(1f, 1, 1f);
	}
}
