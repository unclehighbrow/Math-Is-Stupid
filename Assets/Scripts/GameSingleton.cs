using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;

public class GameSingleton : Singleton<GameSingleton> {

	public int score;
	public int highScore;
	bool loggedIn = false;
	public string leaderboard = "mathisstupid";

	public void StartGame() {
		score = 0;
		Application.LoadLevel("Space");
	}

	// Use this for initialization
	void Awake () {
		highScore = PlayerPrefs.GetInt("highScore", 0);
		Social.localUser.Authenticate(ProcessAuthentication);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ProcessAuthentication (bool success) {
		if (success) {
			loggedIn = true;
		} else {
			Debug.Log ("Failed to authenticate");
		}
	}
	
	void FetchScores() {
		if (loggedIn) {
			Social.LoadScores(leaderboard, scores => {
				if (scores.Length > 0) {
					string myScores = "Leaderboard:\n";
					foreach (IScore score in scores)
						myScores += "\t" + score.userID + " " + score.formattedValue + " " + score.date + "\n";
				}
			});
		}
	}
	
	public bool RecordScore() {
		bool ret = false;
		if (score > highScore) {
			ret = highScore != 0;
			highScore = score;
			PlayerPrefs.SetInt("highScore", score);
		}
		if (loggedIn) {
			Social.ReportScore (score, leaderboard, success => {
				Debug.Log(success ? "Reported score successfully" : "Failed to report score");
			});
		}
		return ret;
	}
	
	public void ShowLeaderboardUI() {
		Social.ShowLeaderboardUI();
	}
}
