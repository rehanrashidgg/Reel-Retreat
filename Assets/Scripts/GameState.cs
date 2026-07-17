using UnityEngine;

public class GameState : MonoBehaviour
{

    public GameObject FishingCanvas1st;

    public static bool FishingCanvas2ndStatus = false;


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
        FishingCanvas1st.SetActive(true);
        FishingCanvas2ndStatus = true;

    }

    //**********************************************.



    //Disable the canvas and resets the progressScore to 0  and set fishing status to false.

    void HideUi()
    {
        FishingCanvas1st.SetActive(false);
        FishingCanvas2ndStatus = false;
    }

    //**************************************************************************************.


}
