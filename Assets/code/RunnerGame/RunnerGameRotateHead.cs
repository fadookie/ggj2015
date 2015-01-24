using UnityEngine;
using System.Collections;

public class RunnerGameRotateHead : MonoBehaviour {

	public float maxAngle = 20f;
	public float animationTime = 2.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float factor = Mathf.Sin(Time.realtimeSinceStartup * Mathf.PI * 2.0f / animationTime);
		transform.localRotation = Quaternion.Euler(270f, factor * maxAngle, 0f);
	}
}
