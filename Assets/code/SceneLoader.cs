using UnityEngine;
using System.Collections;

public class SceneLoader : MonoBehaviour {

	#region Event declarations
	public delegate void SceneLoadEventHandler();
	public event SceneLoadEventHandler onScenesLoaded;
	#endregion

	public string[] sceneList = new string[] {
	};

	public static string mainScene = "Main";
	public static string winScene = "WinScreen";
	public static string loseScene = "LoseScreen";

	public bool scenesLoaded = false;

	// Use this for initialization
	void Start () {
		if (Services.instance.Get<SceneLoader>() != null) {
			//Abort scene load as another scene loader is present
			gameObject.SetActive(false);
			return;
		}

		Services.instance.Set<SceneLoader>(this);

		foreach(string sceneName in sceneList) {
			Application.LoadLevelAdditive(sceneName);
		}
		scenesLoaded = true;
		if (onScenesLoaded != null) {
			onScenesLoaded();
		}
	}

	public static void loadMainScene() {
		Application.LoadLevel(mainScene);
	}

	public static void loadWinScene() {
		Application.LoadLevel(winScene);
	}

	public static void loadLoseScene() {
		Application.LoadLevel(loseScene);
	}
}
