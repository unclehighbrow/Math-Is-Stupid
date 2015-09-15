using UnityEngine;
using System.Collections;

public class DeepThroat : MonoBehaviour {
	public LevelManager levelManager;
	public Vector2 destination;
	Animator animator;
	public bool dead = false;
	float speed = .15f;

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
			} else {
				GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (!dead) {
			if (col.gameObject.CompareTag("Number")) {
				levelManager.HandleNumber(col.gameObject.GetComponent<Number>().number);
			} else if (col.gameObject.CompareTag("Rock")) {
				levelManager.HandleRock();
			} else if (col.gameObject.CompareTag("Powerup")) {
				levelManager.HandlePowerup(col.gameObject.GetComponent<Powerup>().powerup);
			}
			Destroy(col.gameObject);
		}
	}
}
