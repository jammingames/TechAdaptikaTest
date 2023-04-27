using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fish : MonoBehaviour, ICatchable
{
	
	public enum FishState
	{
		NullState = 1,
		Swimming = 2,
		Biting = 3
	}

	[SerializeField] FishData myData;
	[SerializeField] private SpriteRenderer spriteRenderer;
	float speed;
	float lifetime;
	float currentLifeDuration = 0f;
	Color myColor;
	public int points;
	FishState currentFishState = FishState.NullState;

	Transform target = null;
	int direction = 1;
	private void Awake()
	{
		Init(myData);
		GameManager.OnStateChange += HandleGameStateChange;
	}

	void Start()
	{
		SetFishState(FishState.Swimming);
	}

	private void OnDestroy()
	{
		GameManager.OnStateChange -= HandleGameStateChange;
	}

	private void Update()
	{
		if (currentFishState == FishState.NullState) return;
		HandleStateUpdate();
	}

	public void Init(FishData data)
	{
		myData = data;
		myColor = myData.myColor;
		speed = myData.speed;
		points = myData.points;
		lifetime = myData.lifetime;
		spriteRenderer.color = myColor;
		SetFishState(FishState.Swimming);
		direction = transform.position.x < 0 ? 1 : -1;
	}
	public void GetCaught()
	{
		GameManager.instance.CatchFish(this);
		Die();
	}

	public void Die()
	{
		GameObject.Destroy(this.gameObject);
	}


	public void SetFishState(FishState newState)
	{
		if (currentFishState != newState)
		{
			HandleStateChange(newState);
			currentFishState = newState;
		}
		else
		{
			Debug.Log("ALREADY IN THIS STATE  " + currentFishState);
		}
	}

	public void HandleStateChange(FishState newState)
	{
		switch (currentFishState)
		{
			case FishState.Swimming:
				currentLifeDuration = 0;
				break;
		}

	}

	void HandleGameStateChange(GameState newState)
	{
		switch (newState)
		{
			case GameState.Intro:
				break;
			case GameState.Game:
				break;
			case GameState.ScoreScreen:
				Die();
				break;
		}
	}

	public void HandleStateUpdate()
	{
		switch (currentFishState) 
		{
			case FishState.Swimming:
				DoSwimming();
				break;
		}
	}


	void DoSwimming()
	{
		Vector3 pos = transform.position;
		pos.x += speed * Time.deltaTime * direction;
		transform.position = pos;
		currentLifeDuration += Time.deltaTime;
		if (currentLifeDuration >= lifetime)
			Die();
	}
}
