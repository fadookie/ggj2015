using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EnergyBeamView))]
public class Metagame : MinigameBase {

	int _metaScore;
	int metaScore {
		get { return _metaScore; }
		set { 
			_metaScore = value;
			if (value >= winScore) {
				win();
			} else if (value <= 0) {
				lose();
			}
			updateView();
		}
	}
	public int winScore = 10000;
	public int comboScore = 100;
	float winScorePct {
		get { return (float)metaScore / (float)winScore; }
	}

	public float bleedScoreInterval = 1;
	public int bleedScoreAmt = 1;

	EnergyBeamView view;

	// Use this for initialization
	void Start () {
		metaScore = winScore / 2;
		view = GetComponent<EnergyBeamView>();

		StartCoroutine(bleedScore());
	}
	
	// Update i_metaScoreed once per frame
	void Update () {
	
	}
	void updateView() {
		if (view != null) {
			view.FillPct = winScorePct;
		}
	}

	IEnumerator bleedScore() {
		while(true) {
			yield return new WaitForSeconds(bleedScoreInterval);
			metaScore -= bleedScoreAmt;
		}
	}

	void win() {
		print ("WIN!!!!");
		SceneLoader.loadWinScene();
	}

	void lose() {
		print ("LOSE!!!!");
		SceneLoader.loadLoseScene();
	}

	public override void onGoodEvent(int magnitude) {
		metaScore += magnitude;
		print(gameObject.name + " onGoodEvent mag:" + magnitude);
	}

	public override void onBadEvent(int magnitude) {
		metaScore += magnitude;
		print(gameObject.name + " onBadEvent mag:" + magnitude);
	}

	public override void onCombo(Combo combo) {
		print(gameObject.name + " onComboEvent: " + combo);
		metaScore += comboScore;
	}

	public override void onPlayerIdxChange(int oldIdx, int newIdx) {}
}
