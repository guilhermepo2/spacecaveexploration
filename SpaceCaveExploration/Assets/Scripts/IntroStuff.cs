using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroStuff : MonoBehaviour {
    public Text AdelaideOdyssey;
    public Text IntroTextRef;
    private bool PressedToContinue = false;
    private WaitForSeconds InternalBetweenLetters;

    private string AdelaideOdysseyString = "Adelaide's Odyssey";
    private string[] IntroTexts = {
        "The year is 26XX.\n\n",
        "The earth is running out of resources.Shuttles have been launched to the extremes of our galaxy to find sources of fossil fuels and water.\n\n",
        "Not every shuttle is successful, and not every pilot comes home.\n\n",
        "During a routine survey mission, your shuttle is impacted by a solar flare and crashes on an alien planet.\n\n",
        "The only way to escape is to find pieces of your ship and repair your module in order to leave the surface.\n\n",
        "It seems you are not alone here though. Dangers await you."
    };

    private void Start() {
        AdelaideOdyssey.text = "";
        IntroTextRef.text = "";
        InternalBetweenLetters = new WaitForSeconds(0.1f);
        StartCoroutine(PrintText());
    }

    private void Update() {
        PressedToContinue = Input.GetKeyDown(KeyCode.Return);
    }

    private IEnumerator PrintText() {
        for(int i = 0; i < AdelaideOdysseyString.Length; i++) {
            AdelaideOdyssey.text += AdelaideOdysseyString[i];
            yield return InternalBetweenLetters;
        }

        yield return InternalBetweenLetters;

        for(int i = 0; i < IntroTexts.Length; i++) {
            IntroTextRef.text = "";
            for(int j = 0; j < IntroTexts[i].Length; j++) {
                IntroTextRef.text += IntroTexts[i][j];
                yield return InternalBetweenLetters;
            }

            while(!PressedToContinue) { yield return null; }

            PressedToContinue = false;
        }

        yield return InternalBetweenLetters;

        // go to next scene...
        SceneManager.LoadScene("SampleLevel");
    }

}
