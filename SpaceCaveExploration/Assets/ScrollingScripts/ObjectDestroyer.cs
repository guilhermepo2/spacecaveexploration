using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour {
	// When a trigger hits us, destroy it
	void OnTriggerEnter (Collider other) {
		Destroy (other.gameObject);
	}
	// When a collider hits us, destroy it
	void OnCollisionEnter (Collision col) {
		GameObject other = col.collider.gameObject;
		Destroy (other);
	}
}
