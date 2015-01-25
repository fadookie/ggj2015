using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CatcherGame : MinigameBase {

	float TargetTimeScaleDefault = 1f;
	public float TargetTimeScale {
		get {
			return Mathf.Clamp(TargetTimeScaleDefault + timeScaleBad + timeScaleGood, TargetTimeScaleMin, TargetTimeScaleMax);
		}
	}
	public float TargetTimeScaleMin = 0.6f;
	public float TargetTimeScaleMax = 2f;

	public float TimeScaleAdjustOnGood = -0.05f;
	public float TimeScaleAdjustOnBad = 0.05f;

	public float TimeScaleEffectDurationS = 2f;
	float timeScaleBad;
	float timeScaleGood;
	float timeScaleBadEffectElapsed = -1f;
	float timeScaleGoodEffectElapsed = -1f;

	public Text scoreLabel;

	Combo? activeCombo = null;

	// Use this for initialization
	void Start () {
		Services.instance.Set<CatcherGame>(this);
	}
	
	// Update is called once per frame
	void Update () {
		scoreLabel.text = string.Format("Score: {0}", Score);

		if (timeScaleBadEffectElapsed >= 0) {
			timeScaleBadEffectElapsed += Time.deltaTime;
			if (timeScaleBadEffectElapsed > TimeScaleEffectDurationS) {
				timeScaleBad = 0;
				timeScaleBadEffectElapsed = -1f;
			}
			if (timeScaleGoodEffectElapsed > TimeScaleEffectDurationS) {
				timeScaleGood = 0;
				timeScaleGoodEffectElapsed = -1f;
			}
		}
	}

	public void onColorCaught(Combo.Color caughtColor) {
		if (activeCombo.HasValue) {
			if (activeCombo.Value.color.Equals(caughtColor)) {
				postComboEventPassed(activeCombo.Value);
			} else {
				//Combo failed!
				postComboEventFailed(activeCombo.Value);
				activeCombo = null;
			}
		}
	}

	public override void onGoodEvent(int magnitude) {
		print(gameObject.name + " onGoodEvent mag:" + magnitude);
		timeScaleGood += TimeScaleAdjustOnGood *  Mathf.Abs(magnitude);
		timeScaleGoodEffectElapsed = 0f;
	}

	public override void onBadEvent(int magnitude) {
		print(gameObject.name + " onBadEvent mag:" + magnitude);
		Services.instance.Get<TargetSpawner>().spawnTargetOfType(FallingTarget.TargetType.ShouldNotCatch);
		timeScaleBad += TimeScaleAdjustOnBad *  Mathf.Abs(magnitude);
		timeScaleBadEffectElapsed = 0f;
	}

	public override void onCombo(Combo combo) {
		print(gameObject.name + " onComboEvent: " + combo);
		Services.instance.Get<TargetSpawner>().spawnSpecialTargets();
		activeCombo = combo;
	}

	public override void onPlayerIdxChange(int oldIdx, int newIdx) {
//		print(gameObject.name + " onPlayerIdxChange oldIdx:" + oldIdx + " newIdx: " + newIdx);
	}
}
