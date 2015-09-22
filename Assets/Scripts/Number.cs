using UnityEngine;
using System.Collections;

public class Number : Flyer {

	public int number = 0;

	public static int maxNumber = 20;
	public static int midNumber = 10;
	public static int minNumber = 1;

	public new void Start() {
		base.Start();
		int midOrMax = UnityEngine.Random.Range(0, 5);
		int value = UnityEngine.Random.Range(minNumber,(midOrMax == 1 ? maxNumber: midNumber));
		GetComponent<TextMesh>().text = value.ToString();
		GetComponent<Number>().number = value;
	}
}
