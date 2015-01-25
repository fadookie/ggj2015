using UnityEngine;
using System.Collections;

public class Splash : MonoBehaviour {

	public float waitTime = 5.0f;
	// Use this for initialization
	void Start () {
		StartCoroutine(StartGameRoutine());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator StartGameRoutine()
	{
		yield return new WaitForSeconds(waitTime);
		Application.LoadLevel("Main");
	}
}
