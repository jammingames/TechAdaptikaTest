using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject introUI;
    public GameObject gameUI;
	public GameObject summaryUI;

	public Text scoreTextObject;
	public Text summaryScoreTextObject;
	public Text highScoreTextObject;

	private void OnEnable()
	{
		GameManager.OnStateChange += HandleStateChange;
		GameManager.OnAddScore += HandleAddScore;
		GameManager.OnResetScore += HandleResetScore;
	}

	private void OnDisable()
	{
		GameManager.OnStateChange -= HandleStateChange;
		GameManager.OnAddScore -= HandleAddScore;
		GameManager.OnResetScore -= HandleResetScore;
	}

	void HandleAddScore(int score)
	{
		scoreTextObject.text = GameManager.playerScore.ToString();
		summaryScoreTextObject.text = GameManager.playerScore.ToString();
		highScoreTextObject.text = GameManager.highestPlayerScore.ToString();
	}

	void HandleResetScore(int score)
	{
		scoreTextObject.text = GameManager.playerScore.ToString();
		summaryScoreTextObject.text = GameManager.playerScore.ToString();
		highScoreTextObject.text = GameManager.highestPlayerScore.ToString();
	}


	void HandleStateChange(GameState state)
	{
		switch (state)
		{
			case GameState.Intro: 
				introUI.SetActive(true);
				gameUI.SetActive(false);
				summaryUI.SetActive(false);
				break;
			case GameState.Game:
				introUI.SetActive(false);
				gameUI.SetActive(true);
				summaryUI.SetActive(false);
				break;
			case GameState.ScoreScreen:
				introUI.SetActive(false);
				gameUI.SetActive(false);
				summaryUI.SetActive(true);
				break;
		}
	}

}
