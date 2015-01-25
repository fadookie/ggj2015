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
	
	}

	public void SpawnPowerups(Combo combo)
	{
		float[] heights = { 2.5f, 4.9f };
		int idx = Random.Range(0, 2);
		// spawn correct
		SpawnPowerup(combo.shape, new Vector3(world.activeArea + Random.Range(-3.0f, 3.0f), heights[idx], 0f));

		// pick a random other combo shape.
		int length = System.Enum.GetNames(typeof(Combo.Color)).Length;
		int c = ((int)combo.shape + Random.Range(1, length)) % length;
		SpawnPowerup((Combo.Shape)c, new Vector3(world.activeArea + Random.Range(-3.0f, 3.0f), heights[(idx+1)%heights.Length], 0f));
	}

	void SpawnPowerup(Combo.Shape shape, Vector3 pos)
	{
		Debug.Log("Spawning powerup with shape: " + shape); 
		RunnerGamePowerup powerup = null;
		switch(shape)
		{
		case Combo.Shape.Shape0:
			powerup = Instantiate(powerupRed) as RunnerGamePowerup;
			powerup.shape = Combo.Shape.Shape0;
			break;
		case Combo.Shape.Shape1:
			powerup = Instantiate(powerupBlue) as RunnerGamePowerup;
			powerup.shape = Combo.Shape.Shape1;
			break;
		case Combo.Shape.Shape2:
			powerup = Instantiate(powerupGreen) as RunnerGamePowerup;
			powerup.shape = Combo.Shape.Shape2;
			break;
		}
		powerup.transform.parent = transform;
		Vector3 spawnPos = pos;
		powerup.transform.localPosition = spawnPos;
		powerup.world = world;
	}
}
