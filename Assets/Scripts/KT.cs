using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KT : MonoBehaviour
{
	public Transform target;
	public float turnSpeed;
	public GameObject KTHead;
	public GameObject KTEyeL;
	public GameObject KTEyeR;
	public float speed;
	private float maxSpeed;


	private void Awake()
    {
		maxSpeed = speed;
	}

	private void Update()
    {
		MoveKT();

        if (true)
        {
			TurnObject(KTHead);
			TurnObject(KTEyeL);
			TurnObject(KTEyeR);
        }

	}

	private void TurnObject(GameObject It)
	{
		Vector3 direction = (target.position - It.transform.position);
		Quaternion rotation = Quaternion.LookRotation(direction);
		rotation.eulerAngles += new Vector3(-83f, 0, 0);
		It.transform.rotation = Quaternion.Lerp(It.transform.rotation, rotation, turnSpeed * Time.deltaTime);
	}

	private void MoveKT()
	{
		//Trun Body
		Vector3 direction = (new Vector3(target.position.x,0, target.position.z) - new Vector3(transform.position.x, 0, transform.position.z));
		Quaternion rotation = Quaternion.LookRotation(direction);
		transform.rotation = Quaternion.Lerp(transform.rotation, rotation, turnSpeed * 0.7f * Time.deltaTime);
		Vector3 dir = transform.localRotation * Vector3.forward;

		transform.localPosition += dir * speed * Time.deltaTime;
	}
}
