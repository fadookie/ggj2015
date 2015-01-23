using UnityEngine;
using System.Collections;

public class SwordGamePlayer : MonoBehaviour {

	public SwordGameSlashArea leftSlashArea;
	public SwordGameSlashArea rightSlashArea;

	float prevDirection;
	enum AttackDirection
	{
		Left,
		Right,
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float direction = Input.GetAxis("Horizontal");
		if (direction > 0f && !(prevDirection > 0f))
		{
			Attack (AttackDirection.Left);
		}
		if (direction < 0f && !(prevDirection < 0f))
		{
			Attack (AttackDirection.Right);
		}
		prevDirection = direction;

	}

	void Attack(AttackDirection direction)
	{
		Debug.Log("Attacking in direction:" + direction.ToString());

		switch(direction)
		{
		case AttackDirection.Left:
			foreach(var enemy in leftSlashArea.GetIntersectingEnemies())
			{
				Destroy(enemy);
			}
			break;
		case AttackDirection.Right:
			foreach(var enemy in rightSlashArea.GetIntersectingEnemies())
			{
				Destroy(enemy);
			}
			break;
		default:
			Debug.LogError("Unexpected direction:" + direction.ToString());
			break;
		}
	}
}
