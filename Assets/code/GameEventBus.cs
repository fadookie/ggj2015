using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameEventBus : MonoBehaviour {
	public string[] gameNames;
	List<MinigameBase> games;

	bool gamesLoaded = false;

	public bool randomizePlayers = false;
	public float randomizeDelayAlertPreroll = 1;
	public float randomizePlayersDelayS = 15;
	IEnumerator randomizePlayersRoutine = null;

	public RectTransform InputSwapAlert;

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
			if (!randomizePlayers) {
				yield break;
			}

			yield return new WaitForSeconds(randomizePlayersDelayS - randomizeDelayAlertPreroll);

			if (InputSwapAlert) InputSwapAlert.gameObject.SetActive(true);
			yield return new WaitForSeconds(randomizeDelayAlertPreroll);

			if (InputSwapAlert) InputSwapAlert.gameObject.SetActive(false);

			//Shuffle PlayerIdx variables but not actual order in games array to keep input/outputs intact
			MinigameBase[] gamesTemp = games.ToArray();
			if (gamesLoaded) {
				gamesTemp.Shuffle();
				for (int i = 0; i < gamesTemp.Length; ++i) {
					MinigameBase game = gamesTemp[i];
					game.PlayerIdx = i;
				}
				print(string.Format("Game inputs shuffled! new order = {0}", gamesTemp.toString()));
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
