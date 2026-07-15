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


        scoreText.text = "Distance Remaining: " + distanceRemaining + "Meters";

    }
}
