using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public LevelManager levelManager;

	public GameObject numberPrefab;
	public GameObject rockPrefab;
	public GameObject powerupPrefab;

	public float rockFrequency;
	public float powerupFrequency;
	public float numberFrequency;

	public float flyerFrequency;

	// Use this for initialization
	void Start () {
		StartCoroutine("GenerateFlyers");
	}
	
	IEnumerator GenerateFlyers() {
		while (true) {
			int randomNumber = UnityEngine.Random.Range(0, 25);
			if (randomNumber < 15) {
				Instantiate(numberPrefab);
			} else if (randomNumber == 19) {
				Instantiate(powerupPrefab);
			} else {
				Instantiate(rockPrefab);
			}
			float secondsToWait = UnityEngine.Random.Range(0f, flyerFrequency);
			yield return new WaitForSeconds(secondsToWait);
		}
	}
}
