using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Renderer))]
public class TexturePanner : MonoBehaviour {

	public Vector2 panVelocity = Vector2.zero;

	// Update is called once per frame
	void Update () {
		Vector2 offset = renderer.material.GetTextureOffset("_MainTex");
		offset += panVelocity * Time.deltaTime;
		renderer.material.SetTextureOffset("_MainTex", offset);
	}
}
