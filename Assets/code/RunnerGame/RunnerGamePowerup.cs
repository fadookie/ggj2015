using UnityEngine;
using System.Collections;

public class RunnerGamePowerup : MonoBehaviour {

	public Combo.Shape shape;
	public RunnerGameWorld world;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 pos = transform.localPosition;
		pos.x -= world.GetVelocity() * Time.deltaTime;
		transform.localPosition = pos;
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.name == "Player")
		{
			Services.instance.Get<RunnerGame>().onShapeCaught(shape);
			GetComponent<ComboIndicator>().pickedUp = true;
			print("Picked up!");
		}
	}
}
