using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KTMove : MonoBehaviour
{

	public Transform taget;

	public float speed = 1.5f;


	void Awake()
    {
		//taget = GetComponent<Transform>();
    }

	void Update()
	{
		Rot();
		MoveKT();
	}

	public void Rot()
    {
		Vector3 direction = (taget.position - transform.position);
		Quaternion rotation = Quaternion.LookRotation(direction);
		transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);
	}
	
	private void MoveKT()
    {
		//if()
    }
}
