using UnityEngine;
using System.Collections;

public class CatcherPlayer : MonoBehaviour {

	public float moveVel = 1f;
	public Transform xMin;
	public Transform xMax;

	Vector3 defaultScale;

	CatcherGame game;

	Animator anim;

	// Use this for initialization
	void Start () {
		game = Services.instance.Get<CatcherGame>();
		anim = GetComponent<Animator>();
		defaultScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		InputManager input = Services.instance.Get<InputManager>();
		if (input != null) {
			Vector3 newPos = transform.localPosition;
			float targetTimeScale = Mathf.Clamp(game.TargetTimeScale, 1f, 2.5f);
			int walkDir = 0;
			if (input.GetButton(game.PlayerIdx, InputManager.Button.Left)) {
				newPos.x -= moveVel * Time.deltaTime * targetTimeScale; 
				walkDir = -1;
			} else if (input.GetButton(game.PlayerIdx, InputManager.Button.Right)) {
				newPos.x += moveVel * Time.deltaTime * targetTimeScale; 
				walkDir = 1;
			}
			newPos.x = Mathf.Clamp(newPos.x, xMin.localPosition.x, xMax.localPosition.x);
			transform.localPosition = newPos; 

			if (walkDir != 0) {
				Vector3 scale = defaultScale;
				scale.z *= walkDir;
				transform.localScale = scale;
				print (scale);
			}

			if (anim) {
				anim.SetBool("Walk", walkDir != 0);
			}
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
		case FallingTarget.TargetType.SpecialColor0:
			game.onColorCaught(Combo.Color.Color0);
			break;
		case FallingTarget.TargetType.SpecialColor1:
			game.onColorCaught(Combo.Color.Color1);
			break;
		case FallingTarget.TargetType.SpecialColor2:
			game.onColorCaught(Combo.Color.Color2);
			break;
		}

		if (anim) {
			anim.SetTrigger("Punch");
		}
		other.gameObject.SetActive(false);
		Destroy(other.gameObject);
	}
}
