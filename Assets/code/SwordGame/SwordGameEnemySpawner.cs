using UnityEngine;
using System.Collections;

public class SwordGameEnemySpawner : MonoBehaviour {

	public Transform enemyPrefab;
	public Transform[] spawnPositions;
	public float spawnInterval = 2f;

	float timeForNextSpawn;

	// Use this for initialization
	void Start () {
		timeForNextSpawn = spawnInterval;
	}
	
	// Update is called once per frame
	void Update () {
		timeForNextSpawn -= Time.deltaTime;
		if (timeForNextSpawn <= 0)
		{
			timeForNextSpawn += spawnInterval;
			SpawnEnemies();
		}
	}

	void SpawnEnemies()
	{
		SwordGameEnemy instance = Instantiate (enemyPrefab) as SwordGameEnemy;
		instance.transform.parent = transform;
		int size = spawnPositions.Length;
		if (size > 0)
		{
			instance.transform.localPosition = spawnPositions[Random.Range(0, size-1)].localPosition;
		}
		else
		{
			Debug.LogError("Tried to spawn enemies, but no spawn points are set");
		}
	}
}
