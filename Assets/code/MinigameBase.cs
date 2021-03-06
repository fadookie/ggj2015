﻿using UnityEngine;
using System.Collections;

public abstract class MinigameBase : MonoBehaviour {
	#region Event declarations
	public delegate void MinigameScoreEventHandler(MinigameBase sender, int delta);
	public MinigameScoreEventHandler onScoreIncrease;
	public MinigameScoreEventHandler onScoreDecrease;

	public delegate void MinigameComboEventHandler(MinigameBase sender, Combo combo);
	public MinigameComboEventHandler onComboEventPassed;
	public MinigameComboEventHandler onComboEventFailed;
	#endregion

	int _score;
	public int Score {
		get {
			return _score;
		}
		set {
			int delta = value - _score;
			_score = value;
			if (onScoreIncrease != null) {
				if(delta > 0) {
					onScoreIncrease(this, delta);
				} else if (delta < 0) {
					onScoreDecrease(this, delta);
				}
			}
		}
	}

	int _playerIdx;
	public int PlayerIdx {
		get {
			return _playerIdx;
		}
		set {
			int oldIdx = _playerIdx;
			_playerIdx = value;
			onPlayerIdxChange(oldIdx, value);
		}
	}

	public void postComboEventPassed(Combo combo) {
		if(onComboEventPassed != null) {
			onComboEventPassed(this, combo);
		}
	}

	public void postComboEventFailed(Combo combo) {
		if(onComboEventFailed != null) {
			onComboEventFailed(this, combo);
		}
	}

	#region Respond callback
	public abstract void onGoodEvent(int magnitude);
	public abstract void onBadEvent(int magnitude);

	public abstract void onCombo(Combo combo);

	public abstract void onPlayerIdxChange(int oldIdx, int newIdx);
	#endregion
}
