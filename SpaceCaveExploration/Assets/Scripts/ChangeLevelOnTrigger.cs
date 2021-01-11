using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevelOnTrigger : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player") {
            if(SceneManager.GetActiveScene().name == "Tutorial Level") {
                SceneManager.LoadScene("SampleLevel");
            } else if(SceneManager.GetActiveScene().name == "SampleLevel" && FindObjectOfType<TheGameManager>().IsWinConditionMet()) {
                SceneManager.LoadScene("EndLevel");
            } else if(SceneManager.GetActiveScene().name == "EndLevel") {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
