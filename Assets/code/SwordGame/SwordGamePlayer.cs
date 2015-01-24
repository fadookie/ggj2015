using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwordGamePlayer : MonoBehaviour {

	public SwordGame swordGame;
	public SwordGameSlashArea leftSlashArea;
	public SwordGameSlashArea rightSlashArea;

	public int onPlayerHitScore = -1;

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
	}
	
	// Update is called once per frame
	void Update () {
		float direction = Input.GetAxis("Horizontal");
		if (direction > 0f && !(prevDirection > 0f))
		{
			Attack (AttackDirection.Right);
		}
		if (direction < 0f && !(prevDirection < 0f))
		{
			Attack (AttackDirection.Left);
		}
		prevDirection = direction;

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

		Cleanup();
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
