using UnityEngine;
using System.Collections;

public class GameEventBus : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Services.instance.Set<GameEventBus>(this);
	}
}
