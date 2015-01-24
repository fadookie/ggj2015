using UnityEngine;
using System.Collections;

public class SwordGameEnemy : MonoBehaviour {

	public Transform graphics;
	public SwordGame swordGame;
	public float velocity = 1f;
	public int pointsValue = 1;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 direction = swordGame.player.transform.position - transform.position;

		direction.Normalize();
		transform.localPosition += direction * velocity * Time.deltaTime;
	}
}
