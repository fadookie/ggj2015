﻿using UnityEngine;
using System.Collections;

public class SwordGameEnemy : MonoBehaviour {

	public SwordGame swordGame;
	public float velocity = 1f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 direction = swordGame.player.transform.position - transform.position;
		if(direction.sqrMagnitude > 1f)
		{
			direction.Normalize();
		}
		transform.localPosition += direction * velocity * Time.deltaTime;
	}
}
