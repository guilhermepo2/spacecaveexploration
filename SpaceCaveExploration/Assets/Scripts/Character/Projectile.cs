using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    [Header("Projectile Configurations")]
    public LayerMask CollisionLayers;
    public float TimeToDestroy = 10.0f;
    public float Velocity;
    private Vector3 m_Direction;
    private CircleCollider2D m_collider;

    private void Start() {
        m_collider = GetComponent<CircleCollider2D>();
        StartCoroutine(DestroyRoutine());
    }

    private IEnumerator DestroyRoutine() {
        yield return new WaitForSeconds(10.0f);
        Destroy(this.gameObject);
    }

    private void Update() {
        transform.Translate(m_Direction * Time.deltaTime);

        Collider2D Collision = Physics2D.OverlapCircle(transform.position, m_collider.radius, CollisionLayers);
        
        if (Collision != null) {
            HealthComponent _h = Collision.GetComponent<HealthComponent>();

            if (_h != null) {
                // TODO: Varying damage...
                _h.DealDamage(1);
            }
        }
    }

    public void SetDirection(Vector3 Direction) {
        m_Direction = Velocity * Direction;
    }
}
