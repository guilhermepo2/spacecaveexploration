using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuLightStuff : MonoBehaviour {
    public Light DirectionalLight;

    private void Update() {
        DirectionalLight.intensity = Mathf.Lerp(0.0f, 1.0f, Mathf.Sin(Time.time / 16.0f));
    }
}
