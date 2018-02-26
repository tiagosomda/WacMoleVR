using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour {

	public int index;
	MoleSpawner spawner;

	// Use this for initialization
	void Start () {
		//transform.position.Set(transform.position.x, hiddenPos, transform.position.z);
		spawner =  GetComponentInParent<MoleSpawner>();
	}

	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "hammer")
		{
			spawner.ReturnMole(index);
			spawner.AddScore(10);
		}
	}
}
