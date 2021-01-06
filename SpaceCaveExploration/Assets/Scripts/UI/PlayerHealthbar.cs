using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthbar : MonoBehaviour {
    public Slider HealthbarDisplay;
    private float m_CurrentHealth;
    private float m_MinimumHealth = 0;
    private float m_MaxHealth;
    private HealthComponent m_PlayerHealthComponent;

    private float m_LowHealth;
    private float m_MediumHealth;

    [Space]

    [Header("Healthbar Colors:")]
    public Color highHealthColor = new Color(0.35f, 1f, 0.35f);
    public Color mediumHealthColor = new Color(0.9450285f, 1f, 0.4481132f);
    public Color lowHealthColor = new Color(1f, 0.259434f, 0.259434f);

    private void Start() {
        m_PlayerHealthComponent = GameObject.Find("Player").GetComponent<HealthComponent>();
        m_MaxHealth = m_PlayerHealthComponent.MaxHealth;

        m_LowHealth = m_MaxHealth * 0.33f;
        m_MediumHealth = m_MaxHealth * 0.66f;

        HealthbarDisplay.minValue = m_MinimumHealth;
        HealthbarDisplay.maxValue = m_MaxHealth;

        UpdateHealth();
    }

    private void Update() {
        UpdateHealth();
    }

    private void UpdateHealth() {
        float CurrentHealth = m_PlayerHealthComponent.CurrentHealth;

        if(CurrentHealth < m_LowHealth) {
            ChangeHealthbarColor(lowHealthColor);
        } else if(CurrentHealth < m_MediumHealth) {
            ChangeHealthbarColor(mediumHealthColor);
        } else {
            ChangeHealthbarColor(highHealthColor);
        }

        HealthbarDisplay.value = CurrentHealth;
    }

    private void ChangeHealthbarColor(Color _color) {
        HealthbarDisplay.transform.Find("Bar").GetComponent<Image>().color = _color;
    }
}
