using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManager : MonoBehaviour
{
    public float spawnRate = 1.0f;
    public Fish[] fishToSpawn;
    float currentSpawnTimer = 0;
    bool doUpdate = false;
    [SerializeField] List<Transform> spawnPositions = new List<Transform>();

	private void Awake()
	{
        GameManager.OnStateChange += HandleStateChange;
        
	}

	private void OnDestroy()
	{
		GameManager.OnStateChange -= HandleStateChange;
	}

	private void HandleStateChange(GameState nextState)
	{
        if (nextState == GameState.Game) doUpdate = true; else doUpdate = false;

	}

	void Update()
    {
        if (!doUpdate) { return; }
        currentSpawnTimer += Time.deltaTime;
        if (currentSpawnTimer >= spawnRate) { DoSpawn(); }
    }

    void DoSpawn()
    {
        currentSpawnTimer = 0;
        Transform spawnpoint = spawnPositions[GetNextSpawnPoint()];
        Fish nextFish = GameObject.Instantiate(fishToSpawn[Random.RandomRange(0, fishToSpawn.Length - 1)], spawnpoint.position, Quaternion.identity, spawnpoint);
        nextFish.SetFishState(Fish.FishState.Swimming);
    }

	int GetNextSpawnPoint()
	{
        return Random.Range(0, spawnPositions.Count - 1);
	}
}
