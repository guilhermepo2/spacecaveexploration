using UnityEngine;
using UnityEngine.UI;

public class EndLevelUI : MonoBehaviour {
    public Text ScreenText;

    public void GotChest() {
        ScreenText.text = "You found Ezra's gun on the ground.\nIt seems that he went through here first but is now defenseless.\n\nYou have to go help him.";
    }
}
