using UnityEngine;
using System.Collections;

public class FloatingObject : MonoBehaviour {

	public float maxUpAndDown = 1; // amount of meters going up and down
	public float speed = 50; // up and down speed
	public float angle = -90; // angle to determin the height by using the sinus
	float toDegrees = Mathf.PI/180; // radians to degrees
	float startHeight; // height of the object when the script starts
	float startX;
	float startZ;
	Vector3 startLocalPosition;


	void Start()
	{
		startHeight = transform.localPosition.y;
		startX = transform.localPosition.x;
		startZ = transform.localPosition.z;
	}
	void FixedUpdate()
	{
		angle += speed * Time.deltaTime;
		if (angle > 270) angle -= 360;
		//Debug.Log(maxUpAndDown * Mathf.Sin(angle * toDegrees));
		transform.localPosition = new Vector3(startX, startHeight + maxUpAndDown * (1 + Mathf.Sin(angle * toDegrees)) / 2 , startZ);
	}
}
