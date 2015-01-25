using UnityEngine;
using System.Collections;

public class RunnerGame : MinigameBase {

	public RunnerGamePlayer player;
	public RunnerGameWorld world;
	public RunnerGamePowerupSpawner powerupSpawner;

	Combo? activeCombo;

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

	public override void onCombo(Combo combo) {
		print("RunnerGame::onComboEvent: " + combo);
		activeCombo = combo;
		powerupSpawner.SpawnPowerups(combo);
	}

	public override void onPlayerIdxChange(int oldIdx, int newIdx) {
//		print("RunnerGame::onPlayerIdxChange oldIdx:" + oldIdx + " newIdx: " + newIdx);
	}

	public void ResetGame()
	{
		activeCombo = null;
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

	public void onShapeCaught(Combo.Shape caughtShape) {
		if (activeCombo.HasValue) {
			if (activeCombo.Value.shape.Equals(caughtShape)) {
				postComboEventPassed(activeCombo.Value);
			} else {
				//Combo failed!
				postComboEventFailed(activeCombo.Value);
				activeCombo = null;
			}
		}
	}

}
