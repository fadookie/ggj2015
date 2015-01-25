using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Animator))]
public class SwordGamePlayer : MonoBehaviour {

	public SwordGame swordGame;
	public SwordGameSlashArea leftSlashArea;
	public SwordGameSlashArea rightSlashArea;
	public Transform graphics;

	public int onPlayerHitScore = -1;
	public float minTimeBetweenAttacks = 0.5f;
	public float stepDistance = 0.2f;
	public float maxDistance = 1.0f;
	public float powerAttackThreshold = 1.0f;
	public float maxChargeTime = 3.0f;
	float timeSinceLastAttack;
	float chargeTime = 0;

	List<SwordGameEnemy> toBeRemoved;

	float prevDirection;
	enum AttackDirection
	{
		Left,
		Right,
	}

	Animator anim;

	// Use this for initialization
	void Start () {
		toBeRemoved = new List<SwordGameEnemy>();
		timeSinceLastAttack = minTimeBetweenAttacks;
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		timeSinceLastAttack += Time.deltaTime;
		float direction = 0;
		
		InputManager input = Services.instance.Get<InputManager>();
		if (input != null) {
			if(input.GetButton(swordGame.PlayerIdx, InputManager.Button.Left)) {
				direction = -1;
			} else if (input.GetButton(swordGame.PlayerIdx, InputManager.Button.Right)) {
				direction = 1;
			}

			if (direction > 0f)
			{
				if (timeSinceLastAttack >= minTimeBetweenAttacks)
				{
					chargeTime += Time.deltaTime;
				}
			}
			if (direction < 0f)
			{
				if (timeSinceLastAttack >= minTimeBetweenAttacks)
				{
					chargeTime += Time.deltaTime;
				}
			}

		}

		anim.SetBool("Charging", chargeTime > 0f);
		Vector3 scale = transform.localScale;
		if (direction != 0f) {
			scale.x = direction;
		}
		transform.localScale = scale;

		if(direction == 0f && prevDirection != 0f)
		{
			if (timeSinceLastAttack >= minTimeBetweenAttacks)
			{
				if (chargeTime >= powerAttackThreshold)
				{
					PowerAttack();
				}
				else
				{
					Attack(prevDirection > 0 ? AttackDirection.Right : AttackDirection.Left);
				}
				chargeTime = 0.0f;
				timeSinceLastAttack = 0.0f;
			}
		}
		prevDirection = direction;
		UpdateGraphics();
		Cleanup();
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

	void PowerAttack()
	{
		foreach(var enemy in leftSlashArea.GetIntersectingEnemies())
		{
			KillEnemy(enemy);
		}
		foreach(var enemy in rightSlashArea.GetIntersectingEnemies())
		{
			KillEnemy(enemy);
		}

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
		enemy.Kill();
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
		enemy.OnHitPlayer();

	}

}
