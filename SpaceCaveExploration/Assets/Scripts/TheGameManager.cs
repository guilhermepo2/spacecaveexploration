using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TheGameManager : MonoBehaviour {
    PlayerController m_PlayerController;

    private void Start() {
        m_PlayerController = FindObjectOfType<PlayerController>();
        FindObjectOfType<Timer>().OnTimeIsOver += GameOverGetGood;
    }

    public bool IsWinConditionMet() {
        return (m_PlayerController.BlueJarCount == 3 &&
            m_PlayerController.GreenJarCount == 3 &&
            m_PlayerController.RedJarCount == 3);
    }

    public void GameOverGetGood() {
        Debug.Log("Game Is Over Lol");
        StartCoroutine(DealDamageToPlayerEverySecond());
    }

    private IEnumerator DealDamageToPlayerEverySecond() {
        yield return new WaitForSeconds(1.0f);
        GameObject PlayerReference = GameObject.Find("Player"); 
        
        if(PlayerReference != null) {
            PlayerReference.GetComponent<HealthComponent>().DealDamage(1);
            StartCoroutine(DealDamageToPlayerEverySecond());
        }
    }
}
