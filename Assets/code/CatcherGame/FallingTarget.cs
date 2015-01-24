using UnityEngine;
using System.Collections;

public class FallingTarget : MonoBehaviour {

	public float moveVel = 1f;

	CatcherGame game;

	public enum TargetType {
		ShouldCatch = 0,
		ShouldNotCatch,
		SpecialColor0,
		SpecialColor1,
		SpecialColor2,
		Bomb,
	}

	public Material[] materialsPerType;

	public TargetType targetType = TargetType.ShouldCatch;

	// Use this for initialization
	void Start () {
		game = Services.instance.Get<CatcherGame>();
		renderer.material = materialsPerType[(int)targetType];
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newPos = transform.localPosition;
		newPos.y -= moveVel * Time.deltaTime *	game.TargetTimeScale;
		transform.localPosition = newPos; 
	}
}
