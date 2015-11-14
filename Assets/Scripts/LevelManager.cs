using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
	
	public DeepThroat deepThroat;
	public Spawner spawner;
	public GameObject deepThroatPrefab;
	public InputManager inputManager;

	public int minGoal;
	public int maxGoal;
	
	public float maxX;
	public float minX;
	public float maxY;
	public float minY;

	public string currentOperand;
	public int currentTotal;
	public int secondVarHolder;
	public int[] goals = {0,0,0,0};

	public delegate int DoMath(int x, int y);
	public static Dictionary<string,LevelManager.DoMath> operandMap;
	public static List<string> operandList;

	public Text calcText;
	public Text[] goalTexts;
	public Text scoreText;
	public Text timerText;
	public Slider energySlider;
	public Text powerupDisplay;
	public Text highScoreText;

	public CanvasGroup pausePanel;
	public CanvasGroup gameCanvas;
	public CanvasGroup titleCanvas;

	public Canvas canvas;

	public float startTimer;
	public float timer;
	public float goalAddend;

	public GameObject operands;

	public int rockPenalty;
	public int energyPenalty;
	float energySubtractor = .06f;

	bool performingOperation = false;
	bool goalMet = false;
	bool gameOvering = false;

	string tempPowerup;

	public bool invincible = false;

	Dictionary<string,float> powerupTimers = new Dictionary<string, float>();

	public bool gameStarted = false;
	public bool paused = false;

	AudioSource audioSource;
	public AudioClip numberSound;
	public AudioClip operandSound; 
	public AudioClip startGameSound; 
	public AudioClip calculatingSound; 
	public AudioClip choosingSound; 
	public AudioClip gameOverSound; 
	public AudioClip doingMathSound;
	public AudioClip goalHitSound;
	public AudioClip hurtSound;
	public AudioSource music;

	Color transparentButton = new Color(1f, 1f, 1f, .39f);
	Color transparentButtonPressed = new Color(.5f, .5f, .5f, .39f);

	
	private Vector2 GetGameView() {
		System.Type T = System.Type.GetType("UnityEditor.GameView,UnityEditor");
		System.Reflection.MethodInfo getSizeOfMainGameView =
			T.GetMethod("GetSizeOfMainGameView",System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
		System.Object resolution = getSizeOfMainGameView.Invoke(null, null);
		return (Vector2)resolution;
	}

	// Use this for initialization
	void Start () {
		//PlayerPrefs.DeleteAll();
		deepThroat = GameObject.FindObjectOfType<DeepThroat>();
		inputManager = GameObject.FindObjectOfType<InputManager>(); 
		operandMap = new Dictionary<string,DoMath>() {
			{"+", (x,y) => { return x + y; }},
			{"-", (x,y) => { return x - y; }},
			{"*", (x,y) => { return x * y; }},
			{"/", (x,y) => { return x / y; }}
		};
		operandList = new List<string>();
		foreach (KeyValuePair<string,DoMath> kvp in operandMap)  {
			operandList.Add(kvp.Key);
		}
		if (GameSingleton.Instance.highScore > 0) {
			highScoreText.text = "high " + GameSingleton.Instance.highScore.ToString().PadLeft(5, '0');
		} else {
			highScoreText.text = "";
		}
		gameCanvas.alpha = 0;
		titleCanvas.gameObject.SetActive(true);
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (gameStarted) {
			CheckGoal();
			UpdateScore();
			UpdateTimers();
		}
	}

	IEnumerator GameOver() {
		if (invincible) {
			yield return null;
		} else {
			gameOvering = true;
			music.Stop();
			audioSource.PlayOneShot(gameOverSound);
			HandleOperand("");
			gameStarted = false;
			deepThroat.dead = true;
			spawner.StopGame();
			deepThroat.GetComponent<Rigidbody2D>().AddForce(new Vector2(300, -500));
			deepThroat.GetComponent<Rigidbody2D>().AddTorque(-30);
			yield return new WaitForSeconds(1.5f);
			Destroy (deepThroat);
			deepThroat = Instantiate(deepThroatPrefab).GetComponent<DeepThroat>();
			deepThroat.transform.position = new Vector2(-10, 0);
			inputManager.deepThroat = deepThroat;
			yield return StartCoroutine(FadeInFadeOutCanvas(titleCanvas, gameCanvas));
			titleCanvas.blocksRaycasts = true;
			GameSingleton.Instance.RecordScore();
			highScoreText.text = "high " + GameSingleton.Instance.highScore.ToString().PadLeft(5, '0');
			gameOvering = false;
			yield return null;
		}
	}

	void UpdateTimers() {
		if (timer <= 0f) {
			StartCoroutine(GameOver());
			timer = 0f;
		} else if (IsPowerupActive(Powerup.slowDownTimer)) {
			timer -= (Time.deltaTime/3);
		} else {
			timer -= Time.deltaTime;
		}
		timerText.text = timer.ToString("F2");
		if (!IsPowerupActive(Powerup.unlimitedTimeJuice)) {
			energySlider.value -= Time.deltaTime * energySubtractor;
		}
		if (energySlider.value <= 0) {
			timer -= energyPenalty;
			energySlider.value = 1f;
			deepThroat.Shake();
		}

		foreach (string powerup in Powerup.powerups) {
			if (powerupTimers[powerup] > 0) {
				powerupTimers[powerup] -= Time.deltaTime;
				if (powerupTimers[powerup] <=0) {
					DeactivatePowerup(powerup);
				}
			}
		}
	}

	void ResetTimer() {
		timer = startTimer;
	}

	void UpdateScore() {
		int currentDisplay = Convert.ToInt32(scoreText.text);
		if (GameSingleton.Instance.score > currentDisplay) {
			string scoreString = (currentDisplay + 10).ToString();
			scoreText.text = scoreString.PadLeft(5, '0');
		}
	}

	void CheckGoal() {
		for (int i = 0; i < goals.Length; i++) {
			int goal = goals[i];
			if (goal != 0 && !goalMet) {
				if ((IsPowerupActive(Powerup.roundBy2) && Util.between(goal, currentTotal - 2, currentTotal + 2)) 
				    || currentTotal == goal) {
					StartCoroutine(GoalMet(i));
				}
			}
		}
	}

	IEnumerator GoalMet(int i) {
		goalMet = true;
		audioSource.PlayOneShot(goalHitSound);
		StartCoroutine(Util.Glow(calcText));
		yield return StartCoroutine(Util.Glow(goalTexts[i]));
		yield return StartCoroutine(RandomizeGoals());
		StartCoroutine(GenerateGoals());
		GameSingleton.Instance.score += 1000;
		timer += goalAddend;
		goalMet = false;
		yield return null;
	}

	public IEnumerator RandomizeGoals() {
		int goalCount = IsPowerupActive(Powerup.moreGoals) ? 3 : 2;
		for (int i = 0; i <= 10; i++) {
			for (int j = 0; j <= goalCount; j++) {
				goalTexts[j].text = Util.RandomNumber();
			}
			audioSource.PlayOneShot(calculatingSound);
			yield return new WaitForSeconds(.05f);
		}
		yield return null;
	}

	public IEnumerator GenerateGoals() {
		audioSource.PlayOneShot(choosingSound);
		int goalCount = IsPowerupActive(Powerup.moreGoals) ? 3 : 2;
		for (int i = 0; i <= goalCount; i++) {
			int goal = GenerateGoal();
			goalTexts[i].text = goal.ToString();
			goals[i] = goal;
		}
		yield return null;
	}

	public int GenerateGoal() {
		int goal = 0;
		while (goal == 0 || Array.IndexOf(goals, goal) > -1) {
			goal = UnityEngine.Random.Range(minGoal, maxGoal);
		}
		return goal;
	}

	IEnumerator HandlePowerupCo() {
		string powerup = tempPowerup;
		for (int i = 0; i<15; i++) {
			powerupDisplay.text = Util.RandomString(powerup);
			audioSource.PlayOneShot(calculatingSound);
			yield return new WaitForSeconds(.05f);
		}
		powerupDisplay.text = powerup;
		audioSource.PlayOneShot(choosingSound);
		ActivatePowerup(powerup);
		yield return StartCoroutine(Util.Glow(powerupDisplay));
		for (int i = 0; i<5; i++) {
			powerupDisplay.text = Util.RandomString(powerup);
			yield return new WaitForSeconds(.02f);
		}
		powerupDisplay.text = "";
		tempPowerup = "";
		yield return null;
	}

	public void HandleOperand(string operand) {
		if (!Util.IsEmpty(currentTotal)) {
			if (!Util.IsEmpty(operand)) {
				audioSource.PlayOneShot(operandSound);
			}
			if (performingOperation && !Util.IsEmpty(operand)) { // perform operation now
				StopCoroutine("PerformOperation");
				currentTotal = operandMap[currentOperand](currentTotal, secondVarHolder);
				calcText.text = currentTotal.ToString();
				secondVarHolder = 0;
				performingOperation = false;
			}
			currentOperand = operand;
			calcText.text = currentTotal.ToString() + " " + operand;
			deepThroat.GetComponentInChildren<TextMesh>().text = operand;
			foreach (Text iOperand in operands.GetComponentsInChildren<Text>()) {
				if (iOperand.text.Equals(operand)) {
					iOperand.color = Color.yellow;
					iOperand.GetComponentInChildren<Image>().color = transparentButtonPressed;
					StartCoroutine(Util.Glow(iOperand));
				} else {
					iOperand.color = Color.white;
					iOperand.GetComponentInChildren<Image>().color = transparentButton;
				}
			}
		}
	}

	public void HandlePowerup(string powerup) {
		if (!Util.IsEmpty(tempPowerup)) {
			ActivatePowerup(tempPowerup);
		}
		StopCoroutine("HandlePowerupCo");
		tempPowerup = powerup;
		StartCoroutine("HandlePowerupCo");
	}

	void ActivatePowerup(string powerup) {
		powerupTimers[powerup] += 15;
		if (powerup == Powerup.moreGoals) { 
			goalTexts[3].CrossFadeAlpha(1f, 0f, false);
			int goal = GenerateGoal();
			goalTexts[3].text = goal.ToString();
			goals[3] = goal;
			StartCoroutine(Util.Glow(goalTexts[3]));
		} else if (powerup == Powerup.invinciblity) {
			deepThroat.animator.SetBool("invincible", true);
		} else if (powerup == Powerup.unlimitedTimeJuice) {
			energySlider.value = 1;
		}
	}

	void DeactivatePowerup(string powerup) {
		if (powerup == Powerup.moreGoals) {
			goalTexts[3].CrossFadeAlpha(0, 1, false);
			goals[3] = 0;
		} else if (powerup == Powerup.invinciblity) {
			deepThroat.animator.SetBool("invincible", false);
		}
	}

	bool IsPowerupActive(string powerup) {
		return powerupTimers[powerup] > 0;
	}

	public void HandleRock() {
		if (!IsPowerupActive(Powerup.invinciblity)) {
			audioSource.PlayOneShot(hurtSound);
			timer -= rockPenalty;
			deepThroat.Shake();
		}
	}

	public void HandleNumber(Number number) {
		if (Util.IsEmpty(currentTotal)) {
			audioSource.PlayOneShot(numberSound);
			currentTotal = number.number;
			calcText.text = currentTotal.ToString();
			Destroy (number.gameObject);
		} else if (!Util.IsEmpty(currentOperand) && !performingOperation) {
			audioSource.PlayOneShot(numberSound);
			secondVarHolder = number.number;
			StartCoroutine("PerformOperation");
			GameSingleton.Instance.score += 10;
			Destroy(number.gameObject);
		}
	}

	public IEnumerator PerformOperation() {
		performingOperation = true;
		energySlider.value = 1f;

		// set second var
		calcText.text = currentTotal + " " + currentOperand + " " + secondVarHolder.ToString();
		audioSource.PlayOneShot(doingMathSound);
		yield return new WaitForSeconds(.3f);

		// set equal
		calcText.text = currentTotal + " " + currentOperand + " " + secondVarHolder.ToString() + " = ";
		audioSource.PlayOneShot(doingMathSound);
		yield return new WaitForSeconds(.3f);

		// set result
		int tempCurrentTotal = operandMap[currentOperand](currentTotal, secondVarHolder);
		calcText.text = currentTotal + " " + currentOperand + " " + secondVarHolder.ToString() + " = " + tempCurrentTotal.ToString();
		audioSource.PlayOneShot(doingMathSound);
		yield return new WaitForSeconds(.3f);

		// reset everyting
		HandleOperand("");
		currentTotal = tempCurrentTotal;
		calcText.text = currentTotal.ToString();
		secondVarHolder = 0;
		performingOperation = false;
		yield return null;
	}

	public void Pause() {
		if (gameStarted) {
			Time.timeScale = 0;
			pausePanel.alpha = 1;
			pausePanel.blocksRaycasts = true;
			paused = true;
		}
	}	

	public void Resume() {
		pausePanel.alpha = 0;
		pausePanel.blocksRaycasts = false;
		Time.timeScale = 1;
		paused = false;
	}
	
	void OnApplicationPause(bool pauseStatus) {
		if (pauseStatus) {
			Pause();
		}
	}

	public void StartGame() {		
		StartCoroutine("StartGameCo");
	}

	IEnumerator StartGameCo() {	
		if (!gameOvering) {
			audioSource.PlayOneShot(startGameSound);
			titleCanvas.blocksRaycasts = false;
			calcText.text = "0";
			powerupDisplay.text = "";
			currentOperand = "";
			currentTotal = 0;
			secondVarHolder = 0;
			goalTexts[3].CrossFadeAlpha(0, 0, false);
			goals[3] = 0;
			ResetTimer();
			energySlider.value = 1f;
			timerText.text = timer.ToString("F2");
			foreach (string powerup in Powerup.powerups) {
				powerupTimers[powerup] = 0;
			}
			StartCoroutine(GenerateGoals());
			GameSingleton.Instance.score = 0;
			scoreText.text = "00000";
			StartCoroutine(FadeInFadeOutCanvas(gameCanvas, titleCanvas));
			Vector2 destination = new Vector2(-5.5f,0f);
			while ((Vector2)deepThroat.transform.position != destination) {
				Vector2 p = Vector2.MoveTowards(deepThroat.transform.position, destination, deepThroat.speed/3f);
				deepThroat.GetComponent<Rigidbody2D>().MovePosition(p);
				yield return new WaitForEndOfFrame();
			}
			yield return new WaitForSeconds(1);
			spawner.StartGame();
			music.Play();
			gameStarted = true;
		}
		yield return null;
	}

	public void LoadTutorial() {
		Application.LoadLevel("Tutorial");
	}


	IEnumerator FadeInFadeOutCanvas(CanvasGroup fadeIn, CanvasGroup fadeOut) {
		while (fadeIn.alpha < 1) {
			fadeIn.alpha += .01f;
			fadeOut.alpha -= .01f;
			yield return new WaitForSeconds(.01f);
		}
		fadeOut.alpha = 0;
	}

	public void ShowLeaderboardUI() {
		Social.ShowLeaderboardUI();
	}
}
