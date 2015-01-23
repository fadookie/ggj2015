using UnityEngine;
using System.Collections;

public class SceneLoader : MonoBehaviour {

	#region Event declarations
	public delegate void SceneLoadEventHandler();
	public event SceneLoadEventHandler onScenesLoaded;
	#endregion

	public string[] sceneList = new string[] {
	};

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
}
