using UnityEngine;

public class FishingController : MonoBehaviour
{
    public GameObject QualityUiPanel;
    public static bool QualityPanelStatus = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HideUi();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            ShowUi();
        }
    }



//enable the canvas and set fishing status true.

    void ShowUi()
    {
        QualityUiPanel.SetActive(true);
        QualityPanelStatus=true;

    }

//**********************************************.



//Disable the canvas and resets the progressScore to 0  and set fishing status to false.

    void HideUi()
    {
        QualityUiPanel.SetActive(false);
        QualityPanelStatus = false;

        ScoreUpdater.progressScore = 0f;
    }

//**************************************************************************************.


}
