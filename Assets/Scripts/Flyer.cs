using UnityEngine;
using System.Collections;

public class Flyer : MonoBehaviour {

	Vector3 rotation;
	LevelManager levelManager;
	Spawner spawner;

	// Use this for initialization
	public void Start () {
		levelManager = GameObject.FindObjectOfType<LevelManager>();
		spawner = GameObject.FindObjectOfType<Spawner>();
		if (spawner != null) {
			transform.position = new Vector2(levelManager.maxX, UnityEngine.Random.Range(levelManager.minY, levelManager.maxY));
			GetComponent<Rigidbody2D>().AddForce(new Vector2(-200 * spawner.flyerSpeed * UnityEngine.Random.Range(1f, 1.5f), 0));
		} 
		rotation = new Vector3(0,0,Random.Range (-3f,3f));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() {
		transform.GetChild(0).Rotate(rotation);
	}
}
