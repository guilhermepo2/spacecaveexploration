using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactEffect : MonoBehaviour {
    private void Start() {
        // play sound
        StartCoroutine(DestroyAfter(0.3f));
    }

    private IEnumerator DestroyAfter(float Time) {
        yield return new WaitForSeconds(Time);
        Destroy(this.gameObject);
    }


}
