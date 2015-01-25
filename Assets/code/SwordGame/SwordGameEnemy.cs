using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class SwordGameEnemy : MonoBehaviour {

	public SwordGame swordGame;
	public float velocity = 1f;
	public int pointsValue = 1;
	float alpha = 1;
	bool dead;
	SpriteRenderer spriteRenderer;

	Animator anim;

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!IsDead())
		{
			Vector3 direction = swordGame.player.transform.position - transform.position;

			direction.Normalize();
			transform.localPosition += direction * velocity * Time.deltaTime;

			if (direction.x != 0f)
			{
				Vector3 scale = transform.localScale;
				scale.x = direction.x < 0f ? 1f : -1f;
				transform.localScale = scale;
			}
		}
		else
		{
			// fade out
			alpha -= Time.deltaTime;
			Color color = spriteRenderer.color;
			color.a = alpha;
			spriteRenderer.color = color;
			if(alpha <= 0.0f)
			{
				Destroy(gameObject);
			}
		}
	}
	public bool IsDead()
	{
		return dead;
	}

	public void Kill()
	{
		dead = true;
		if(anim)
		{
			anim.SetTrigger("Die");
		}
	}
}
