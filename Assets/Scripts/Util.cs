using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Util : MonoBehaviour {
	static string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

	public static bool IsEmpty(int x) {
		return x == 0;
	}

	public static bool IsEmpty(string x) {
		return x == null || x == "";
	}

	public static string RandomString(string input) {
		string ret = "";
		int length = input.Length;
		for (int i = 0; i <length; i++) {
			if (UnityEngine.Random.Range(0, 3) == 1) {
				ret += input.ToCharArray()[i];
			} else {
				ret += chars.Substring(UnityEngine.Random.Range(0, chars.Length - 1), 1);
			}
		}
		return ret;
	}

	public static bool between(int x, int low, int high) {
		return x >= low && x <= high;
	}

	public static IEnumerator Glow(Text text) {
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
}
