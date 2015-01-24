using UnityEngine;
using System.Collections;

public class RunnerGamePowerup : MonoBehaviour {

	public Combo.Shape shape;
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
		transform.rotation = Quaternion.Euler(rotation, 0f, 0f);
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.name == "Player")
		{
			Services.instance.Get<RunnerGame>().onShapeCaught(shape);
			Destroy(this.gameObject);
		}
	}
}
