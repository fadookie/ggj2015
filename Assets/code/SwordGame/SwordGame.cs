using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SwordGame : MinigameBase {

	public SwordGamePlayer player;
	public SwordGameEnemySpawner spawner;
	public Text scoreLabel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		scoreLabel.text = string.Format("Score: {0}", Score);
	}

	public override void onGoodEvent(int magnitude) {
		print ("good");
	}
	
	public override void onBadEvent(int magnitude) {
		spawner.SpawnBadEnemy();
	}
}
