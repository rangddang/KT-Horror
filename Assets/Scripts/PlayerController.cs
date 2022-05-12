using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public GameManager gameManager;

	public float maxSpeed;
	private float saveSpeed;
	private float walkSpeed;
	public float gravity;
	public float setLook;
	private float lookSensitivity;
	public float cameraRotationLimit;
	private float currentCameraRotationX;
	private float lightPosition;
	private int lightBool;
	private bool isMap;
	public float v;

	public Camera theCamera;
	public Light theLight;
	public CharacterController _charter;
	private Rigidbody myRigid;
	public Stamina stamina;
	public GameObject phone;

	private Vector3 _charaterRotationY;



	private void Awake()
	{
		theCamera = GameObject.Find("PlayerCamera").GetComponent<Camera>();
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		myRigid = GetComponent<Rigidbody>();
	}

	private void Start()
	{
		phone.SetActive(false);
		lightBool = 1;
		lightPosition = 0.0f;
		lookSensitivity = setLook;
		currentCameraRotationX = 0.0f;
		_charaterRotationY = Vector3.zero;
		saveSpeed = maxSpeed;
	}

	private void Update()
	{
		Gravity();
		Move();
		CharacteraRotation();
		CameraRotation();
		PhoneMove();
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			MiniMap();
		}

	}

	private float Gravity()
	{
		float _moveDirY = 0;
		if (_charter.isGrounded == false)
		{
			_moveDirY -= gravity;
		}
		return _moveDirY;
	}

	private void Move()
	{
		//if (stamina.run) //스테미나 바와 라이트 컨트롤러 코드 재구축하기!

		float _moveDirX = Input.GetAxisRaw("Horizontal");
		float _moveDirZ = Input.GetAxisRaw("Vertical");

		if (stamina.run)
		{
			walkSpeed = maxSpeed * 2f;
		}
		else
		{
			walkSpeed = maxSpeed;
		}

		//버그때문에 끔!(CCTV를 켰을때 뒤로가면 이동속도가 -0.5가 되서 0.5만큼씩 앞으로 감)
		//if (_moveDirZ < 0)
		//{
		//	walkSpeed -= 0.5f;
		//}

		Vector3 _moveJump = new Vector3(0, 0, 0);
		_moveJump += transform.up * (Gravity());

		Vector3 _moveHorizontal = transform.right * _moveDirX;
		Vector3 _moveVertical = transform.forward * _moveDirZ;

		Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * walkSpeed;

		_charter.Move((_velocity + _moveJump) * Time.deltaTime * 1.0f);

	}

	private void CharacteraRotation()
	{
		float _yRotation = Input.GetAxisRaw("Mouse X");
		_charaterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;
		myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_charaterRotationY));
	}

	private void CameraRotation()
	{
		float _xRotation = Input.GetAxisRaw("Mouse Y");
		float _cameraRotationX = _xRotation * lookSensitivity;

		currentCameraRotationX -= _cameraRotationX;
		currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

		if(!isMap)
			theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
		if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
		{

			if (Mathf.Abs(lightPosition) > 10.0f)
			{
				lightPosition = 10f * lightBool;
				lightBool *= -1;
			}
			lightPosition += walkSpeed * 8.0f * lightBool * Time.deltaTime;
		}
		else
		{
			lightPosition *= 0.5f;
			lightBool = 1;
		}
		theLight.transform.localEulerAngles = new Vector3((Mathf.Abs(lightPosition) * 0.5f) + 0.5f + currentCameraRotationX, lightPosition, 0);
	}

	void MiniMap()
	{
		if (!isMap)
		{
			phone.SetActive(true);
			StartCoroutine(PhonMove());
			currentCameraRotationX = 0.0f;
			lookSensitivity = 0f;
			maxSpeed = 0f;
			isMap = true;
		}
		else
		{
			phone.SetActive(false);
			lookSensitivity = setLook;
			maxSpeed = saveSpeed;
			isMap = false;
		}
	}

	void PhoneMove()
    {
		if (isMap)
		{
			theCamera.transform.localRotation = Quaternion.Lerp(theCamera.transform.localRotation, Quaternion.Euler(0f,0f,0f), 7f * Time.deltaTime);
			phone.transform.localPosition = new Vector3(0, v, 0.4f);
		}
		else phone.transform.localPosition = new Vector3(0, 0, 0);

	}

	IEnumerator PhonMove()
    {
		for(v = 0.1f;v<0.8f;v+=0.05f)
			yield return new WaitForSeconds(0.02f);
    }
	
}
