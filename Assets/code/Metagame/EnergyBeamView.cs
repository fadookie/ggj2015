using UnityEngine;
using System.Collections;

public class EnergyBeamView : MonoBehaviour {

	public Transform energyBeamCenter;
	public Transform energyBeamMinX;
	public Transform energyBeamMaxX;

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
		energyBeamCenter.position = Vector3.Lerp(energyBeamMinX.position, energyBeamMaxX.position, _FillPct);
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
