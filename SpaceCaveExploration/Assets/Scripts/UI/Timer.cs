using System;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    public Text TimerText;
    public float LevelTime = 180;
    private float m_TimeRemaining;
    private bool m_IsTimerStopped = false;
    public void StopTimer() { m_IsTimerStopped = true; }

    public event Action OnTimeIsOver;

    private void Start() {
        m_TimeRemaining = LevelTime;
    }

    private void Update() {
        if(m_TimeRemaining == 0.0f || m_IsTimerStopped) {
            return;
        }

        m_TimeRemaining -= Time.deltaTime;
        int TimeCeil = Mathf.CeilToInt(m_TimeRemaining);
        TimerText.text = TimeCeil.ToString();

        if(TimeCeil <= 0) {
            m_TimeRemaining = 0.0f;
            if(OnTimeIsOver != null) {
                OnTimeIsOver();
            }
        }
    }
}
