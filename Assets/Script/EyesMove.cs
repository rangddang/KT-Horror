using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyesMove : MonoBehaviour
{
	private LightController _light;

	[SerializeField]
	private Transform taget;

	[SerializeField]
	public float speed = 4f;

	private void Awake()
	{
		_light = GetComponent<LightController>();
	}

	void Update()
	{
		Vector3 direction = (taget.position - transform.position) + new Vector3(0, 23f, 0);
		Quaternion rotation = Quaternion.LookRotation(direction);
		transform.rotation = Quaternion.Lerp(transform.rotation,rotation,speed*Time.deltaTime);
		Debug.Log(transform.rotation);
	}

	void EyeScale()
	{
		//if(_light. == 0)
		transform.localScale = new Vector3(0.7f,1,0.7f);

		transform.localScale = new Vector3(1f, 1, 1f);
	}
}
