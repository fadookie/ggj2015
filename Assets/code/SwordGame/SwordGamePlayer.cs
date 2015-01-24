﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwordGamePlayer : MonoBehaviour {

	public SwordGame swordGame;
	public SwordGameSlashArea leftSlashArea;
	public SwordGameSlashArea rightSlashArea;
	public Transform graphics;

	public int onPlayerHitScore = -1;
	public float minTimeBetweenAttacks = 0.5f;
	public float stepDistance = 0.2f;
	public float maxDistance = 1.0f;
	float timeSinceLastAttack;

	List<SwordGameEnemy> toBeRemoved;

	float prevDirection;
	enum AttackDirection
	{
		Left,
		Right,
	}

	// Use this for initialization
	void Start () {
		toBeRemoved = new List<SwordGameEnemy>();
		timeSinceLastAttack = minTimeBetweenAttacks;
	}
	
	// Update is called once per frame
	void Update () {
		timeSinceLastAttack += Time.deltaTime;

		float direction = Input.GetAxis("Horizontal");
		if (direction > 0f && !(prevDirection > 0f))
		{
			if (timeSinceLastAttack >= minTimeBetweenAttacks)
			{
				Attack (AttackDirection.Right);
				timeSinceLastAttack = 0.0f;
			}
		}
		if (direction < 0f && !(prevDirection < 0f))
		{
			if (timeSinceLastAttack >= minTimeBetweenAttacks)
			{
				Attack (AttackDirection.Left);
				timeSinceLastAttack = 0.0f;
			}
		}
		prevDirection = direction;

		UpdateGraphics();

	}

	void UpdateGraphics()
	{
		if (timeSinceLastAttack < minTimeBetweenAttacks)
		{
			graphics.renderer.material.color = new Color(1f, 1f, 0f);
		}
		else
		{
			graphics.renderer.material.color = new Color(0f, 1f, 0f);
		}
	}

	void Attack(AttackDirection direction)
	{

		switch(direction)
		{
		case AttackDirection.Left:
			foreach(var enemy in leftSlashArea.GetIntersectingEnemies())
			{
				KillEnemy(enemy);
			}
			break;
		case AttackDirection.Right:
			foreach(var enemy in rightSlashArea.GetIntersectingEnemies())
			{
				KillEnemy(enemy);
			}
			break;
		default:
			Debug.LogError("Unexpected direction:" + direction.ToString());
			break;
		}


		Move(direction);
		Cleanup();
	}

	void Move (AttackDirection direction)
	{
		Vector3 pos = transform.localPosition;
		pos.x += direction == AttackDirection.Right ? stepDistance : -stepDistance;
		pos.x = Mathf.Clamp(pos.x, -maxDistance, maxDistance);
		transform.localPosition = pos;
	}
	
	void Cleanup()
	{
		foreach(var enemy in toBeRemoved)
		{
			leftSlashArea.RemoveEnemy(enemy);
			rightSlashArea.RemoveEnemy(enemy);
		}

		toBeRemoved.Clear();
	}

	void KillEnemy (SwordGameEnemy enemy)
	{
		toBeRemoved.Add(enemy);
		swordGame.Score += enemy.pointsValue;
		Destroy(enemy.gameObject);
	}

	void OnTriggerEnter(Collider collider)
	{
		SwordGameEnemy enemy = collider.GetComponent<SwordGameEnemy>();
		if (enemy)
		{
			EnemyHitPlayer(enemy);
		}
	}

	void EnemyHitPlayer(SwordGameEnemy enemy)
	{
		// remove enemy from both slash areas
		leftSlashArea.RemoveEnemy(enemy);
		rightSlashArea.RemoveEnemy(enemy);

		swordGame.Score += onPlayerHitScore;
		Destroy(enemy.gameObject);
	}

}
