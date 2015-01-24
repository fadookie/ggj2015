using UnityEngine;
using System.Collections;

public class CatcherGame : MinigameBase {

	// Use this for initialization
	void Start () {
		Services.instance.Set<CatcherGame>(this);
	}
	
	// Update is called once per frame
//	void Update () {
//	}

	public override void onGoodEvent(int magnitude) {
		print(gameObject.name + " onGoodEvent mag:" + magnitude);
	}

	public override void onBadEvent(int magnitude) {
		print(gameObject.name + " onBadEvent mag:" + magnitude);
	}
}
