using UnityEngine;
using System.Collections;

public class TitleManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public void StartGame() {
		GameSingleton.Instance.StartGame();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
