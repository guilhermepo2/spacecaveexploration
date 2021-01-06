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
    private SpriteRenderer m_SpriteRenderer;


    private void Awake() {
        m_SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

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
            } else {
                StartCoroutine(InvincibilityFlash());
            }
        }
    }

    private IEnumerator InvincibilityFlash() {

        while(m_InvincibilityTimeRemaining > 0) {
            m_SpriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.05f);
            m_SpriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.05f);
        }

        m_SpriteRenderer.enabled = true;
        yield return null;

    }

    private void Die() {
        // explode
        Destroy(this.gameObject);
    }
}
