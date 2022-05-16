using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KT : MonoBehaviour
{
	public GameManager gameManager;

	public Transform target;
	public GameObject player;
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
	public Light flash;
	private Vector3 dir;

	private void Awake()
    {
		rigid = GetComponent<Rigidbody>();
		cha = GetComponent<CharacterController>();
		GameObject.Find("GameManager").GetComponent<GameManager>();
	}

    private void Start()
    {
		maxSpeed = speed;
		StartCoroutine(randKTMove());
	}

    private void Update()
    {
		if (gameManager.gameOver == false)
		{
			Targeting();
			TargetObject(KTHead);
			if (!isCatch)
			{
				TargetObject(KTEyeL);
				TargetObject(KTEyeR);
			}
			else
			{
				KTEyeL.transform.rotation = Quaternion.identity;//Euler(Random.Range(-120f,120f), Random.Range(-120f, 120f), Random.Range(-120f, 120f));
				KTEyeR.transform.rotation = Quaternion.identity;

			}

			if (isTargeting)
			{
				MoveKT();
			}
			if (isCatch)
			{
				rigid.position = transform.position;
				Vector3 direction = (KTHead.transform.position - target.transform.position);
				Quaternion rotation = Quaternion.LookRotation(direction);
				rotation.eulerAngles += new Vector3(-15f, 0, 0);
				target.transform.rotation = Quaternion.Lerp(target.transform.rotation, rotation * Quaternion.Euler(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f)), turnSpeed * Time.deltaTime);
			}
		}
	}

	void Targeting()
    {
		if (!isCatch)
		{
			Vector3 direction = (target.transform.position - KTHead.transform.position);
			direction += new Vector3(0, -0.4f, 0);
			//Debug.DrawRay(KTHead.transform.position, direction * 30, Color.blue, 0.3f);
			RaycastHit ray;
			if (Physics.Raycast(KTHead.transform.position, direction, out ray, 30))
			{
				//Debug.Log(ray.collider.tag);
				if (ray.collider.tag == "Player")
				{
					isTargeting = true;
				}
				else
				{
					isTargeting = false;
					cha.Move(dir.normalized * speed * 1.0f * Time.deltaTime);
				}
			}
			else
			{
				isTargeting = false;
				cha.Move(dir.normalized * speed * 1.0f * Time.deltaTime);
			}
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

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
		if (hit.gameObject.tag == "Player" && player.gameObject.GetComponent<PlayerController>().isHide == false)
		{
			isTargeting = false;
			isCatch = true;
		}
	}

	IEnumerator randKTMove()
	{
		while (true)
		{
			yield return new WaitForSeconds(0.5f);

			dir = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
		}
	}
}
