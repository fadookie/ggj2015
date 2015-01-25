using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RunnerGamePowerupSpawner : MonoBehaviour {

	public RunnerGamePowerup powerup;

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
		float[] heights = { 3.5f, 6.2f };
		int idx = Random.Range(0, 2);
		// spawn correct
		Combo correctCombo = combo;
		SpawnPowerup(correctCombo, new Vector3(world.activeArea, heights[idx], 0f));

		// pick a random other combo shape.
		int length = System.Enum.GetNames(typeof(Combo.Color)).Length;
		Combo fakeCombo = combo;
		fakeCombo.shape = (Combo.Shape)(((int)combo.shape + Random.Range(1, length)) % length);
		SpawnPowerup(fakeCombo, new Vector3(world.activeArea, heights[(idx+1)%heights.Length], 0f));
	}

	void SpawnPowerup(Combo combo, Vector3 pos)
	{
		Debug.Log("Spawning powerup with shape: " + combo.shape); 
		RunnerGamePowerup newPowerup = Instantiate(powerup) as RunnerGamePowerup;;
		
		switch(combo.shape)
		{
		case Combo.Shape.Shape0:
			newPowerup.shape = Combo.Shape.Shape0;
			break;
		case Combo.Shape.Shape1:
			newPowerup.shape = Combo.Shape.Shape1;
			break;
		case Combo.Shape.Shape2:
			newPowerup.shape = Combo.Shape.Shape2;
			break;
		}
		newPowerup.transform.parent = transform;
		Vector3 spawnPos = pos;
		newPowerup.transform.localPosition = spawnPos;
		newPowerup.world = world;

		newPowerup.GetComponent<ComboIndicator>().SetCombo(combo);
	}
}
