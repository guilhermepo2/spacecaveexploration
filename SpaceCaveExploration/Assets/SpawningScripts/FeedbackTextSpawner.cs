using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackTextSpawner : MonoBehaviour {

	[SerializeField] HouseFeedbackText hft;

	public void SpawnNewText (string _string) {
		HouseFeedbackText instance = Instantiate (hft, transform.position, transform.rotation);
		instance.pos = transform.position;
		instance.text.text = _string;
	}

	void Spawn (string _string) {

	}
}
