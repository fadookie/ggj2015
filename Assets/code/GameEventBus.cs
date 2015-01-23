using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameEventBus : MonoBehaviour {
	public string[] gameNames;
	List<MinigameBase> games;

	// Use this for initialization
	void Start () {
		Services.instance.Set<GameEventBus>(this);
		SceneLoader sl = Services.instance.Get<SceneLoader>();
		if (sl.scenesLoaded) {
			onScenesLoaded();
		} else {
			sl.onScenesLoaded += onScenesLoaded;
		}
	}

	void onScenesLoaded() {
		StartCoroutine(findGameObjectsAfterDelay());
	}

	IEnumerator findGameObjectsAfterDelay() {
		yield return new WaitForEndOfFrame();

		games = new List<MinigameBase>(gameNames.Length);
		foreach(string gameName in gameNames) {
			GameObject gameObj = GameObject.Find(gameName);
			print ("find object (" + gameObj + ") with name " + gameName);
			MinigameBase game = gameObj.GetComponent<MinigameBase>();
			games.Add(game);
			subscribeToGame(game);
		}
	}

	void subscribeToGame(MinigameBase game) {
		game.onScoreIncrease += onScoreIncrease;
		game.onScoreDecrease += onScoreDecrease;
	}

	void onScoreIncrease(MinigameBase sender, int delta) {
		int gameIdx = games.IndexOf(sender);
		int nextGameIdx = (gameIdx + 1) % games.Count;
		games[nextGameIdx].onGoodEvent(delta);
	}

	void onScoreDecrease(MinigameBase sender, int delta) {
		int gameIdx = games.IndexOf(sender);
		int nextGameIdx = (gameIdx + 1) % games.Count;
		games[nextGameIdx].onBadEvent(delta);
	}
}
