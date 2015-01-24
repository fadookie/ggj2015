using UnityEngine;
using System.Collections;

public class RunnerGamePowerup : MonoBehaviour {

	public RunnerGameWorld world;
	float rotation = 0;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 pos = transform.localPosition;
		pos.x -= world.GetVelocity() * Time.deltaTime;
		transform.localPosition = pos;

		rotation += 20f * Time.deltaTime;
		transform.rotation = Quaternion.Euler(0f, rotation, 0f);
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.name == "Player")
		{
			Debug.Log("player picked up powerup!");
			Destroy(this.gameObject);
		}
	}
}
