using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

	public LevelManager levelManager;

	public GameObject numberPrefab;
	public GameObject rockPrefab;
	public GameObject powerupPrefab;

	public float rockFrequency;
	public float powerupFrequency;
	public float numberFrequency;

	// Use this for initialization
	void Start () {
		StartCoroutine("GenerateNumbers");
		StartCoroutine("GenerateRocks");
		StartCoroutine("GeneratePowerups");

	}

	IEnumerator GenerateNumbers() {
		while (true) {
			Instantiate(numberPrefab);
			yield return new WaitForSeconds(UnityEngine.Random.Range(0f, numberFrequency));
		}
	}

	IEnumerator GenerateRocks() {
		yield return new WaitForSeconds(5);
		while (true) {
			Instantiate(rockPrefab);
			yield return new WaitForSeconds(UnityEngine.Random.Range(0f, rockFrequency));
		}
	}

	IEnumerator GeneratePowerups() {
		yield return new WaitForSeconds(10);
		while (true) {
			Instantiate(powerupPrefab);
			yield return new WaitForSeconds(UnityEngine.Random.Range(5f, powerupFrequency));
		}
	}


}
