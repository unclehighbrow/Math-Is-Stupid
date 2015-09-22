using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Powerup : Flyer {

	public static string invinciblity = "Invincibility";
	public static string slowDownTimer = "Slow Down Timer";
	public static string unlimitedTimeJuice =  "Unlimited Time Juice";
	public static string roundBy2 = "Round by 2";
	public static string moreGoals =  "More Goals";

	// unimplemented
	public static string longerPowerups = "Longer powerups";
	public static string perfectNumbers = "Perfect numbers";

	public static List<string> powerups = new List<string>() {
		invinciblity, slowDownTimer, unlimitedTimeJuice, roundBy2, moreGoals
	};
	public string powerup;

	public new void Start () {
		base.Start();
		powerup = powerups[Random.Range(0, powerups.Count)];
	}
}
