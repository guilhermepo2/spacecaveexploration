using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevelOnTrigger : MonoBehaviour {
    public string LevelName;

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player") {
            SceneManager.LoadScene(LevelName);
        }
    }
}
