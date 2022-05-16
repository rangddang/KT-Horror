using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public GameManager gameManager;

	public float maxSpeed;
	private float saveSpeed;
	public float walkSpeed;
	public float gravity;
	public float setLook;
	private float lookSensitivity;
	public float cameraRotationLimit;
	private float currentCameraRotationX;
	private float lightPosition;
	private int lightBool;
	public bool isMap;
	public float v;
	private int cCam;
	public bool isHide;
	private bool ifHide;

	public GameObject KT;
	public Camera theCamera;
	public Light theLight;
	public CharacterController _charter;
	private Rigidbody myRigid;
	public Stamina stamina;
	public GameObject phone;
	public Camera[] cameras;
	public GameObject hideObject;
	public Light supportLight;
	private float gameOverTime;


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
		cCam = 0;
		gameOverTime = 0;
	}

	private void Update()
	{
		if (KT.GetComponent<KT>().isCatch == false)
		{
			Gravity();
			Move();
			CharacteraRotation();
			CameraRotation();
			PhoneMove();
			HideOn();
			Ground();
			if (Input.GetKeyDown(KeyCode.Tab))
			{
				MiniMap();
			}
			if (isMap && Input.GetKeyDown(KeyCode.A))
			{
				ChangeCamera();
			}
		}
        else 
		{
			stamina.SetStaminaClarity(0);
			phone.SetActive(false);
			lookSensitivity = setLook;
			isMap = false;
			theCamera.gameObject.SetActive(true);
			cameras[cCam].gameObject.SetActive(false);
			isHide = false;
			maxSpeed =0;
			transform.position = new Vector3(transform.position.x, 1.8f, transform.position.z);
			theLight.GetComponent<LightController>().battery = 0;
			supportLight.range = 3;
			supportLight.intensity = Random.Range(0f,3f);
			GameOver();
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
		if (!isMap && !isHide)
		{
			float _xRotation = Input.GetAxisRaw("Mouse Y");
			float _cameraRotationX = _xRotation * lookSensitivity;

			currentCameraRotationX -= _cameraRotationX;
			currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

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
	}

	void MiniMap()
	{
		if (!isMap)
		{
			stamina.SetStaminaClarity(0);
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
			theCamera.gameObject.SetActive(true);
			cameras[cCam].gameObject.SetActive(false);
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

	void ChangeCamera()
    {
		
		cameras[cCam].gameObject.SetActive(false);
		cCam++;
		if (cCam >= cameras.Length) cCam = 0;
		cameras[cCam].gameObject.SetActive(true);
		
	}

	void HideOn()
    {
		if(isHide == true)
        {
			theCamera.transform.localRotation = Quaternion.Lerp(theCamera.transform.localRotation, Quaternion.Euler(0f, 0f, 0f), 7f * Time.deltaTime);
			transform.position = hideObject.transform.position + new Vector3(0,-0.4f,0);
        }
		if(ifHide == true)
        {
			if (Input.GetKeyDown(KeyCode.E) && !isMap)
			{
				if (isHide == false)
				{
					isHide = true;
					maxSpeed = 0f;
					gravity = 0;
					theLight.transform.localEulerAngles = new Vector3(0, 0, 0);
					currentCameraRotationX = 0.0f;
				}
				else
				{
					isHide = false;
					maxSpeed = saveSpeed;
					gravity = 9.8f;
				}

			}
		}
    }

	void Ground()
    {
		if(transform.position.y < -0.5f)
        {
			transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
        }
    }

	void GameOver()
    {
		gameOverTime += Time.deltaTime;
		if (gameOverTime >= 2.5f)
		{
			gameManager.gameOver = true;
			KT.GetComponent<KT>().isCatch = false;
			theCamera.gameObject.SetActive(false);
		}
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Hide")
        {
			ifHide = true;
			hideObject = other.gameObject;
		}
    }
    private void OnTriggerExit(Collider other)
    {
		if (other.gameObject.tag == "Hide")
		{
			ifHide = false;
			hideObject = null;
		}
	}

    IEnumerator PhonMove()
    {
		for(v = 0.1f;v<0.8f;v+=0.05f)
			yield return new WaitForSeconds(0.02f);
		yield return new WaitForSeconds(0.2f);
		theCamera.gameObject.SetActive(false);
		cameras[cCam].gameObject.SetActive(true);
	}
	
}
