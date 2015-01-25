using UnityEngine;
using System.Collections;

public class EnergyBeamView : MonoBehaviour {

	public Transform energyBeamCenter;
//	public Transform energyBeamMinX;
//	public Transform energyBeamMaxX;
	public float meterMinXScale = 0.01f;
	public float meterMaxXScale = 2f;

	public BoxCollider playerMeter;
	public Transform playerMeterCap;

	public BoxCollider enemyMeter;
	public Transform enemyMeterCap;

#if UNITY_EDITOR
	public float _FillPct = 0f;
#else
	float _FillPct = 0f;
#endif

	public float FillPct {
		get { return _FillPct; }
		set {
			_FillPct = value;
			updatePos();
		}
	}

	void updatePos() {
//		energyBeamCenter.position = Vector3.Lerp(energyBeamMinX.position, energyBeamMaxX.position, _FillPct);
		{
			Vector3 scale = playerMeter.transform.localScale;
			scale.x = Mathf.Lerp(meterMinXScale, meterMaxXScale, _FillPct);
			playerMeter.transform.localScale = scale;
			print (string.Format("playerScale: {0}", scale));

			Vector3 pos = playerMeterCap.transform.position;
			pos.x = playerMeter.bounds.max.x;
			playerMeterCap.transform.position = pos;
		}

		{
			Vector3 scale = enemyMeter.transform.localScale;
			scale.x = Mathf.Lerp(meterMinXScale, meterMaxXScale, 1 - _FillPct);
			enemyMeter.transform.localScale = scale;
			print (string.Format("enemyScale: {0}", scale));

			Vector3 pos = enemyMeterCap.transform.position;
			pos.x = enemyMeter.bounds.min.x;
			enemyMeterCap.transform.position = pos;
		}
	}

	// Use this for initialization
	void Start () {
	
	}

#if UNITY_EDITOR
	void Update () {
		FillPct = _FillPct;
	}
#endif
}
