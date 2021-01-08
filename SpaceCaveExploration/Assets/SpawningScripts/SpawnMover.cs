using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMover : MonoBehaviour {
	#pragma warning disable 0649
	[SerializeField] private Transform moveBound1, moveBound2;
	private Vector3 minBound, maxBound;

	private Transform spawnBound1, spawnBound2;

	private Vector3 spawnDist, maxSpawnDist;

	private Transform player;
	[SerializeField] private float boundScale = 1;
	[SerializeField] private float shrinkSpeed = 1;

	private ObjectSpawner spawner;

	[SerializeField] private bool xB, yB, zB;

	// Use this for initialization
	void Start () {
		spawner = GetComponent<ObjectSpawner> ();
		player = GameObject.FindObjectOfType<PlayerStats> ().transform;
		spawnBound1 = spawner.spawnBoundary1;
		spawnBound2 = spawner.spawnBoundary2;
		maxSpawnDist = spawnBound1.position - transform.position;
		spawnDist = maxSpawnDist;
	}
	
	// Update is called once per frame
	void Update () {
		FollowPlayer ();
		MoveBounds ();
		ShrinkBounds ();
	}

	private void FollowPlayer () {
		if (xB && yB && zB) {
			transform.position = player.position;
		} else {
			if (xB) {
				transform.position = new Vector3 (player.position.x, transform.position.y, transform.position.z);
			}
			if (yB) {
				transform.position = new Vector3 (transform.position.x, player.position.y, transform.position.z);
			}
			if (zB) {
				transform.position = new Vector3 (transform.position.x, transform.position.y, player.position.z);
			}
		}
	}

	private void MoveBounds () {
		CalculateBounds ();
		spawnBound1.position = transform.position + spawnDist;
		spawnBound2.position = transform.position - spawnDist;
		ClampBound (spawnBound1);
		ClampBound (spawnBound2);
	}

	private void CalculateBounds () {
		// Grab our boundary positions
		Vector3 bound1Pos = moveBound1.position;
		Vector3 bound2Pos = moveBound2.position;
		// Calculate our maximum and minimum limits on movement
		minBound = Vector3.Min (bound1Pos, bound2Pos);
		maxBound = Vector3.Max (bound1Pos, bound2Pos);
	}

	private void ClampBound (Transform t) {
		float x = transform.position.x;
		float y = transform.position.y;
		float z = transform.position.z;
		// Grab our position as individual floats
		if (xB)
			x = Mathf.Clamp (t.position.x, minBound.x, maxBound.x);
		if (yB)
			y = Mathf.Clamp (t.position.y, minBound.y, maxBound.y);
		if (zB)
			z = Mathf.Clamp (t.position.z, minBound.z, maxBound.z);
		// Clamp our position inside of our boundaries
		t.position = new Vector3 (x, y, z);
	}

	private void ShrinkBounds () {
		if (player.GetComponent<Rigidbody> ().IsSleeping ()) {
			boundScale -= Time.deltaTime * shrinkSpeed;
		} else {
			boundScale += Time.deltaTime * shrinkSpeed;
		}
		boundScale = Mathf.Clamp (boundScale, 0.1f, 1f);
		spawnDist = maxSpawnDist * (boundScale);

	}
}
