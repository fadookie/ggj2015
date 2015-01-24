using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RunnerGamePowerupSpawner : MonoBehaviour {

	public RunnerGamePowerup powerupRed;
	public RunnerGamePowerup powerupBlue;
	public RunnerGamePowerup powerupGreen;

	public RunnerGameWorld world;

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			SpawnPowerups();
		}
	}

	void SpawnPowerups()
	{
		RunnerGamePowerup powerup = Instantiate(powerupRed) as RunnerGamePowerup;
		powerup.transform.parent = transform;
		Vector3 spawnPos = new Vector3(world.activeArea, 2f, 0f);
		powerup.transform.localPosition = spawnPos;
		powerup.world = world;
	}
}
