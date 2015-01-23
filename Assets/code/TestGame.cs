using UnityEngine;
using System.Collections;

public class TestGame : MinigameBase {

	public string button1 = "Fire1";
	public string button2 = "Fire2";

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
		print(gameObject.name + " onGoodEvent mag:" + magnitude);
	}

	public override void onBadEvent(int magnitude) {
		print(gameObject.name + " onBadEvent mag:" + magnitude);
	}
}
