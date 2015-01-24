using UnityEngine;
using System.Collections;

public class CatcherPlayer : MonoBehaviour {

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
		InputManager input = Services.instance.Get<InputManager>();
		if (input != null) {
			Vector3 newPos = transform.localPosition;
			if (input.GetButton(game.PlayerIdx, InputManager.Button.Left)) {
				newPos.x -= moveVel * Time.deltaTime; 
			} else if (input.GetButton(game.PlayerIdx, InputManager.Button.Right)) {
				newPos.x += moveVel * Time.deltaTime; 
			}
			newPos.x = Mathf.Clamp(newPos.x, xMin.transform.localPosition.x, xMax.transform.localPosition.x);
			transform.localPosition = newPos; 
		}
	}

	void OnTriggerEnter(Collider other) {
		FallingTarget target = other.GetComponent<FallingTarget>();
		switch (target.targetType) {
		case FallingTarget.TargetType.ShouldCatch:
			game.Score++;
			break;
		case FallingTarget.TargetType.ShouldNotCatch:
			game.Score--;
			break;
		case FallingTarget.TargetType.Bomb:
			game.Score++;
			break;
		}
		other.gameObject.SetActive(false);
		Destroy(other.gameObject);
	}
}
