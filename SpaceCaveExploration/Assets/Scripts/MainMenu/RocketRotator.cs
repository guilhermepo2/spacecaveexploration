using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketRotator : MonoBehaviour {
    public float RotationVelocity = 10.0f;
    public float UpAndDownVelocity = 0.25f;
    public float HowMuchUpAndDown = .25f;

    private Vector3 m_OriginalPosition;

    private void Start() {
        m_OriginalPosition = transform.position;
    }

    private void Update() {
        transform.RotateAround(transform.position, Vector3.up, RotationVelocity * Time.deltaTime);

        transform.position = new Vector3(
            m_OriginalPosition.x,
            m_OriginalPosition.y + (Mathf.Sin(Time.time * UpAndDownVelocity) * HowMuchUpAndDown),
            m_OriginalPosition.z
            );
    }
}
