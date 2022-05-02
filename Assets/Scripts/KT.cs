using UnityEngine;

public class KT : KTEye
{
	protected override void Update()
	{
		base.Update();

		TurnObject();
	}

	protected override void TurnObject()
	{
		Vector3 direction = (target.position - transform.position);
		Quaternion rotation = Quaternion.LookRotation(direction);
		rotation.eulerAngles += new Vector3(-90f, 0, 0);
		transform.rotation = Quaternion.Lerp(transform.rotation, rotation, turnSpeed * Time.deltaTime);
	}
}
