using UnityEngine;

public class KTEye : MonoBehaviour
{
	public Transform target;

	public float turnSpeed;

	protected virtual void Update()
	{
		TurnObject();
	}

	protected virtual void TurnObject()
	{
		Vector3 direction = (target.position - transform.position);
		Quaternion rotation = Quaternion.LookRotation(direction);
		rotation.eulerAngles += new Vector3(-87f, 0, 0);
		transform.rotation = Quaternion.Lerp(transform.rotation, rotation, turnSpeed * Time.deltaTime);
	}
}
