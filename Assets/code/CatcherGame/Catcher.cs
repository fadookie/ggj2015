using UnityEngine;
using System.Collections;

public class Catcher : MinigameBase {

	public string leftButton = "Fire1";
	public string rightButton = "Fire3";

	public float moveVel = 1f;
	public GameObject xMin;
	public GameObject xMax;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newPos = transform.localPosition;
		if (Input.GetButton(leftButton)) {
			newPos.x -= moveVel; 
		} else if (Input.GetButton(rightButton)) {
			newPos.x += moveVel; 
		}
		newPos.x = Mathf.Clamp(newPos.x, xMin.transform.localPosition.x, xMax.transform.localPosition.x);
		transform.localPosition = newPos; 
	}

	public override void onGoodEvent(int magnitude) {
		print(gameObject.name + " onGoodEvent mag:" + magnitude);
	}

	public override void onBadEvent(int magnitude) {
		print(gameObject.name + " onBadEvent mag:" + magnitude);
	}
}
