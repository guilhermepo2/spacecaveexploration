using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    private PlayerController m_PlayerReference;

    public Text BlueJarAmount;
    public Text RedJarAmount;
    public Text GreenJarAmount;
    public Text ChestAmount;

    private void Awake() {
        m_PlayerReference = GameObject.Find("Player").GetComponent<PlayerController>();
        m_PlayerReference.OnItemCollected += UpdateUINumbers;
    }

    private void Start() {
        UpdateUINumbers(0, 0, 0, 0);
    }

    public void UpdateUINumbers() {
        UpdateUINumbers(
            m_PlayerReference.BlueJarCount,
            m_PlayerReference.RedJarCount,
            m_PlayerReference.GreenJarCount,
            m_PlayerReference.ChestCount
            );
    }

    public void UpdateUINumbers(int _b, int _r, int _g, int _c) {
        BlueJarAmount.text = _b.ToString();
        RedJarAmount.text = _r.ToString();
        GreenJarAmount.text = _g.ToString();
        ChestAmount.text = _c.ToString();
    }
}
