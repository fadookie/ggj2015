﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputManager : MonoBehaviour {

	public const int NUM_PLAYERS = 3;
	public List<string[]> playerInputs = new List<string[]>(NUM_PLAYERS);
	public string[] player0Inputs;
	public string[] player1Inputs;
	public string[] player2Inputs;

	public enum Button {
		Left = 0,
		Right,
//		Special
	}

	// Use this for initialization
	void Start () {
		Services.instance.Set<InputManager>(this);

		playerInputs.Add(player0Inputs);
		playerInputs.Add(player1Inputs);
		playerInputs.Add(player2Inputs);
	}

	public bool GetButton(int playerIdx, Button button) {
		string buttonName = GetButtonName(playerIdx, button);
		bool ret = false;
		try {
			ret = Input.GetButton(buttonName);
		} catch(UnityException e) {
			throw new System.Exception(string.Format("GetButton exception: playerIdx:{0} button:{1} buttonName:'{2}', innerMessage: {3}", playerIdx, button, buttonName, e.Message));
		}
		return ret;
	}

	public bool GetButtonDown(int playerIdx, Button button) {
		string buttonName = GetButtonName(playerIdx, button);
		return Input.GetButtonDown(buttonName);
	}

	public string GetButtonName(int playerIdx, Button button) {
		try {
			return playerInputs[playerIdx][(int)button];
		} catch (System.ArgumentOutOfRangeException) {
			throw new UnityException(string.Format("Argument out of range for playerIdx={0}, button={1}", playerIdx, button));
		}
	}
}
