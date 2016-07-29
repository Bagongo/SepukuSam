using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ScoreMan : MonoBehaviour {

	public Text scoreDisplayer;
	public int scoreCount;

	private string fixedTxt;

	void Start () { 

		fixedTxt = scoreDisplayer.text;
		scoreCount = 0;

		UpdateScore(scoreCount);
	
	}
	
	void Update () {
	
	}

	public void UpdateScore(int score)
	{
		scoreCount += score;
		scoreDisplayer.text = fixedTxt + " " + scoreCount;
	}
}
