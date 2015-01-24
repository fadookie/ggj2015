using UnityEngine;
using System.Collections;

public class RunnerGame : MinigameBase {

	// Use this for initialization
	void Start () {
	
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
}
