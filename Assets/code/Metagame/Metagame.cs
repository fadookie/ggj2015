using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EnergyBeamView))]
public class Metagame : MinigameBase {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void onGoodEvent(int magnitude) {}

	public override void onBadEvent(int magnitude) {}

	public override void onCombo(Combo combo) {
		print(gameObject.name + " onComboEvent: " + combo);
	}

	public override void onPlayerIdxChange(int oldIdx, int newIdx) {}
}
