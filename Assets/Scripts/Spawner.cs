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
	public float flyerSpeed = 1f;
	float timer; 

	public float startRockFrequency;
	public float startPowerupFrequency;
	public float startNumberFrequency;
	public float startFlyerSpeed;
	float startTimer = 20f;
	float startStartTimer = 20f;

	// Use this for initialization
	void Start () {
	}

	public void StartGame() {
		StartCoroutine("GenerateNumbers");
		StartCoroutine("GenerateRocks");
		StartCoroutine("GeneratePowerups");
		timer = startStartTimer;
		flyerSpeed = startFlyerSpeed;
		rockFrequency = startRockFrequency;
		powerupFrequency = startPowerupFrequency;
		numberFrequency = startNumberFrequency;
	}

	public void StopGame() {
		StopAllCoroutines();
	}

	void Update() {
		if (levelManager.gameStarted) {
			timer -= Time.deltaTime;
			if (timer < 0) {
				timer = startTimer;
				startTimer -= .7f;
				if (startTimer < 5) {
					startTimer = 5f;
				}
				rockFrequency -= .25f;
				if (rockFrequency < .5) {
					rockFrequency = .5f;
				}
				numberFrequency -= .1f;
				if (numberFrequency < .2) {
					numberFrequency = .2f;
				}
				powerupFrequency += .2f;
				flyerSpeed += .03f;
			}
		}
	}

	IEnumerator GenerateNumbers() {
		while (true) {
			Instantiate(numberPrefab);
			yield return new WaitForSeconds(UnityEngine.Random.Range(0f, numberFrequency));
		}
	}

	IEnumerator GenerateRocks() {
		yield return new WaitForSeconds(9);
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
