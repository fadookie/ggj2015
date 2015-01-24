using UnityEngine;
using System.Collections;

public class RunnerGamePlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.W))
		{
			rigidbody.AddForce(new Vector3(0.0f, 300.0f, 0.0f));
		}
	}
}
