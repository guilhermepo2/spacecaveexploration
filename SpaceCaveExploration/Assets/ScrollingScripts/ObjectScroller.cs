using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScroller : MonoBehaviour {

	[SerializeField] 
	[Tooltip ("How fast will objects scroll across the screen?")]
	private float defaultScrollSpeed;
	private float scrollSpeed;
	[SerializeField] 
	[Tooltip("This Vector3 will be normalized, reseting its magnitude to 1 while keeping its direction.")]
	private Vector3 scrollDirection;
	// This list will store all objects that we should be scrolling.
	private List<Transform> tList = new List<Transform> ();

	void Start () {
		ResetScrollSpeed ();
	}

	// Update is called once per frame
	void Update () {
		
		// This is the list of objects we should remove from our tList
		List<Transform> removeList = new List<Transform> ();
		// Go through the entire tList
		foreach (Transform t in tList) {
			// If the transform in our list is null
			if (t == null) {
				// Add it to our remove list
				removeList.Add (t);
			} else {
				// If the transform is still there, scroll it!
				t.Translate (scrollDirection.normalized * scrollSpeed * Time.deltaTime, Space.World);
			}
		}
		// Take any Transforms that have been added to our removeList out of our tList
		foreach (Transform t in removeList) {
			if (tList.Contains (t)) {
				tList.Remove (t);
			}
		}
	}

	// This adds Transforms to our tList
	// This should be done to a scrollable object in either Start() or Awake()
	public void AddToList (Transform _transform) {
		tList.Add (_transform);
	}

	// This removes items from our tList
	// This should be done if the item has been destroyed
	public void RemoveFromList (Transform newT) {
		foreach (Transform t in tList) {
			if (t.Equals (newT)) {
				tList.Remove (t);
			}
		}
	}

	public void SetScrollSpeed (float speed) {
		scrollSpeed = speed;
	}

	public float GetScrollSpeed () {
		return scrollSpeed;
	}

	public float GetRegularScrollSpeed () {
		return defaultScrollSpeed;
	}

	public void ResetScrollSpeed () {
		scrollSpeed = defaultScrollSpeed;
	}
}
