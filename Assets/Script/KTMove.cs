using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KTMove : MonoBehaviour
{


	[SerializeField]
	private Transform taget;

	[SerializeField]
	public float speed = 1.5f;

	void Update()
	{
		//if (1)
		//{
			Vector3 direction = (taget.position - transform.position);
			Quaternion rotation = Quaternion.LookRotation(direction);
			transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);
			
		//}
	}
}
