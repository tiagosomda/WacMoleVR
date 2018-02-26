using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleHole : MonoBehaviour {

	public Vector3 hiddenPos;
	public Vector3 showPos;
	public Mole mole;

	public float duration;
	public bool isShowing;
	private Vector3 targetPos;
	private float hideDelay;

	MoleSpawner spawner;

	// Use this for initialization
	void Start () {
		spawner =  GetComponentInParent<MoleSpawner>();

		mole.transform.localPosition = hiddenPos;
		if(duration <= 0)
		{
			duration = 1f;
		}
	}

	public void Show(float speed, float hideDelayInSec)
	{
		hideDelay = hideDelayInSec;
		StopCoroutine(GoToTarget());
		targetPos = showPos;
		duration = speed;
		isShowing = true;
		StartCoroutine(GoToTarget());
	}
	public void Hide()
	{
		StopCoroutine(GoToTarget());
		targetPos = hiddenPos;
		duration = 0.1f;
		isShowing = false;
		StartCoroutine(GoToTarget());
	}

	IEnumerator GoToTarget()
	{
		var start = mole.transform.localPosition;
		float t = 0;
		while(t < 1)
		{
			t += Time.deltaTime/duration;
			mole.transform.localPosition = Vector3.Lerp(start, targetPos, t);
			yield return new WaitForEndOfFrame();
		}


		if(isShowing)
		{
			yield return new WaitForSeconds(hideDelay);
			if(isShowing)
			{
				spawner.ReturnMole(mole.index);
			}
		}
	}
}
