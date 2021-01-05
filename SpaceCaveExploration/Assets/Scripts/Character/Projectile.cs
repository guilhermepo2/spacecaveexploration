using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    [Header("Projectile Configurations")]
    public float TimeToDestroy = 10.0f;
    public float Velocity;
    private Vector3 m_Direction;

    private void Start() {
        StartCoroutine(DestroyRoutine());
    }

    private IEnumerator DestroyRoutine() {
        yield return new WaitForSeconds(10.0f);
        Destroy(this.gameObject);
    }

    private void Update() {
        transform.Translate(m_Direction * Time.deltaTime);
    }

    public void SetDirection(Vector3 Direction) {
        m_Direction = Velocity * Direction;
    }
}
