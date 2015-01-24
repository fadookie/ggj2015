using UnityEngine;
using System.Collections;

public class CatcherPlayer : MonoBehaviour {

	public string leftButton = "Fire1";
	public string rightButton = "Fire3";

	public float moveVel = 1f;
	public GameObject xMin;
	public GameObject xMax;

	CatcherGame game;

	// Use this for initialization
	void Start () {
		game = Services.instance.Get<CatcherGame>();
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

	void OnTriggerEnter(Collider other) {
		game.Score++;
		other.gameObject.SetActive(false);
		Destroy(other.gameObject);
	}
}
