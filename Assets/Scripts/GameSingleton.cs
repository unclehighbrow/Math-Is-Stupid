using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;
using UnityEngine.SceneManagement;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GameSingleton : Singleton<GameSingleton> {

	public int score;
	public int highScore;
	bool loggedIn = false;
	public string leaderboard = "mathisstupid";

	public void StartGame() {
		score = 0;
		SceneManager.LoadScene("Space");
	}

	// Use this for initialization
	void Awake () {
		highScore = PlayerPrefs.GetInt("highScore", 0);
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
		PlayGamesPlatform.InitializeInstance(config);
		PlayGamesPlatform.Activate();
		Social.localUser.Authenticate(ProcessAuthentication);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ProcessAuthentication (bool success) {
		if (success) {
			loggedIn = true;
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

	public void ReportAchievement(string acheivement) {
		Social.ReportProgress(acheivement, 100.0f, (bool success) => {
			Debug.Log ("reporiting acheivement: " + acheivement);
		});
	}
}
