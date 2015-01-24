using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CatcherGame : MinigameBase {

	float TargetTimeScaleDefault;
	public float TargetTimeScale = 1f; //{ get; private set; }
	public float TargetTimeScaleMin = 0.6f;
	public float TargetTimeScaleMax = 2f;

	public float TimeScaleAdjustOnGood = -0.05f;
	public float TimeScaleAdjustOnBad = 0.05f;

	public float TimeScaleEffectDurationS = 2f;
	float timeScaleEffectElapsed = -1f;

	public Text scoreLabel;

	// Use this for initialization
	void Start () {
		Services.instance.Set<CatcherGame>(this);
		TargetTimeScaleDefault = TargetTimeScale;
	}
	
	// Update is called once per frame
	void Update () {
		scoreLabel.text = string.Format("Score: {0}", Score);

		if (timeScaleEffectElapsed >= 0) {
			timeScaleEffectElapsed += Time.deltaTime;
			if (timeScaleEffectElapsed > TimeScaleEffectDurationS) {
				TargetTimeScale = TargetTimeScaleDefault;
				timeScaleEffectElapsed = -1f;
			}
		}
	}

	void adjustTargetTimeScale(float deltaS) {
		TargetTimeScale = Mathf.Clamp(TargetTimeScale + deltaS, TargetTimeScaleMin, TargetTimeScaleMax);
		timeScaleEffectElapsed = 0f;
	}

	public override void onGoodEvent(int magnitude) {
		print(gameObject.name + " onGoodEvent mag:" + magnitude);
		adjustTargetTimeScale(TimeScaleAdjustOnGood * Mathf.Abs(magnitude));
	}

	public override void onBadEvent(int magnitude) {
		print(gameObject.name + " onBadEvent mag:" + magnitude);
		Services.instance.Get<TargetSpawner>().spawnTargetOfType(FallingTarget.TargetType.ShouldNotCatch);
		adjustTargetTimeScale(TimeScaleAdjustOnBad *  Mathf.Abs(magnitude));
	}

	public override void onPlayerIdxChange(int oldIdx, int newIdx) {
		print(gameObject.name + " onPlayerIdxChange oldIdx:" + oldIdx + " newIdx: " + newIdx);
	}
}
