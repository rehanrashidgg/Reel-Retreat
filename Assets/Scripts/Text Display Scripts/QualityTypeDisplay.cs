using TMPro;
using UnityEngine;

public class QualityTypeDisplay : MonoBehaviour
{

    private TextMeshProUGUI QualityTypeText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        QualityTypeText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        QualityTypeText.text = "Rarity Chances: " + QualityBarTracker.QualityType;
    }
}
