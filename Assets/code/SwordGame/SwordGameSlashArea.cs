using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwordGameSlashArea : MonoBehaviour {

	List<SwordGameEnemy> enemies;

	// Use this for initialization
	void Start () {
		enemies = new List<SwordGameEnemy>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public List<SwordGameEnemy> GetIntersectingEnemies()
	{
		return enemies;
	}

	void OnCollisionEnter(Collision collision)
	{
		Collider collider = collision.collider;
		SwordGameEnemy enemy = collider.GetComponent<SwordGameEnemy>();
		if (enemy)
		{
			Debug.Log("Enemy entered the collision area!");
			enemies.Add(enemy);
		}
	}

	void OnCollisionExit(Collision collision)
	{
		Collider collider = collision.collider;
		SwordGameEnemy enemy = collider.GetComponent<SwordGameEnemy>();
		if (enemy)
		{
			Debug.Log("Enemy left the collision area!");
			enemies.Remove(enemy);
		}
	}
}
