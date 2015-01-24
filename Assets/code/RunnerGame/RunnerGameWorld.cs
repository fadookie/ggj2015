using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RunnerGameWorld : MonoBehaviour {

	public float awesomeness = 10f;
	public Transform startBuilding;
	public Transform[] largeBuildings;
	public Transform[] mediumBuildings;
	public Transform[] smallBuildings;
	public Transform[] tinyBuildings;

	public float largeBuildingsThreshold = 9f;
	public float mediumBuildingsThreshold = 6f;
	public float smallBuildingsThreshold = 4f;
	public float tinyBuildingsThreshold = 1f;

	public float initialVelocity = 1.0f;
	public float velocityIncrease = 0.1f;
	public float activeArea = 10.0f;

	float velocity;

	List<Transform> activeBuildings;
	List<Transform> toBeDeleted;
	List<Transform> toBeAdded;
	// Use this for initialization
	void Start () {
		activeBuildings = new List<Transform>();
		toBeDeleted = new List<Transform>();
		toBeAdded = new List<Transform>();

		ResetGame();
	}
	
	// Update is called once per frame
	void Update () 
	{
		velocity += velocityIncrease * Time.deltaTime;
		UpdateBuildings();
	}

	void UpdateBuildings()
	{
		foreach(var building in activeBuildings)
		{
			Vector3 pos = building.localPosition;
			pos.x -= velocity * Time.deltaTime;
			building.localPosition = pos;
			if (pos.x <= -activeArea)
			{
				toBeDeleted.Add(building);
			}
		}
		int index = -1;
		index = activeBuildings.Count-1;
		float lastPos = 0;
		float scale = 1;
		if(index != -1)
		{
			lastPos = activeBuildings[index].localPosition.x;
			scale = activeBuildings[index].localScale.x;
		}
		if (lastPos <= activeArea)
		{
			SpawnNewBuilding(new Vector3(lastPos + scale + Random.Range(6.0f, 8.5f), Random.Range(-2.2f, 2.2f), 0f));
		}

		{
			Vector3 pos = startBuilding.localPosition;
			pos.x -= velocity * Time.deltaTime;
			startBuilding.localPosition = pos;
		}

		foreach(var building in toBeAdded)
		{
			activeBuildings.Add(building);
		}
		toBeAdded.Clear();

		foreach(var building in toBeDeleted)
		{
			activeBuildings.Remove(building);
			Destroy (building.gameObject);
		}
		toBeDeleted.Clear();
	}

	void SpawnNewBuilding(Vector3 spawnPos)
	{
		Transform transform = null;
		if (awesomeness > largeBuildingsThreshold) 
		{
			transform = SpawnRandomBuilding(largeBuildings);
		}
		else if (awesomeness > mediumBuildingsThreshold)
		{
			// spawn medium or large
			if(awesomeness > Random.Range(mediumBuildingsThreshold, largeBuildingsThreshold))
			{
				transform = SpawnRandomBuilding(largeBuildings);
			}
			else
			{
				transform = SpawnRandomBuilding(mediumBuildings);
			}
		}
		else if (awesomeness > smallBuildingsThreshold)
		{
			// spawn medium or small
			if(awesomeness > Random.Range(smallBuildingsThreshold, mediumBuildingsThreshold))
			{
				transform = SpawnRandomBuilding(mediumBuildings);
			}
			else
			{
				transform = SpawnRandomBuilding(smallBuildings);
			}
		}
		else if (awesomeness > tinyBuildingsThreshold)
		{
			// spawn small or tiny
			if(awesomeness > Random.Range(tinyBuildingsThreshold, smallBuildingsThreshold))
			{
				transform = SpawnRandomBuilding(mediumBuildings);
			}
			else
			{
				transform = SpawnRandomBuilding(smallBuildings);
			}
		}
		else
		{
			transform = SpawnRandomBuilding(tinyBuildings);
		}

		transform.parent = this.transform;
		transform.localPosition = spawnPos;
		toBeAdded.Add(transform);
	}

	Transform SpawnRandomBuilding(Transform[] sources)
	{
		int index = Random.Range(0, sources.Length);
		return Instantiate(sources[index]) as Transform;
	}

	public void ResetGame()
	{
		velocity = initialVelocity;
		// remove all active buildings
		foreach(var building in activeBuildings)
		{
			Destroy (building.gameObject);
		}
		activeBuildings.Clear();

		Vector3 startPos = startBuilding.transform.localPosition;
		startPos.x = 0f;
		startBuilding.transform.localPosition = startPos;


		SpawnNewBuilding(new Vector3(24f, -1f, 0f));
		SpawnNewBuilding(new Vector3(48f, -1f, 0f));
		
		foreach(var building in toBeAdded)
		{
			activeBuildings.Add(building);
		}
		toBeAdded.Clear();
	}

	public void Pause()
	{
		velocity = 0.0f;
	}
}
