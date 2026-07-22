using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour
{


    //ui variables
    public GameObject FishingCanvas1st;
    public GameObject FishingCanvas2nd;
    //public GameObject FishCaughtCanvas;
    public GameObject InventoryCanvas;


    //used this to block player camera and movements while fishing.
    public PlayerMovement playerMovement;


    public bool PlayerInsideFishingZone = false;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //HideUi();
    }

    // Update is called once per frame
    void Update()
    {

        //opens the inventory if player press B
        if (Input.GetKeyDown(KeyCode.B)) 
        {
            InventoryCanvas.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            InventoryCanvas.SetActive(false);
        }


        // checks if player pressed E in fishing zone or not and show ui

        if (Input.GetKeyDown(KeyCode.E) && PlayerInsideFishingZone)
        {
            ShowFishingCanvas1st();
            print("Epressed Inside Zone");
        }
        else if (Input.GetKeyDown(KeyCode.E) && !PlayerInsideFishingZone)
        {
            print("Player is not in fishing zone.");
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            print("Epressed");
        }

        //**************************************************************


        if (ProgressBarUpdater.movingUp == false)
        {
            HideFishingCanvas1st();
            HideFishingCanvas2nd();

            //FishCaughtCanvas.SetActive(true);

            //print("BOTH UIs DISABLED");
        }




    }



    //enable and disable the first Canvas.

    void ShowFishingCanvas1st()
    {
        FishingCanvas1st.SetActive(true);

        playerMovement.IsFrozen = true;
    }



    void HideFishingCanvas1st()
    {
        QualityBarTracker.stopMoving = false;
        FishingCanvas1st.SetActive(false);
        FishingCanvas1Controller.Canvas1SpaceBarListener = true;
    }

    //************************************.


    // Disables the 2ndcanvas and resets all its variable and score for next fishing
    void HideFishingCanvas2nd()
    {
        FishingCanvas2nd.SetActive(false);

        ProgressBarUpdater.FishCaught = false;
        ProgressBarUpdater.FishEscaped = false;
        ProgressBarUpdater.RopeBroke = false;

        ProgressBarUpdater.movingUp = true;

        ScoreUpdater.progressScore = 0;


        //turned IsFrozen to false so player movement and look controls regain after last canvas is disabled.
        if(FishCaughtCanvas.IsEnabled == true)
        {
            return;
        }
        else
        {
            playerMovement.IsFrozen = false;
        }
       



    }
    //*******************************************************************************






    //Checks if the player is in fishing Zone and toggles the PlayerInsideFishingZone to true
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FishingZone"))
        {

            PlayerInsideFishingZone = true;
            print("Player Entered Fishing Zone");

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("FishingZone"))
        {

            PlayerInsideFishingZone = false;
            print("Player Left Fishing Zone");

            print("OnTriggerExit fired by: " + other.gameObject.name);
        }
    }
















}
