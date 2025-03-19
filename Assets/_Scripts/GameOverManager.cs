using UnityEngine;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Reference to the Score UI Text

    private void Start()
    {
        // Retrieve the stored score and update the UI
        int finalScore = PlayerPrefs.GetInt("PlayerScore", 0);
        scoreText.text = "" + finalScore;
    }
}
