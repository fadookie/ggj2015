using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class TargetSpawner : MonoBehaviour {
	public GameObject spawnedPrefab;
	BoxCollider spawnArea;
	Vector2 maxSpawnPos;
	
	float lastSpawnTimeS = -1;
	public float spawnDelayS = 5;
	public float shouldNotCatchSpawnChancePct = 0.10f;

	CatcherGame game;
	
	// Use this for initialization
	void Start () {
		game = Services.instance.Get<CatcherGame>();
		spawnArea = GetComponent<BoxCollider>();
		spawnArea.enabled = false; //We don't need this to test for any collisions, just to show visual bounds info in the editor.
		maxSpawnPos = new Vector2(spawnArea.size.x / 2, spawnArea.size.y / 2);
		Services.instance.Set<TargetSpawner>(this);
	}
	
	// Update is called once per frame
	void Update () {
		if (lastSpawnTimeS < 0) {
			lastSpawnTimeS = Time.time;
			FallingTarget.TargetType spawnType;
			if(Random.value <= shouldNotCatchSpawnChancePct) {
				spawnType = FallingTarget.TargetType.ShouldNotCatch;
			} else {
				spawnType = FallingTarget.TargetType.ShouldCatch;
			}
			spawnTargetOfType(spawnType);
		} else if (lastSpawnTimeS >= 0 && Time.time - lastSpawnTimeS > spawnDelayS / game.TargetTimeScale) {
			lastSpawnTimeS = -1;
		}
	}

	public void spawnTargetOfType(FallingTarget.TargetType type) {
		Vector3 pos = new Vector3(Random.Range(-maxSpawnPos.x, maxSpawnPos.x), Random.Range(-maxSpawnPos.y, maxSpawnPos.y), 0);
		spawnTargetOfType(type, pos);
	}

	public void spawnTargetOfType(FallingTarget.TargetType type, Vector3 pos) {
		GameObject spawned = Instantiate(spawnedPrefab, Vector3.zero, Quaternion.Euler(new Vector3(90, 90, 0))) as GameObject;
		spawned.transform.parent = transform;
		spawned.transform.localPosition = pos;
		FallingTarget target = spawned.GetComponent<FallingTarget>();
		target.targetType = type;
	}

	Vector3 posForSpecialTarget(int idx) {
		float partitionWidth = spawnArea.size.x / System.Enum.GetNames(typeof(Combo.Color)).Length;
		float posX = (partitionWidth * (idx)) - partitionWidth;
		return new Vector3(posX, -maxSpawnPos.y, 0);
	}

	public void spawnSpecialTargets() {
		for(int i = 0; i < System.Enum.GetNames(typeof(Combo.Color)).Length; ++i) {
			Vector3 pos = posForSpecialTarget(i);
			FallingTarget.TargetType targetType = default(FallingTarget.TargetType);
			switch(i) {
			case 0:
				targetType = FallingTarget.TargetType.SpecialColor0;
				break;
			case 1:
				targetType = FallingTarget.TargetType.SpecialColor1;
				break;
			case 2:
				targetType = FallingTarget.TargetType.SpecialColor2;
				break;
			}
			spawnTargetOfType(targetType, pos);
		}
	}
}
