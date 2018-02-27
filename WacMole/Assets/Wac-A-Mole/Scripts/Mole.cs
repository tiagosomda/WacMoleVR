using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour {

	public Color normalColor;
	public Color hitColor;
	public int points = 10;
	public int index;
	public bool canHit;
	MoleSpawner spawner;
	AudioSource audioSource;

	private MeshRenderer meshRenderer;

	// Use this for initialization
	void Start () {
		//transform.position.Set(transform.position.x, hiddenPos, transform.position.z);
		spawner =  GetComponentInParent<MoleSpawner>();
		audioSource = GetComponent<AudioSource>();
		meshRenderer = GetComponent<MeshRenderer>();
		canHit = false;
	}

	public void SetColor(Color color)
	{
		meshRenderer.material.color = color;
	}

	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "hammer")
		{
			if(!canHit)
				return;
			
			SetColor(hitColor);
			canHit = false;

			audioSource.Play();
			spawner.ReturnMole(index);
			spawner.AddScore(points);
		}
	}
}
