using UnityEngine;

public class KT : KTEye
{
	protected override void Update()
	{
		base.Update();

		MoveKT();
	}

	protected override void TurnObject()
	{
		Vector3 direction = (target.position - transform.position);
		Quaternion rotation = Quaternion.LookRotation(direction);
		transform.rotation = Quaternion.Lerp(transform.rotation, rotation, turnSpeed * Time.deltaTime);
	}

	private void MoveKT()
	{
		//if()
	}
}
