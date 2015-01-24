using UnityEngine;
using System.Collections;

public class TestGame : MinigameBase {

	public bool loggingEnabled = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		InputManager input = Services.instance.Get<InputManager>();
		if (input != null) {
			if(input.GetButtonDown(PlayerIdx, InputManager.Button.Left)) {
				++Score;
			} else if (input.GetButtonDown(PlayerIdx, InputManager.Button.Right)) {
				--Score;
			}
		}
	}

	public override void onGoodEvent(int magnitude) {
		if(loggingEnabled) {
			print(gameObject.name + " onGoodEvent mag:" + magnitude);
		}
	}

	public override void onBadEvent(int magnitude) {
		if(loggingEnabled) {
			print(gameObject.name + " onBadEvent mag:" + magnitude);
		}
	}

	public override void onPlayerIdxChange(int oldIdx, int newIdx) {
		if(loggingEnabled) {
			print(gameObject.name + " onPlayerIdxChange oldIdx:" + oldIdx + " newIdx: " + newIdx);
		}
	}
}
