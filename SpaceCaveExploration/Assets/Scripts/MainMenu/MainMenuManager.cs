using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {
    public Text PlayText;
    public Text ExitText;
    public Text ArrowText;
    private float m_LeftOffset = 200.0f;
    private float m_HorizontalOffset = 10.0f;

    public enum ESelectableOptions {
        EPlay,
        EExit
    }
    private ESelectableOptions m_CurrentOption;

    private void Start() {
        m_CurrentOption = ESelectableOptions.EPlay;
    }

    private void Update() {
        switch(m_CurrentOption) {
            case ESelectableOptions.EPlay:
                ArrowText.rectTransform.position = new Vector3(
                    PlayText.rectTransform.position.x -  m_LeftOffset + (5.0f * Mathf.Sin(Time.time * 5.0f)),
                    PlayText.rectTransform.position.y + m_HorizontalOffset,
                    PlayText.rectTransform.position.z
                    );
                break;
            case ESelectableOptions.EExit:
                ArrowText.rectTransform.position = new Vector3(
                    ExitText.rectTransform.position.x - m_LeftOffset + (5.0f * Mathf.Sin(Time.time * 5.0f)),
                    ExitText.rectTransform.position.y + m_HorizontalOffset,
                    ExitText.rectTransform.position.z
                    );
                break;
        }

        if(Input.GetKeyDown(KeyCode.W) && m_CurrentOption == ESelectableOptions.EExit) {
            SoundManager.instance.PlayEffect(SoundBank.instance.UIMove);
            m_CurrentOption = ESelectableOptions.EPlay;
        }

        if(Input.GetKeyDown(KeyCode.S) && m_CurrentOption == ESelectableOptions.EPlay) {
            SoundManager.instance.PlayEffect(SoundBank.instance.UIMove);
            m_CurrentOption = ESelectableOptions.EExit;
        }

        if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) {
            SoundManager.instance.PlayEffect(SoundBank.instance.UISelect);
            if(m_CurrentOption == ESelectableOptions.EPlay) {
                Play();
            } else if(m_CurrentOption == ESelectableOptions.EExit) {
                Exit();
            }
        }
    }

    public void Play() { SceneManager.LoadScene("Intro"); }
    public void Exit() { Application.Quit(); }
}
