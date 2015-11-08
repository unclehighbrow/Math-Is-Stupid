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
	protected bool pointing = false;
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
	public Text plusOperand;

	public GameObject numberPrefab;
	public GameObject pointerPrefab;

	GameObject bouncePointer1;
	GameObject bouncePointer2;
	
	protected int Line = 0;

	AudioSource audioSource;
	public AudioClip deepThroatTalk;
	public AudioClip nixonTalk;
	
	public void Start() {
		realDeepThroat.speed = .035f;
		audioSource = GetComponent<AudioSource>();
		StartCoroutine("Tutorial");
	}

	static void Hide(Graphic g) {
		g.CrossFadeAlpha(0f, 0f, false);
	}
	static void Show(Graphic g) {
		g.CrossFadeAlpha(1f, 0f, false);
	}

	public void Next() {
		next = true;
	}

	// fdkslaj fdslkafjdslakfjd salfkjd slakfj dlsakjf dlsak jfdlsakjf dlskajf dlksafj  fd

	IEnumerator Tutorial() {
		gameCanvas.alpha = 0;
		realDeepThroat.gameObject.SetActive(false);

		Hide(deepThroat);
		Show(nixon);
		StartCoroutine(DisplayLine("Deep Throat, hear my call!", true, nixonTalk));		
		while (!next) {	yield return new WaitForEndOfFrame(); }

		Show(deepThroat);
		Hide(nixon);
		StartCoroutine(DisplayLine("Wait, what? What's going on? Richard Nixon?!??!", true, deepThroatTalk));
		while (!next) {	yield return new WaitForEndOfFrame(); }

		Show(nixon);
		Hide(deepThroat);
		StartCoroutine(DisplayLine("Correct. We must put our political differences aside and you must save the universe!", true, nixonTalk));
		while (!next) {	yield return new WaitForEndOfFrame(); }

		Show(deepThroat);
		Hide(nixon);
		StartCoroutine(DisplayLine("Oh. Okay. Sure.", true, deepThroatTalk));
		while (!next) {	yield return new WaitForEndOfFrame(); }

		Show(nixon);
		Hide(deepThroat);
		StartCoroutine(DisplayLine("The fabric of space time is falling apart.", true, nixonTalk));
		while (!next) {	yield return new WaitForEndOfFrame(); }

		StartCoroutine(DisplayLine("and the only way to stop it is by doing math equations in space on a purple surfboard.", true, nixonTalk));
		while (!next) {	yield return new WaitForEndOfFrame(); }
		
		Show(deepThroat);
		Hide(nixon);
		StartCoroutine(DisplayLine("A purple surfboard?", true, deepThroatTalk));
		while (!next) {	yield return new WaitForEndOfFrame(); }

		Show(nixon);
		Hide(deepThroat);
		StartCoroutine(DisplayLine("Yeah, you're already on it.", true, nixonTalk));
		while (!next) {	yield return new WaitForEndOfFrame(); }

		Show(deepThroat);
		Hide(nixon);
		StartCoroutine(DisplayLine("Oh, you're right. Let's get to it!", true, deepThroatTalk));
		while (!next) {	yield return new WaitForEndOfFrame(); }

		goal1.text = "5";
		goal2.text = "7";
		goal3.text = "16";
		timerText.text = "90:00";
		deepThroat.CrossFadeAlpha(0f, 1f, false);
		realDeepThroat.gameObject.SetActive(true);
		Hide(nixon);
		while (gameCanvas.alpha < 1f) {
			gameCanvas.alpha += .01f;
			yield return new WaitForEndOfFrame();
		}

		StartCoroutine(DisplayLine("Move up and down by tapping the left side of the screen!", true, nixonTalk));
		bouncePointer1 = Instantiate(pointerPrefab);
		bouncePointer1.transform.position = new Vector2(realDeepThroat.gameObject.transform.position.x, realDeepThroat.gameObject.transform.position.y - 3);
		realDeepThroat.destination = bouncePointer1.transform.position;
		bouncePointer2 = Instantiate(pointerPrefab);
		bouncePointer2.transform.position = new Vector2(realDeepThroat.gameObject.transform.position.x, realDeepThroat.gameObject.transform.position.y);
		bouncePointer2.SetActive(false);
		while (!next) {	yield return new WaitForEndOfFrame(); }


		bouncePointer1.SetActive(false);
		bouncePointer2.SetActive(false);
		realDeepThroat.destination = realDeepThroat.transform.position;
		StartCoroutine(DisplayLine("Numbers will fly at you. Here's comes a four!", false, nixonTalk));
		GameObject four = Instantiate(numberPrefab);
		four.GetComponent<Number>().SetValue(4);
		four.transform.position = new Vector2(10, realDeepThroat.transform.position.y);
		four.GetComponent<Rigidbody2D>().AddForce(new Vector2(-250, 0));
		while (!next) {	yield return new WaitForEndOfFrame(); }

		calcText.text = "4";
		StartCoroutine(DisplayLine("Now you have a number to do math with!", true, nixonTalk));
		calcText.transform.Find("Arrow").gameObject.SetActive(true);
		while (!next) {	yield return new WaitForEndOfFrame(); }

		calcText.transform.Find("Arrow").gameObject.SetActive(false);
		StartCoroutine(DisplayLine("You're trying to get to one of these three goals.", true, nixonTalk));
		goal1.transform.Find("Arrow").gameObject.SetActive(true);
		while (!next) {	yield return new WaitForEndOfFrame(); }

		goal1.transform.Find("Arrow").gameObject.SetActive(false);
		StartCoroutine(DisplayLine("Now you tap one of these math things on the right. We'll do plus for now.", true, nixonTalk));
		plusOperand.transform.Find("PointerUI").gameObject.SetActive(true);
		StartCoroutine("GlowRepeat", plusOperand);
		while (!next) {	yield return new WaitForEndOfFrame(); }

		StartCoroutine(DisplayLine("They're called operands, but whatever.", true, nixonTalk));
		while (!next) {	yield return new WaitForEndOfFrame(); }

		StopCoroutine("GlowRepeat");
		calcText.text = "4 + ";
		plusOperand.transform.Find("PointerUI").gameObject.SetActive(false);
		plusOperand.color = Color.yellow;
		StartCoroutine(DisplayLine("Here comes a one!", false, nixonTalk));
		GameObject one = Instantiate(numberPrefab);
		one.GetComponent<Number>().SetValue(1);
		one.transform.position = new Vector2(10, realDeepThroat.transform.position.y);
		one.GetComponent<Rigidbody2D>().AddForce(new Vector2(-250, 0));
		while (!next) {	yield return new WaitForEndOfFrame(); }

		StartCoroutine(DisplayLine("4 + 1 = 5, ya dummy! We did it!", true, nixonTalk));
		StartCoroutine("GoalMet");
		while (!next) {	yield return new WaitForEndOfFrame(); }

		StartCoroutine(DisplayLine("You gotta choose an operand each time, so choose carefully.", true, nixonTalk));
		while (!next) {	yield return new WaitForEndOfFrame(); }

		plusOperand.color = Color.white;
		StartCoroutine(DisplayLine("Reaching goals adds to your time. If your run out of time, it's game over for you and the universe.", true, nixonTalk));
		StopCoroutine("GlowRepeat");
		StopCoroutine("GlowRepeat");
		timerText.transform.Find("Arrow").gameObject.SetActive(true);
		while (!next) {	yield return new WaitForEndOfFrame(); }

		StartCoroutine(DisplayLine("If you don't do math often enough, your math juice will deplete.", true, nixonTalk));
		timerText.transform.Find("Arrow").gameObject.SetActive(false);
		mathJuice.transform.Find("Arrow").gameObject.SetActive(true);
		StartCoroutine("DepleteJuice");
		while (!next) {	yield return new WaitForEndOfFrame(); }

		mathJuice.transform.Find("Arrow").gameObject.SetActive(false);
		StopCoroutine("DepleteJuice");
		StartCoroutine(DisplayLine("I don't know, you'll probably figure the rest out. Just play the game already!", true, nixonTalk));
		while (!next) {	yield return new WaitForEndOfFrame(); }

		Application.LoadLevel("Space");

		yield return null;
	}

	public IEnumerator DepleteJuice() {
		while (true) {
			mathJuice.value -= .01f;
			if (mathJuice.value <= 0) {
				realDeepThroat.Shake();
				yield return new WaitForSeconds(.5f);
				mathJuice.value = 1;
			}
			yield return new WaitForEndOfFrame();
		}
	}

	public void Bounce(GameObject bounce) {
		if (bounce.Equals(bouncePointer1)) {
			bouncePointer2.SetActive(true);
			bouncePointer1.SetActive(false);
			realDeepThroat.destination = bouncePointer2.transform.position;
		} else {
			bouncePointer1.SetActive(true);
			bouncePointer2.SetActive(false);
			realDeepThroat.destination = bouncePointer1.transform.position;
		}
	}

	
	IEnumerator GoalMet() {
		// set second var
		calcText.text = "4 + 1";
		yield return new WaitForSeconds(.3f);
		
		// set equal
		calcText.text = "4 + 1 =";
		yield return new WaitForSeconds(.3f);
		
		calcText.text = "4 + 1 = 5";
		yield return new WaitForSeconds(.3f);
		
		StartCoroutine("GlowRepeat", calcText);
		StartCoroutine("GlowRepeat", goal1);
	}

	public IEnumerator GlowRepeat(Text text) {
		while (true) {
			yield return StartCoroutine(Util.Glow(text));
		}
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
			} else if (pointing) {
				StopPointer();
				next = true;
				Line++;
			}
		}
	}
	
	public void StopPointer() {
		pointing = false;
		StopCoroutine("Pointer");
		pointer.gameObject.SetActive(false);
	}
	
	public IEnumerator DisplayLine(string text, bool pointer, AudioClip talkSound) {
		StopPointer();
		writingDelay = writingDelayDefault;
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
					audioSource.pitch = Random.Range(.8f,1.8f);
					audioSource.PlayOneShot(talkSound);
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
		if (pointer) {
			StartCoroutine("Pointer");
		} else {
			StopPointer();
		}
		writing = false;
	}
	
	public IEnumerator Pointer() {
		pointing = true;
		while (true) {
			pointer.gameObject.SetActive(!pointer.gameObject.activeSelf);
			yield return new WaitForSeconds(.7f);
		}
	}
}
