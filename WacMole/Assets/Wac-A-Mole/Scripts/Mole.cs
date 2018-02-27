using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour {

	public int index;
	public bool canHit;
	MoleSpawner spawner;
	AudioSource audioSource;

	// Use this for initialization
	void Start () {
		//transform.position.Set(transform.position.x, hiddenPos, transform.position.z);
		spawner =  GetComponentInParent<MoleSpawner>();
		audioSource = GetComponent<AudioSource>();
		canHit = false;
	}

	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "hammer")
		{
			if(!canHit)
				return;
			
			canHit = false;

			audioSource.Play();
			spawner.ReturnMole(index);
			spawner.AddScore(10);
		}
	}
}
