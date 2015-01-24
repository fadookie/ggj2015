using UnityEngine;
using System.Collections;

public class RunnerGame : MinigameBase {

	public RunnerGamePlayer player;
	public RunnerGameWorld world;

	// Use this for initialization
	void Start () {
		Services.instance.Set<RunnerGame>(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void onGoodEvent(int magnitude) {
		print ("RunnerGame::Good");
	}
	
	public override void onBadEvent(int magnitude) {
		print ("RunnerGame::Bad");
	}

	public override void onPlayerIdxChange(int oldIdx, int newIdx) {
		print("RunnerGame::onPlayerIdxChange oldIdx:" + oldIdx + " newIdx: " + newIdx);
	}

	public void ResetGame()
	{
		world.ResetGame();
		player.ResetGame();
	}
}
