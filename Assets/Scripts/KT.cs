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
	public bool isTargeting;
	public bool isCatch;
	private Rigidbody rigid;
	private CharacterController cha;


	private void Awake()
    {
		rigid = GetComponent<Rigidbody>();
		cha = GetComponent<CharacterController>();
	}

    private void Start()
    {
		maxSpeed = speed;
	}

    private void Update()
    {
		TargetObject(KTHead);
		TargetObject(KTEyeL);
		TargetObject(KTEyeR);

		if (isTargeting)
        {
			MoveKT();
		}
        if (isCatch) {
			rigid.position = transform.position;
			Vector3 direction = (KTHead.transform.position - target.transform.position);
			Quaternion rotation = Quaternion.LookRotation(direction);
			rotation.eulerAngles += new Vector3(-15f, 0, 0);
			target.transform.rotation = Quaternion.Lerp(target.transform.rotation, rotation * Quaternion.Euler(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f)), turnSpeed * Time.deltaTime);
		}

	}

	private void TargetObject(GameObject It)
	{
		Quaternion qua = Quaternion.identity;
        if (isCatch)
        {
            qua = Quaternion.Euler(/*Random.Range(-30f, 30f)*/0, Random.Range(-90f, 90f), /*Random.Range(-30f, 30f)*/0);
        }
        Vector3 direction = (target.position - It.transform.position);
		Quaternion rotation = Quaternion.LookRotation(direction);
		rotation.eulerAngles += new Vector3(-81f, 0, 0);
		It.transform.rotation = Quaternion.Lerp(It.transform.rotation, rotation * qua, turnSpeed * Time.deltaTime);
	}

	private void MoveKT()
	{
		//Trun Body
		Vector3 direction = (new Vector3(target.position.x,0, target.position.z) - new Vector3(transform.position.x, 0, transform.position.z));
		Quaternion rotation = Quaternion.LookRotation(direction);
		transform.rotation = Quaternion.Lerp(transform.rotation, rotation, turnSpeed * 0.7f * Time.deltaTime);
		Vector3 dir = transform.localRotation * Vector3.forward;

		cha.Move(dir * speed * Time.deltaTime);
		//transform.localPosition += dir * speed * Time.deltaTime;
	}

  //  private void OnCollisionEnter(Collision collision)
  //  {
  //      if(collision.gameObject.tag == "Player")
  //      {
		//	isTargeting = false;
		//	isCatch = true;
			
		//}
  //  }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
		if (hit.gameObject.tag == "Player")
		{
			isTargeting = false;
			isCatch = true;

		}
	}
}
