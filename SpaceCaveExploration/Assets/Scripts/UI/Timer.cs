using System;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    public Text TimerText;
    public float LevelTime = 180;
    private float m_TimeRemaining;

    public event Action OnTimeIsOver;

    private void Start() {
        m_TimeRemaining = LevelTime;
    }

    private void Update() {
        m_TimeRemaining -= Time.deltaTime;
        int TimeCeil = Mathf.CeilToInt(m_TimeRemaining);
        TimerText.text = TimeCeil.ToString();

        if(TimeCeil <= 0) {
            if(OnTimeIsOver != null) {
                OnTimeIsOver();
            }
        }
    }
}
