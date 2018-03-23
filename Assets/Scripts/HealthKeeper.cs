using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthKeeper : MonoBehaviour {

	public static float health = 250;

	private Text healthText;

	void Start() {
		healthText = GetComponent<Text>();
		Reset();
		UpdateText();
	}

	void UpdateText() {
		healthText.text = "Health: " + health;
	}

	public void UpdateHealth(float num) {
		health = num;
		UpdateText();
	}

	public static void Reset() {
		health = 250;
	}
}
