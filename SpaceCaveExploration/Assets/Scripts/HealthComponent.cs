using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour {
    [Header("Health Configurations")]
    public int MaxHealth;
    private int m_CurrentHealth;
    public int CurrentHealth { get { return m_CurrentHealth; } }
    public float InvencibilityTime = 0.2f;
    private float m_InvincibilityTimeRemaining;

    private void Start() {
        m_CurrentHealth = MaxHealth;
    }

    private void Update() {
        m_InvincibilityTimeRemaining -= Time.deltaTime;
    }

    public void DealDamage(int _Damage) {
        if(m_InvincibilityTimeRemaining <= 0.0f) {
            m_InvincibilityTimeRemaining = InvencibilityTime;
            Debug.Log($"{gameObject.name} taking damage");

            m_CurrentHealth -= _Damage;

            if(m_CurrentHealth <= 0) {
                Die();
            }
        }
    }

    private void Die() {
        // explode
        Destroy(this.gameObject);
    }
}
