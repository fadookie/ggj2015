using UnityEngine;
using System.Collections;

public class TestGame : MinigameBase {

	public string button1 = "Fire3";
	public string button2 = "Jump";

	public bool loggingEnabled = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown(button1)) {
			++Score;
		} else if (Input.GetButtonDown(button2)) {
			--Score;
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
