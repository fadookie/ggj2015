using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class SwordGameEnemy : MonoBehaviour {

	public SwordGame swordGame;
	public ComboIndicator comboIndicator;
	public float velocity = 1f;
	public int pointsValue = 1;
	public bool superEnemy = false;
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
		if (!dead)
		{
			dead = true;
			if (anim)
			{
				anim.SetTrigger("Die");
			}
			if (superEnemy)
			{
				Combo.Color[] colors = { Combo.Color.Color0, Combo.Color.Color1, Combo.Color.Color2 }; 
				Combo.Shape[] shapes = { Combo.Shape.Shape0, Combo.Shape.Shape1, Combo.Shape.Shape2 }; 
				Combo newCombo;
				newCombo.color = colors[Random.Range(0, colors.Length)];
				newCombo.shape = shapes[Random.Range(0, shapes.Length)];
				Debug.Log (string.Format("Creating new combo: {0} / {1}", newCombo.color, newCombo.shape));
				swordGame.postComboEventPassed(newCombo);

				ComboIndicator newComboIndicator = Instantiate(comboIndicator) as ComboIndicator;
				newComboIndicator.SetCombo(newCombo);
				newComboIndicator.transform.parent = transform.parent;
				newComboIndicator.transform.position = transform.position + new Vector3(0f, 3f, 0f);
				newComboIndicator.pickedUp = true;
			}
		}
	}

	public void OnHitPlayer()
	{
		if (!dead)
		{
			dead = true;
			alpha = 0f;
			if (anim)
			{
				anim.SetTrigger("Die");
			}
		}
	}
}
