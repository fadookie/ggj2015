using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameEventBus : MonoBehaviour {
	const int DEFAULT_NUM_GAMES = 2;
	public MinigameBase[] initialGames = new MinigameBase[DEFAULT_NUM_GAMES]; //Size may change in the editor
	List<MinigameBase> games;

	// Use this for initialization
	void Start () {
		games = new List<MinigameBase>(initialGames);
		foreach(MinigameBase game in games) {
			subscribeToGame(game);
		}

		Services.instance.Set<GameEventBus>(this);
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
