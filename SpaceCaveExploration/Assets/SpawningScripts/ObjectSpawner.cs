using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour {

	[SerializeField]
	[Tooltip ("These are the Transforms that this script is allowed to spawn.")]
	Transform[] spawnables;
	[SerializeField]
	[Tooltip ("How long on average between spawns of the related Transform in \"spawnables[]\".")]
	float[] cooldowns;
	[SerializeField]
	[Tooltip ("Each spawnable and cooldown needs its own timer. This array stores those timers.")]
	float[] timers;

	[SerializeField]
	[Tooltip (
		"The \"wiggle room\" around each spawnable's cooldown time.\n" +
		"A range of 0 yields no variety between spawntimes.")]
	float randomizerRange = 0;

	public Transform spawnBoundary1;
	public Transform spawnBoundary2;

	[SerializeField]
	[Tooltip ("This should be true for all objects that don't move on their own.")]
	private bool followScrolling = false;

	void Start () {
		// Make sure all arrays are the same length
		if (spawnables.Length != cooldowns.Length || spawnables.Length != timers.Length) {
			Debug.LogWarning ("The arrays in ObjectSpawner do not match lengths. Fix this.");
		}
		// Reset all timers to zero
		for (int i = 0; i < timers.Length; i++) {
			timers [i] = 0;
		}
	}

	void Update () {
		// Increment and check all timers
		for (int i = 0; i < timers.Length; i++) {
			// Increase the timers
			if (followScrolling) {
				ObjectScroller objScrl = GameObject.FindObjectOfType<ObjectScroller> ();
				float scrollSpeedPercent = objScrl.GetScrollSpeed () / objScrl.GetRegularScrollSpeed ();
				timers [i] += Time.deltaTime * scrollSpeedPercent * (objScrl.GetScrollSpeed () / 3);
			} else {
				timers [i] += Time.deltaTime;
			}
			// If the timer has passed the cooldown
			if (timers [i] >= cooldowns [i]) {
				// Reset the timer randomly in a range of size "randomizerRange" centered on zero
				timers [i] = Random.Range (-randomizerRange / 2, randomizerRange / 2);
				// This is the position the newly spawned object will be instantiated at.
				Vector3 spawnPos = transform.position;
				// If both spawnBoundaries are valid
				if (spawnBoundary1 != null && spawnBoundary2 != null) {
					// Set spawnPos to a random point between them
					// If one or both spawnBoundaries are null, the
					// spawnPos will default back to the ObjectSpawner's position
					spawnPos = new Vector3 (
						                  Random.Range (spawnBoundary1.position.x, spawnBoundary2.position.x),
						                  Random.Range (spawnBoundary1.position.y, spawnBoundary2.position.y),
						                  Random.Range (spawnBoundary1.position.z, spawnBoundary2.position.z));
				}
				// Grab the spawnable we should spawn and store it for cleaner access
				Transform spawn = spawnables [i];
				// Grab the enemy script from the spawnable if it exists
				_EnemyBase enemy = spawn.GetComponent<_EnemyBase> ();
				// Grab the pickup script from the spawnable if it exists
				_PickupBase pickup = spawn.GetComponent<_PickupBase> ();
				// Set the parent of the object to "Misc"
				// This helps keep the heirarchy clean
				Transform parent = GameObject.Find ("Dynamic/Misc").transform;
				if (enemy != null) {
					// If the spawnable is an enemy, set its parent to "Enemies"
					parent = GameObject.Find ("Dynamic/Enemies").transform;
				} else if (pickup != null) {
					// If the spawnable is a pickup, set its parent to "Pickups"
					parent = GameObject.Find ("Dynamic/Pickups").transform;
				}
				// Finally instantiate the object
				Instantiate (spawn, spawnPos, transform.rotation, parent);
			}
		}

	}
}