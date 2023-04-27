using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Flags]
public enum GameState
{
	NullState = 1 << 0,
	Intro = 1 << 1,
	Game = 1 << 2,
	ScoreScreen = 1 << 3
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static int playerScore = 0;
	public static int highestPlayerScore = 0;
	float gameDuration = 60f;
	float currentTime = 0;

	public GameState currentState = GameState.NullState;

	public delegate void OnStateChangeHandler(GameState nextState);
	public static event OnStateChangeHandler OnStateChange;

	public delegate void OnGameEvent();
	public static event OnGameEvent OnStateUpdateHandler;

	public delegate void OnScoreEvent(int scoreChange);
	public static event OnScoreEvent OnAddScore, OnResetScore;

	public void SetGameState(GameState gameState)
	{
		if (gameState != currentState)
		{
			OnStateChange?.Invoke(gameState);
			currentState = gameState;
		}
		else
		{
			Debug.Log("ALREADY IN THIS STATE  " + gameState);
		}
	}

	public void StartGame()
	{
		playerScore = 0;
		currentTime = 0;
		SetGameState(GameState.Game);
	}

	public void EndGame()
	{
		currentTime = 0;
		SetGameState(GameState.ScoreScreen);
		if (playerScore > highestPlayerScore) highestPlayerScore = playerScore;
	}

	public void CatchFish(Fish fish)
	{
		AddToScore(fish.points);
	}

	public void AddToScore(int pointsToAdd)
	{
		OnAddScore?.Invoke(pointsToAdd);
		playerScore += pointsToAdd;
	}

	public void ResetScore()
	{
		playerScore = 0;
		OnResetScore?.Invoke(0);
	}

	public void Update()
	{
		switch (currentState)
		{
			case GameState.Intro:
				if (Input.anyKeyDown)
					StartGame();
				break;
			case GameState.Game:
				currentTime += Time.deltaTime;
				if (currentTime > gameDuration)
				{
					EndGame();
					
				}
				break;
			case GameState.ScoreScreen:
				if (Input.anyKeyDown)
					SetGameState(GameState.Game);
				break;
		}
	}



	private void Awake()
	{
        if (instance == null) instance = this;
	}

	private void Start()
	{
		SetGameState(GameState.Intro);
	}



}
