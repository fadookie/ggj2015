using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class DestroyTrigger : MonoBehaviour {
	
	void OnTriggerEnter(Collider other) {
		other.gameObject.SetActive(false);
		Destroy(other.gameObject);
		Services.instance.Get<CatcherGame>().Score--;
	}
	
}
