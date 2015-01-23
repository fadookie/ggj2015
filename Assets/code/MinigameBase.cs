using UnityEngine;
using System.Collections;

public abstract class MinigameBase : MonoBehaviour {
	#region Event declarations
	public delegate void MinigameScoreEventHandler(MinigameBase sender, int delta);
	public MinigameScoreEventHandler onScoreIncrease;
	public MinigameScoreEventHandler onScoreDecrease;
	#endregion

	int _score;
	public int Score {
		get {
			return _score;
		}
		set {
			_score = value;
			if (onScoreIncrease != null) {
				if(value > 0) {
					onScoreIncrease(this, value);
				} else if (value < 0) {
					onScoreDecrease(this, value);
				}
			}
		}
	}

	#region Respond callback
	public abstract void onGoodEvent(int magnitude);
	public abstract void onBadEvent(int magnitude);
	#endregion
}
