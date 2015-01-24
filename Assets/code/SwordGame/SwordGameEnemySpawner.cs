using UnityEngine;
using System.Collections;

public class SwordGameEnemySpawner : MonoBehaviour {

	public SwordGame swordGame;
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
			SpawnEnemy();
		}
	}

	SwordGameEnemy SpawnEnemy()
	{
		Transform instance = Instantiate (enemyPrefab, new Vector3(0f,0f, 10e10f), Quaternion.identity) as Transform;
		instance.parent = transform;
		int size = spawnPositions.Length;
		if (size > 0)
		{
			instance.localPosition = spawnPositions[Random.Range(0, size)].localPosition;
		}
		else
		{
			Debug.LogError("Tried to spawn enemies, but no spawn points are set");
		}

		SwordGameEnemy enemy = instance.GetComponent<SwordGameEnemy>();
		enemy.swordGame = swordGame;

		return enemy;
	}

	public void SpawnBadEnemy()
	{
		SwordGameEnemy enemy = SpawnEnemy();
		enemy.graphics.renderer.material.color = new Color(1f, 0f, 0f);
		enemy.velocity += 1.0f;
	}
	
}
