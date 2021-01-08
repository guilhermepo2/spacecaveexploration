using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollableObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// Add ourselves to the list of Transforms that the ObjectScroller will move
		ObjectScroller objectScroller = FindObjectOfType<ObjectScroller> ();
		if (objectScroller != null)
			objectScroller.AddToList (transform);
	}
}
