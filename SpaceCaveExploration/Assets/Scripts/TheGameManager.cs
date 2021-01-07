using UnityEngine;
using System.Collections;

public class TheGameManager : MonoBehaviour {
    private void Start() {
        FindObjectOfType<Timer>().OnTimeIsOver += GameOverGetGood;
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
