using UnityEngine;
using System.Collections;

public class ComboIndicator : MonoBehaviour {

	public Transform triangle;
	public Transform square;
	public Transform circle;

	public Color red = new Color(1f, 0f, 0f);
	public Color green = new Color(0f, 1f, 0f);
	public Color blue = new Color(0f, 0f, 1f);
	public float velocity = 1f;
	public float fadeOutTime = 2f;
	Combo combo;

	Color currentColor;

	public bool pickedUp = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(pickedUp)
		{
			transform.localPosition += new Vector3(0f, velocity * Time.deltaTime, 0f);
			currentColor.a -= (1.0f / fadeOutTime) * Time.deltaTime;
			SetColor(currentColor);
		}
	}

	public void SetCombo(Combo combo)
	{
		triangle.gameObject.SetActive(combo.shape == Combo.Shape.Shape0);
		square.gameObject.SetActive(combo.shape == Combo.Shape.Shape1);
		circle.gameObject.SetActive(combo.shape == Combo.Shape.Shape2);

		currentColor = Color.white;
		switch (combo.color)
		{
		case Combo.Color.Color0:
			currentColor = red;
			break;
		case Combo.Color.Color1:
			currentColor = green;
			break;
		case Combo.Color.Color2:
			currentColor = blue;
			break;
		}

		SetColor(currentColor);
	}

	void SetColor(Color color)
	{
		triangle.GetComponent<SpriteRenderer>().color = color;
		square.GetComponent<SpriteRenderer>().color = color;
		circle.GetComponent<SpriteRenderer>().color = color;
	}
}
