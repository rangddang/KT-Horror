using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField]
	private float maxSpeed;

	private float walkSpeed;

	[SerializeField]
	private float gravity;

	[SerializeField]
	private float lookSensitivity;

	[SerializeField]
	private float cameraRotationLimit;
	private float currentCameraRotationX = 0;

	[SerializeField]
	private Camera theCamera;

	[SerializeField]
	private Light theLight;

	[SerializeField]
	private CharacterController _charter;

	private Rigidbody myRigid;

	private float lightPosition;

	private int lightBool;



	private void Awake()
	{
		//theCamera = FindObjectOfType<Camera>();
		myRigid = GetComponent<Rigidbody>();
        lightBool = 1;
        lightPosition = 0.0f;
    }

	private void Update()
	{
		Gravity();
		Move();
		CharacteraRotation();
		CameraRotation();
		
		
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
        if (Input.GetKey(KeyCode.LeftShift))
        {
			walkSpeed = maxSpeed * 1.6f;
        }
        else
        {
			walkSpeed = maxSpeed;
        }

		float _moveDirX = Input.GetAxisRaw("Horizontal");
		float _moveDirZ = Input.GetAxisRaw("Vertical");

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
		Vector3 _charaterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;
		myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_charaterRotationY));
	}

	private void CameraRotation()
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
			lightPosition += walkSpeed * 9.5f * lightBool * Time.deltaTime;
		}
		else
		{
			lightPosition *= 0.5f;
			lightBool = 1;
		}
		theLight.transform.localEulerAngles = new Vector3((Mathf.Abs(lightPosition) * 0.5f) + 0.5f + currentCameraRotationX, lightPosition, 0);
	}

	

	
}
