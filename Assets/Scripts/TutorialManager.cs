using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class TutorialManager : MonoBehaviour {
	
	public Text uiText;
	public Text shadowText;
	public Image pointer;
	public Image nixon;
	public Image deepThroat;
	public DeepThroat realDeepThroat;
	
	protected int currentLines;
	protected bool next = false;
	protected bool writing = false;
	protected int mode = 1;
	protected int goalsHit = 0;
	protected float writingDelayDefault = .02f;
	protected float writingDelay = .02f;

	public Text calcText;
	public Text goal1;
	public Text goal2;
	public Text goal3;
	public Text timerText;
	public Slider mathJuice;
	public CanvasGroup gameCanvas;
	
	protected int Line = 0;
	
	public void Start() {
		StartCoroutine("Tutorial");
	}

	static void Hide(Graphic g) {
		g.CrossFadeAlpha(0f, 0f, false);
	}
	static void Show(Graphic g) {
		g.CrossFadeAlpha(1f, 0f, false);
	}

	// fdkslaj fdslkafjdslakfjd salfkjd slakfj dlsakjf dlsak jfdlsakjf dlskajf dlksafj  fd

	IEnumerator Tutorial() {
		gameCanvas.alpha = 0;
		realDeepThroat.gameObject.SetActive(false);


		Hide(deepThroat);
		Show(nixon);
		StartCoroutine(DisplayLine("Deep Throat, hear my call!"));		
		while (!next) {	yield return new WaitForEndOfFrame(); }

		Show(deepThroat);
		Hide(nixon);
		StartCoroutine(DisplayLine("Wait, what? What's going on? Richard Nixon?!??!"));
		while (!next) {	yield return new WaitForEndOfFrame(); }

		Show(nixon);
		Hide(deepThroat);
		StartCoroutine(DisplayLine("Correct. We must put our political differences aside and you must save the universe!"));
		while (!next) {	yield return new WaitForEndOfFrame(); }

		Show(deepThroat);
		Hide(nixon);
		StartCoroutine(DisplayLine("Oh. Okay. Sure."));
		while (!next) {	yield return new WaitForEndOfFrame(); }

		Show(nixon);
		Hide(deepThroat);
		StartCoroutine(DisplayLine("The fabric of space time is falling apart."));
		while (!next) {	yield return new WaitForEndOfFrame(); }

		StartCoroutine(DisplayLine("and the only way to stop it is by doing math equations in space on a purple surfboard."));
		while (!next) {	yield return new WaitForEndOfFrame(); }
		
		Show(deepThroat);
		Hide(nixon);
		StartCoroutine(DisplayLine("A purple surfboard?"));
		while (!next) {	yield return new WaitForEndOfFrame(); }

		Show(nixon);
		Hide(deepThroat);
		StartCoroutine(DisplayLine("Yeah, you're already on it."));
		while (!next) {	yield return new WaitForEndOfFrame(); }

		Show(deepThroat);
		Hide(nixon);
		StartCoroutine(DisplayLine("Oh, you're right. Let's get to it!"));
		while (!next) {	yield return new WaitForEndOfFrame(); }

		goal1.text = "5";
		goal2.text = "7";
		goal3.text = "16";
		timerText.text = "90:00";
		deepThroat.CrossFadeAlpha(0f, 1f, false);
		realDeepThroat.gameObject.SetActive(true);
		Hide (nixon);
		while (gameCanvas.alpha < 1f) {
			gameCanvas.alpha += .01f;
			yield return new WaitForEndOfFrame();
		}

		StartCoroutine(DisplayLine("You fly around on the left. Move up and down by tapping the left side of the screen!"));
		while (!next) {	yield return new WaitForEndOfFrame(); }

		yield return null;
	}
	
	public void Update() {
		bool click = false;
		// mouse
		#if UNITY_EDITOR
		if (Input.GetMouseButtonDown(0)) {
			click = true;
		}
		#endif
		// touch
		Touch[] touches = Input.touches;
		for (int i = 0 ; i < Input.touchCount; i++) {
			Touch touch = touches[i];
			if (touch.phase == TouchPhase.Began) {
				click = true;
			}
		}
		if (click) {
			if (writing) {
				writingDelay = 0f;
			} else {
				writingDelay = writingDelayDefault;
				StopPointer();
				next = true;
				Line++;
			}
		}
	}
	
	public void StopPointer() {
		StopCoroutine("Pointer");
		pointer.gameObject.SetActive(false);
	}
	
	public IEnumerator DisplayLine(string text) {
		StopPointer();
		while (writing) {
			yield return new WaitForEndOfFrame();
		}
		writing = true;
		next = false;
		shadowText.text = "";
		uiText.text = "";
		currentLines = 1;
		string[] words = text.Split(' ');
		yield return new WaitForEndOfFrame(); // this renders the shadowtext to reset it
		while (uiText.text.Length < text.Length) {
			if (words.Length >= 2) {
				shadowText.text = words[0] + " " + words[1] + " "; // the shadow text is looking two words ahead, i think because it needs to wait for the render?
			}
			for (int i = 0; i < words.Length; i++) {
				string word = words[i];
				
				bool newLine = false;
				if (i + 3 <= words.Length) {
					shadowText.text += words[i + 2] + " ";
				} 
				if (shadowText.cachedTextGenerator.lineCount > currentLines) {
					currentLines = shadowText.cachedTextGenerator.lineCount;
					newLine = true;
				}	
				
				foreach (char letter in word) {
					uiText.text += letter;
					yield return new WaitForSeconds(writingDelay);
				}
				
				if (newLine) {
					uiText.text += "\n";
					newLine = false;
				} else {
					uiText.text += " ";
				}
			}
		}
		StartCoroutine("Pointer");
		writing = false;
	}
	
	public IEnumerator Pointer() {
		while (true) {
			pointer.gameObject.SetActive(!pointer.gameObject.activeSelf);
			yield return new WaitForSeconds(.7f);
		}
	}
}
