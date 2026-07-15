using UnityEngine;
using TMPro;

public class DisplayDistance : MonoBehaviour
{
    private TextMeshProUGUI scoreText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText =  GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        float subtractedScore = 100f - ScoreUpdater.progressScore;

        int distanceRemaining = Mathf.RoundToInt(subtractedScore);


        if (ProgressBarUpdater.FishCaught)
        {
            scoreText.text = "YOU CAUGHT THE FISH";
        }
        else if (ProgressBarUpdater.FishEscaped)
        {
            scoreText.text = "Fish Escaped Because You Did'nt Pull";
        }
        else if (ProgressBarUpdater.RopeBroke)
        {
            scoreText.text = "Rope Broke Because You Pulled Too Hard";
        }
        else if (distanceRemaining != 0)
        {
            scoreText.text = "Distance Remaining: " + distanceRemaining + "Meters";
        }
    }
}
