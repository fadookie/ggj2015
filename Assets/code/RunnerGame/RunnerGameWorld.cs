using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RunnerGameWorld : MonoBehaviour {

	public Transform startBuilding;
	public Transform[] largeBuildings;
	public float velocity = 1.0f;
	public float velocityIncrease = 0.1f;
	public float activeArea = 10.0f;


	List<Transform> activeBuildings;
	List<Transform> toBeDeleted;
	List<Transform> toBeAdded;
	// Use this for initialization
	void Start () {
		activeBuildings = new List<Transform>();
		toBeDeleted = new List<Transform>();
		toBeAdded = new List<Transform>();

		SpawnNewBuilding(new Vector3(10f, -1f, 0f));
		SpawnNewBuilding(new Vector3(15f, -1f, 0f));
		SpawnNewBuilding(new Vector3(20f, -1f, 0f));
		SpawnNewBuilding(new Vector3(25f, -1f, 0f));

		foreach(var building in toBeAdded)
		{
			activeBuildings.Add(building);
		}
		toBeAdded.Clear();
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
		if(index != -1)
		{
			lastPos = activeBuildings[index].localPosition.x;
		}
		if (lastPos <= activeArea)
		{
			SpawnNewBuilding(new Vector3(lastPos + Random.Range(5.0f, 6.5f), Random.Range(-1f, 1f), 0f));
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
		int index = Random.Range(0, largeBuildings.Length);
		Transform transform = Instantiate(largeBuildings[index]) as Transform;
		transform.parent = this.transform;
		transform.localPosition = spawnPos;
		toBeAdded.Add(transform);
	}
}
