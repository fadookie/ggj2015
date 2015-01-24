using UnityEngine;
using System.Collections;

public class FallingTarget : MonoBehaviour {

	public float moveVel = 1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newPos = transform.localPosition;
		newPos.y -= moveVel; 
		transform.localPosition = newPos; 
	}
}
