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
			} else if (value <= warningFlashThreshold) {
				startWarningFlash();
			} else {
				stopWarningFlash();
			}
			updateView();
		}
	}
	public int winScore = 10000;
	public int warningFlashThreshold = 1000;
	public int comboScore = 100;
	float winScorePct {
		get { return (float)metaScore / (float)winScore; }
	}

	public float bleedScoreInterval = 1;
	public int bleedScoreAmt = 1;

	public float timeStretchAcceleration = 0.01f;

	public Renderer warningFlashQuad;
	bool warningFlashing = false;
	IEnumerator warningFlashRoutine = null;
	public float warningFlashPeriod = 1;

	EnergyBeamView view;

	// Use this for initialization
	void Start () {
		metaScore = winScore / 2;
		view = GetComponent<EnergyBeamView>();

		StartCoroutine(bleedScore());
	}
	
	// Update i_metaScoreed once per frame
	void Update () {
		float timeScaleDelta = timeStretchAcceleration * (Time.deltaTime / Time.timeScale);
//		print (timeScaleDelta);
		Time.timeScale += timeScaleDelta;
		Time.timeScale = Mathf.Clamp(Time.timeScale, 1f, 4f);
//		print (Time.timeScale);
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

	void startWarningFlash() {
		warningFlashQuad.gameObject.SetActive(true);
		warningFlashRoutine = runWarningFlash();
		StartCoroutine(warningFlashRoutine);
	}

	void stopWarningFlash() {
		if (warningFlashRoutine != null) {
			StopCoroutine(warningFlashRoutine);
			warningFlashRoutine = null;
			warningFlashQuad.gameObject.SetActive(false);
		}
	}

	IEnumerator runWarningFlash() {
//		float timeElapsedS = 0;
		while(true) {
			Color newColor = warningFlashQuad.material.color;
			newColor.a = ((Mathf.Sin(Time.time * warningFlashPeriod) / 2 * Mathf.PI) * 0.5f) + 0.5f;
			warningFlashQuad.material.color = newColor;
			yield return new WaitForEndOfFrame();
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
//		print(gameObject.name + " onGoodEvent mag:" + magnitude);
	}

	public override void onBadEvent(int magnitude) {
		metaScore += magnitude;
//		print(gameObject.name + " onBadEvent mag:" + magnitude);
	}

	public override void onCombo(Combo combo) {
		print(gameObject.name + " onComboEvent: " + combo);
		metaScore += comboScore;
	}

	public override void onPlayerIdxChange(int oldIdx, int newIdx) {}
}
