using UnityEngine;
using System.Collections;

public class GameSingleton : Singleton<GameSingleton> {

	public int score;

	public void StartGame() {
		score = 0;
		Application.LoadLevel("Space");
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
