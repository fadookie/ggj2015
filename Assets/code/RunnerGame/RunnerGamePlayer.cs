﻿using UnityEngine;
using System.Collections;

public class RunnerGamePlayer : MonoBehaviour {

	const int FRAMES_NO_COLLISION_CONSIDERED_VALID = 4;
	public float jumpBoostTime = 0.5f;
	public float jumpBoost = 10.0f;

	int collisionCount = 0;
	int framesNoCollision = 0;

	bool onTheGround = false;
	float currentJumpBoost = 0;
	// Use this for initialization
	void Start () {
		currentJumpBoost = jumpBoostTime;
	}
	
	// Update is called once per frame
	void Update () {
		if(collisionCount <= 0)
		{
			framesNoCollision++;
		}
		else
		{
			framesNoCollision = 0;
		}

		onTheGround = framesNoCollision < FRAMES_NO_COLLISION_CONSIDERED_VALID;

		if (onTheGround)
		{
			currentJumpBoost = jumpBoostTime;
		}

		if (Input.GetKeyDown(KeyCode.W))
		{
			if (onTheGround)
			{
				rigidbody.velocity = new Vector3(0f, jumpBoost, 0f);
				framesNoCollision = FRAMES_NO_COLLISION_CONSIDERED_VALID;
			}
			else
			{
				Debug.Log("Could not jump: framesNoCollision: " + framesNoCollision);
			}
		}

#if true
		if (!onTheGround) 
		{
			if (Input.GetKey(KeyCode.W))
			{
				if (currentJumpBoost > 0f)
				{
					currentJumpBoost -= Time.fixedDeltaTime;
					rigidbody.velocity = new Vector3(0f, jumpBoost, 0f);
				}
			}
			else
			{
				currentJumpBoost = 0f;
			}

		}
#endif
	}

	void OnCollisionEnter(Collision collision)
	{
		collisionCount++;
	}

	void OnCollisionExit(Collision collision)
	{
		collisionCount--;
	}
}
