using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField]
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

	


	private void Awake()
	{
		//theCamera = FindObjectOfType<Camera>();
		myRigid = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		Gravity();
		Move();
		CameraRotation();
		CharacteraRotation();
		
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
		theLight.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
	}

	

	
}
