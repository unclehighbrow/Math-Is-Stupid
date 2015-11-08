using UnityEngine;
using System.Collections;

public class DeepThroat : MonoBehaviour {
	public LevelManager levelManager;
	public TutorialManager tutorialManager;
	public Vector2 destination;
	public Animator animator;
	public bool dead = false;
	public float speed = .15f;

	// Use this for initialization
	void Start () {
		levelManager = GameObject.FindObjectOfType<LevelManager>();
		animator = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Shake() {
		StartCoroutine("ShakeCo");
	}

	private IEnumerator ShakeCo() {
		animator.SetBool("shake", true);
		yield return new WaitForSeconds(.25f);
		animator.SetBool("shake", false);
	}

	void FixedUpdate() {
		if (!dead) {
			if (destination != Vector2.zero && (Vector2)transform.position != destination) { // go to destination
				Vector2 p = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, destination.y), speed);
				GetComponent<Rigidbody2D>().MovePosition(p);
				if (destination.y > transform.position.y) {
					animator.SetInteger("direction", 1);
				} else {
					animator.SetInteger("direction", -1);
				}
			} else {
				GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				animator.SetInteger("direction", 0);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (!dead) {
			if (levelManager != null) {
				if (col.gameObject.CompareTag("Number")) {
					levelManager.HandleNumber(col.gameObject.GetComponent<Number>());
				} else if (col.gameObject.CompareTag("Rock")) {
					levelManager.HandleRock();
					Destroy(col.gameObject);
				} else if (col.gameObject.CompareTag("Powerup")) {
					levelManager.HandlePowerup(col.gameObject.GetComponent<Powerup>().powerup);
					Destroy(col.gameObject);
				}
			} else if (tutorialManager != null) {
				if (col.gameObject.CompareTag("Pointer")) {
					tutorialManager.Bounce(col.gameObject);
				} else {
					tutorialManager.Next();
					Destroy(col.gameObject);
				}
			}
		}
	}
}
