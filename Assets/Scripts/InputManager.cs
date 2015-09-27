using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	public DeepThroat deepThroat;
	public LevelManager levelManager;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale < 1 || !levelManager.gameStarted) {
			return;
		}
		Vector2 destination = Vector2.zero;
		Vector2 worldPoint = Vector2.zero;
		
		if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow)) {
			destination = new Vector2(0, levelManager.maxY);
		}
		if (Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow)) {
			destination = new Vector2(0, levelManager.minY);
		}
		if (Input.GetKeyUp("w") || Input.GetKeyUp("s") || Input.GetKeyUp(KeyCode.UpArrow)|| Input.GetKeyUp(KeyCode.DownArrow)) {
			deepThroat.destination = Vector2.zero;
			return;
		}
		
		
		#if UNITY_EDITOR
		// mouse
		if (Input.GetMouseButtonDown(0)) {
			worldPoint = Input.mousePosition;
		}
		#endif
		// touch
		Touch[] touches = Input.touches;
		for (int i = 0 ; i < Input.touchCount; i++) {
			Touch touch = touches[i];
			if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved) {
				worldPoint = touch.position;
			} 
		}
		
		if (worldPoint != Vector2.zero && worldPoint.x < Screen.width/2) {
			destination = Camera.main.ScreenToWorldPoint(worldPoint);
		}
		
		if (destination != Vector2.zero) {
			deepThroat.destination = destination;
		}
	}
}
