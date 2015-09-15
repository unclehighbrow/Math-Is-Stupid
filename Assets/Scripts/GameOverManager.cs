using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour {
	public int score;
	public int highScore;

	public Text scoreText;
	public Text highScoreText;

	// Use this for initialization
	void Start () {
		score = GameSingleton.Instance.score;
		highScore = PlayerPrefs.GetInt("highScore", 0);
		if (score > highScore) {
			highScore = score;
			PlayerPrefs.SetInt("highScore", highScore);
		}
		scoreText.text = score.ToString().PadLeft(5, '0');;
		highScoreText.text = highScore.ToString().PadLeft(5, '0');;
	}

	public void StartGame() {
		GameSingleton.Instance.StartGame();
	}

	// Update is called once per frame
	void Update () {
	
	}
}
