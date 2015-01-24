using UnityEngine;
using System.Collections;

public class RunnerGame : MinigameBase {

	public RunnerGamePlayer player;
	public RunnerGameWorld world;

	bool resetting = false;
	// Use this for initialization
	void Start () {
		Services.instance.Set<RunnerGame>(this);
	}
	
	// Update is called once per frame
	void Update () {

	}

	public override void onGoodEvent(int magnitude) {
		world.awesomeness = Mathf.Clamp((float)world.awesomeness+1.0f, 0f, 10f);
	}
	
	public override void onBadEvent(int magnitude) {
		world.awesomeness = Mathf.Clamp((float)world.awesomeness-1.0f, 0f, 10f);
	}

	public override void onPlayerIdxChange(int oldIdx, int newIdx) {
		print("RunnerGame::onPlayerIdxChange oldIdx:" + oldIdx + " newIdx: " + newIdx);
	}

	public void ResetGame()
	{
		if (!resetting)
		{
			Score -= 1;
			StartCoroutine(ResetGameRoutine());
		}
	}

	IEnumerator ResetGameRoutine()
	{
		resetting = true;
		world.Pause();
		yield return new WaitForSeconds(1.0f);
		world.ResetGame();
		player.ResetGame();
		resetting = false;
	}
}
