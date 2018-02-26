using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleSpawner : MonoBehaviour {

	[Header("Mole Frequency Range")]
	public float minFrequency;
	public float maxFrequency;

	[Header("Mole Show Speed")]
	public float minShowSpeed;
	public float maxShowSpeed;

	[Header("Mole Hide Delay Range")]
	public float minHideDelay;
	public float maxHideDelay;

	[Header("Others")]
	public Transform holesParent;
	public TextMesh scoreTxt;

	MoleHole[] holes;
	List<int> available;
	private int score;
	

	// Use this for initialization
	void Start () 
	{		
		if(minFrequency > maxFrequency)
		{
			Debug.LogError("min frequency can not be less than max frequency. Setting default values.");
			
			minFrequency = 1f;
			maxFrequency = 3f;
		}

		if(minHideDelay > maxHideDelay)
		{
			Debug.LogError("minHideDelay can not be less than maxHideDelay. Setting default values.");
			minHideDelay = 0.3f;
			maxHideDelay = 0.8f;
		}

		if(minShowSpeed > maxShowSpeed)
		{
			Debug.LogError("minShowSpeed can not be less than maxShowSpeed. Setting default values.");
			minShowSpeed = 0.3f;
			maxShowSpeed = 1f;
		}

		holes = holesParent.GetComponentsInChildren<MoleHole>();
		available = new List<int>();

		for(int i = 0; i < holes.Length; i++)
		{
			holes[i].mole.index = i;
			available.Add(i);
		}

		StartCoroutine(AutoSpawn());
	}

	IEnumerator AutoSpawn()
	{
		SpawnMole();
		var time = Random.Range(minFrequency, maxFrequency);
		yield return new WaitForSeconds(time);
		StartCoroutine(AutoSpawn());
	}

	void SpawnMole()
	{
		if(available.Count == 0)
		{
			Debug.Log("No available hole");
			return;
		}

		int i = Random.Range(0,available.Count);
		int index = available[i];
		available.Remove(index);
		holes[index].Show(Random.Range(minShowSpeed, maxShowSpeed), Random.Range(minHideDelay, maxHideDelay));
	}

	public void ReturnMole(int index)
	{
		holes[index].Hide();
		available.Add(index);
	}

	public void AddScore(int points)
	{
		score += 10;
		scoreTxt.text = "Score : " + score;
	}
}
