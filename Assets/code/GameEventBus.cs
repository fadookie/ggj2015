using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameEventBus : MonoBehaviour {
	public string[] gameNames;
	List<MinigameBase> games;

	bool gamesLoaded = false;

	public bool randomizePlayers = false;
	public float randomizePlayersDelayS = 15;
	IEnumerator randomizePlayersRoutine = null;

	// Use this for initialization
	void Start () {
		if (Services.instance.Get<GameEventBus>() != null) {
			//Abort load as another GameEventBus is present
			gameObject.SetActive(false);
			return;
		}

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
		games = new List<MinigameBase>(gameNames.Length);
		foreach(string gameName in gameNames) {
			GameObject gameObj = null;
			int numCyclesToFindObj = 0;
			do {
				gameObj = GameObject.Find(gameName);
				if (gameObj != null) {
					break;
				}
				++numCyclesToFindObj;
				yield return new WaitForEndOfFrame();
			} while (gameObj == null);
			print ("find object (" + gameObj + ") with name " + gameName + " after " + numCyclesToFindObj + " cycles");
			MinigameBase game = gameObj.GetComponent<MinigameBase>();
			games.Add(game);
			game.PlayerIdx = games.IndexOf(game);
			subscribeToGame(game);
		}
		gamesLoaded = true;

		randomizePlayersRoutine = randomizePlayersAfterDelay();
		StartCoroutine(randomizePlayersRoutine);

	}

	IEnumerator randomizePlayersAfterDelay() {
		do {
			yield return new WaitForSeconds(randomizePlayersDelayS);
			if (randomizePlayers && gamesLoaded) {
				games.Shuffle();
				for (int i = 0; i < games.Count; ++i) {
					MinigameBase game = games[i];
					game.PlayerIdx = i;
				}
				print(string.Format("Games shuffled! new order = {0}", games.toString()));
			}
		} while(true);
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
