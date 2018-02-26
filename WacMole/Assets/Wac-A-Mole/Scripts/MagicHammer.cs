using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NewtonVR;

public class MagicHammer : NVRInteractableItem {

	public float flyBackSpeed;
	public float rotateStep;
	NVRHand myHand;
	public override void BeginInteraction(NVRHand hand)
    {
		myHand = hand;
		base.BeginInteraction(hand);
	}

	protected override void  Update()
	{
		base.Update();

		if(myHand != null)
		{
			if(myHand.UseButtonDown && !IsAttached)
			{
				RetrieveHammer();
			}
		}
	}

	public void RetrieveHammer()
	{
		StopCoroutine(RetrieveRoutine());
		StartCoroutine(RetrieveRoutine());
	}

	IEnumerator RetrieveRoutine()
	{
		var rb = GetComponent<Rigidbody>();

		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;

		rb.useGravity = false;
		//rb.isKinematic = true;
		Vector3 direction = myHand.transform.position - transform.position;

		while(Vector3.Distance(myHand.transform.position, transform.position) > 0.9)
		{
			transform.position = Vector3.MoveTowards(transform.position, myHand.transform.position, Time.deltaTime*flyBackSpeed);
			direction = myHand.transform.position - transform.position;
			Vector3 newDir = Vector3.RotateTowards(transform.forward, direction, rotateStep, 0.0F);
			transform.rotation = Quaternion.LookRotation(newDir);
			yield return new WaitForEndOfFrame();
		}

		//rb.isKinematic = false;
		rb.useGravity = true;
		myHand.BeginInteraction(this);
	}
}
