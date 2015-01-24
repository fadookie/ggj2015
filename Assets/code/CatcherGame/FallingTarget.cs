using UnityEngine;
using System.Collections;

public class FallingTarget : MonoBehaviour {

	public float moveVel = 1f;

	public enum TargetType {
		ShouldCatch = 0,
		ShouldNotCatch,
		Bomb
	}

	public Material[] materialsPerType;

	public TargetType targetType = TargetType.ShouldCatch;

	// Use this for initialization
	void Start () {
		renderer.material = materialsPerType[(int)targetType];
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newPos = transform.localPosition;
		newPos.y -= moveVel; 
		transform.localPosition = newPos; 
	}
}
