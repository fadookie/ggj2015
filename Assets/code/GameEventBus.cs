//#define SEND_EVENTS_ON_SCORE_CHANGE

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameEventBus : MonoBehaviour {
	public string[] gameNames;
	public List<MinigameBase> games;

	public string metagameName = "Metagame";
	MinigameBase metagame;

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

	void Update() {
		float timeScaleDelta = 0.01f * (Time.deltaTime / Time.timeScale);
//		print (timeScaleDelta);
		Time.timeScale += timeScaleDelta;
		Time.timeScale = Mathf.Clamp(Time.timeScale, 1f, 4f);
//		print (Time.timeScale);
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

		do {
			GameObject gameObj = GameObject.Find(metagameName);
			metagame = gameObj.GetComponent<MinigameBase>();
			if (metagame != null) {
				print ("found object (" + metagame + ") with name " + metagameName);
				break;
			}
			yield return new WaitForEndOfFrame();
		} while (metagame == null);

		gamesLoaded = true;

		randomizePlayersRoutine = randomizePlayersAfterDelay();
		StartCoroutine(randomizePlayersRoutine);

	}

	IEnumerator randomizePlayersAfterDelay() {
		do {
			if (!randomizePlayers) {
				yield break;
			}

			yield return new WaitForSeconds((randomizePlayersDelayS - randomizeDelayAlertPreroll) * Time.timeScale);

			if (InputSwapAlert) InputSwapAlert.gameObject.SetActive(true);
			yield return new WaitForSeconds(randomizeDelayAlertPreroll *  Time.timeScale);

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
		game.onComboEventPassed += onComboEventPassed;
		game.onComboEventFailed += onComboEventFailed;
	}

	void onComboEventPassed(MinigameBase sender, Combo combo) {
		//Only pass combo events forward, don't wrap back to 1st game
		int gameIdx = games.IndexOf(sender);
		int nextGameIdx = gameIdx + 1;
		if (nextGameIdx < games.Count) {
			games[nextGameIdx].onCombo(combo);
		} else if (nextGameIdx == games.Count) {
			//Special case, pass to metagame
			metagame.onCombo(combo);
		}
	}

	void onComboEventFailed(MinigameBase sender, Combo combo) {
		print(string.Format("GameEventBus::onComboEventFailed: {0} from {1}", combo, sender.gameObject.name));
	}

	void onScoreIncrease(MinigameBase sender, int delta) {
#if SEND_EVENTS_ON_SCORE_CHANGE
		int gameIdx = games.IndexOf(sender);
		int nextGameIdx = (gameIdx + 1) % games.Count;
		games[nextGameIdx].onGoodEvent(delta);
#endif
	}

	void onScoreDecrease(MinigameBase sender, int delta) {
#if SEND_EVENTS_ON_SCORE_CHANGE
		int gameIdx = games.IndexOf(sender);
		int nextGameIdx = (gameIdx + 1) % games.Count;
		games[nextGameIdx].onBadEvent(delta);
#endif
	}
}
