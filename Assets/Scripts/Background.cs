using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {
	public Texture[] textures;
	Renderer rend;
	float scrollTimer = 0;
	int timeTilScroll = 5;
	LevelManager levelManager;

	public GameObject stars;
	Vector3 starsStartPos;

	void Start () {
		rend = GetComponent<Renderer>();
		levelManager = GameObject.FindObjectOfType<LevelManager>();
		starsStartPos = stars.transform.position;
		transform.position = new Vector3(levelManager.maxX + rend.bounds.size.x, transform.position.y, transform.position.z);
		timeTilScroll = 7;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x == 0 || (transform.position.x > 0 && !rend.isVisible)) { // wait
			scrollTimer += Time.deltaTime;
		}
		if (scrollTimer > timeTilScroll) { // scroll
			Vector3 pos = new Vector3(transform.position.x - .5f, transform.position.y, transform.position.z);
			transform.position = pos;
			if (transform.position.x < 0 && !rend.isVisible) { // reset to right, change second texture
				Texture texture = textures[Random.Range(0,textures.Length)];
				rend.material.SetTexture("_PaletteTex", texture);
				transform.position = new Vector3(levelManager.maxX + rend.bounds.size.x, transform.position.y, transform.position.z);
				scrollTimer = 0;
			} else if (transform.position.x == 0) { // back to waiting
				scrollTimer = 0;
			}
			if (scrollTimer == 0) {
				timeTilScroll = Random.Range(5,10);
			}
		}

		// other background handling
		if (transform.position.x == 0) {
			stars.transform.position = starsStartPos;
		} else {
			Vector3 pos = new Vector3(stars.transform.position.x - .009f, stars.transform.position.y, stars.transform.position.z);
			stars.transform.position = pos;
		}

		rend.material.SetFloat("_UVXOffset", rend.material.GetFloat("_UVXOffset") + -.01f);
	}

	void OnTrigger2D() {

	}
}
