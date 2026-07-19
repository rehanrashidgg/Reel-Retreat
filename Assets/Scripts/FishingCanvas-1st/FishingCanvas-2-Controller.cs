using UnityEngine;
using System.Collections;
using UnityEditor;

public class FishingCanvas2Controller : MonoBehaviour
{

    //ui variables
    public GameObject FishingCanvas1st;
    public GameObject FishCaughtCanvas;


    public static bool FishingStatus = false;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    //Show Ui logic with delay. call the showuidelayed if u want delay.

    void ShowUi()
    {
        //FishingCanvas2nd.SetActive(true);
        FishingStatus = true;
    }

    private IEnumerator ShowUiDelayed()
    {
        yield return new WaitForSeconds(2.0f);

        ShowUi();
    }

    //****************************************************************.










    void HideUi()
    {

        FishingStatus = false;
        //stopMoving = false;
        ScoreUpdater.progressScore = 0f;
        ProgressBarUpdater.movingUp = true;
        ProgressBarUpdater.FishCaught = false;
        ProgressBarUpdater.RopeBroke = false;
        ProgressBarUpdater.FishEscaped = false;

        //FishingCanvas2nd.SetActive(false);
    }

}
