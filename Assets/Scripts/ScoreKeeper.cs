using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

	public int score = 0;

	private Text scoreText;

	void Start() {
		scoreText = GetComponent<Text>();
		Reset();
		UpdateText();
	}

	void UpdateText() {
		scoreText.text = "Score: " + score;
	}

	public void UpdateScore(int num) {
		score += num;
		UpdateText();
	}

	public void Reset() {
		score = 0;
		UpdateText();
	}
}
