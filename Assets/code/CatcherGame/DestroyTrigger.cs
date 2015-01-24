using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class DestroyTrigger : MonoBehaviour {
	
	void OnTriggerEnter(Collider other) {
		other.gameObject.SetActive(false);
		Destroy(other.gameObject);
		FallingTarget target = other.GetComponent<FallingTarget>();
		if (target != null && target.targetType == FallingTarget.TargetType.ShouldCatch) {
			Services.instance.Get<CatcherGame>().Score--;
		}
	}
	
}
