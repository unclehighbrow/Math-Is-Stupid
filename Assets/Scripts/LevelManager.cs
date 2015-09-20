using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
	
	public DeepThroat deepThroat;

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
	
	public float startTimer;
	public float timer;
	public float goalAddend;

	public GameObject operands;

	public int rockPenalty;
	public int energyPenalty;
	float energySubtractor = .06f;

	bool performingOperation = false;
	bool goalMet = false;

	string tempPowerup;

	public bool invincible = false;

	Dictionary<string,float> powerupTimers = new Dictionary<string, float>();

	// Use this for initialization
	void Start () {
		deepThroat = GameObject.FindObjectOfType<DeepThroat>();
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
		calcText.text = "0";
		powerupDisplay.text = "";

		foreach (string powerup in Powerup.powerups) {
			powerupTimers.Add(powerup, 0);
		}

		goalTexts[3].CrossFadeAlpha(0f, 0f, false);

		StartCoroutine(GenerateGoals());
		ResetTimer();
	}
	
	// Update is called once per frame
	void Update () {
		CheckGoal();
		UpdateScore();
		UpdateTimers();
	}

	IEnumerator GameOver() {
		if (invincible) {
			yield return null;
		} else {
			deepThroat.dead = true;
			deepThroat.GetComponent<Rigidbody2D>().AddForce(new Vector2(30, -50));
			deepThroat.GetComponent<Rigidbody2D>().AddTorque(-3);
			yield return new WaitForSeconds(2);
			Application.LoadLevel("GameOver");
			yield return null;
		}
	}

	void UpdateTimers() {
		if (timer <= 0f) {
			StartCoroutine(GameOver());
			timer = 0;
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
		int currentDisplay = Convert.ToInt16(scoreText.text);
		if (GameSingleton.Instance.score > currentDisplay) {
			string scoreString = (currentDisplay + 10).ToString();
			scoreText.text = scoreString.PadLeft( 5, '0');
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
		StartCoroutine(Glow(calcText));
		yield return StartCoroutine(Glow(goalTexts[i]));
		StartCoroutine(GenerateGoals());
		GameSingleton.Instance.score += Mathf.FloorToInt(timer) * 10;
		timer += goalAddend;
		goalMet = false;
		yield return null;
	}

	public IEnumerator GenerateGoals() {
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

	IEnumerator Glow(Text text) {
		Outline outline = text.GetComponent<Outline>();
		if (outline != null) {
			outline.enabled = true;	
			for (int i = 0; i<12; i++) {
				outline.effectDistance += Vector2.one * .25f;
				yield return new WaitForSeconds(.04f);
			}
			for (int i = 0; i<10; i++) {
				outline.effectDistance -= Vector2.one * .3f;
				yield return new WaitForSeconds(.1f);
			}
			outline.effectDistance = Vector2.zero;
			outline.enabled = false;
		} 
		yield return null;
	}


	IEnumerator HandlePowerupCo() {
		string powerup = tempPowerup;
		for (int i = 0; i<15; i++) {
			powerupDisplay.text = Util.RandomString(powerup);
			yield return new WaitForSeconds(.05f);
		}
		powerupDisplay.text = powerup;
		ActivatePowerup(powerup);
		yield return StartCoroutine(Glow(powerupDisplay));
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
			if (performingOperation && !Util.IsEmpty(operand)) { // perform operation now
				StopCoroutine("PerformOperation");
				currentTotal = operandMap[currentOperand](currentTotal, secondVarHolder);
				calcText.text = currentTotal.ToString();
				secondVarHolder = 0;
				performingOperation = false;
			}
			currentOperand = operand;
			calcText.text = currentTotal.ToString() + " " + operand;
			foreach (Text iOperand in operands.GetComponentsInChildren<Text>()) {
				if (iOperand.text.Equals(operand)) {
					iOperand.color = Color.yellow;
					StartCoroutine(Glow(iOperand));
				} else {
					iOperand.color = Color.white;
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
			StartCoroutine(Glow(goalTexts[3]));
		}
	}

	void DeactivatePowerup(string powerup) {
		if (powerup == Powerup.moreGoals) {
			goalTexts[3].CrossFadeAlpha(0, 1, false);
			goals[3] = 0;
		}
	}

	bool IsPowerupActive(string powerup) {
		return powerupTimers[powerup] > 0;
	}

	public void HandleRock() {
		if (!IsPowerupActive(Powerup.invinciblity)) {
			timer -= rockPenalty;
			deepThroat.Shake();
		}
	}

	public void HandleNumber(int number) {
		if (Util.IsEmpty(currentTotal)) {
			currentTotal = number;
			calcText.text = currentTotal.ToString();
		} else if (!Util.IsEmpty(currentOperand) && !performingOperation) {
			secondVarHolder = number;
			StartCoroutine("PerformOperation");
			GameSingleton.Instance.score += 10;
		}
	}

	public IEnumerator PerformOperation() {
		performingOperation = true;
		energySlider.value = 1f;

		// set second var
		calcText.text = currentTotal + " " + currentOperand + " " + secondVarHolder.ToString();
		yield return new WaitForSeconds(.3f);

		// set equal
		calcText.text = currentTotal + " " + currentOperand + " " + secondVarHolder.ToString() + " = ";
		yield return new WaitForSeconds(.3f);

		// set result
		int tempCurrentTotal = operandMap[currentOperand](currentTotal, secondVarHolder);
		calcText.text = currentTotal + " " + currentOperand + " " + secondVarHolder.ToString() + " = " + tempCurrentTotal.ToString();
		yield return new WaitForSeconds(.3f);

		// reset everyting
		HandleOperand("");
		currentTotal = tempCurrentTotal;
		calcText.text = currentTotal.ToString();
		secondVarHolder = 0;
		performingOperation = false;
		yield return null;
	}
}
