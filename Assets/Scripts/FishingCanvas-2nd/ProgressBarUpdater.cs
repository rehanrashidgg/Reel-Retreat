using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class ProgressBarUpdater : MonoBehaviour
{
    public float speed = 100f;
    public float pullSpeed = 200f;

    public float minY = -200f; // Bottom limit of your progress bar
    public float maxY = 200f;  // Top limit of your progress bar

    public static bool movingUp = true;

    //bools for status of fishing for DisplayDistance
    public static bool FishEscaped = false;
    public static bool FishCaught = false;
    public static bool RopeBroke = false;



    //Variables for FishCaughtCanvas to display recently caught fish
    public GameObject FishCaughtCanvas;

    public static Sprite fishImage;
    public static string fishName;
    public static string fishRarity;
    public static int fishPrice;
    public static float fishWeight;
    //---------------------------------------------------------------



    private RectTransform rectTransform;


    //Variable for Fishing Reward function
    public FishingRewardManager rewardManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {

    // Moves Progress indicator Upwards till the top end of progressBar and stops. and also stops if score is 100.

        if (movingUp) {

            rectTransform.anchoredPosition += new Vector2(0, -speed * Time.deltaTime);

            if(rectTransform.anchoredPosition.y <= minY)
            {
                rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, minY);
                movingUp = false;
                FishEscaped = true;
                print("Fish Escaped Because You Did'nt Pull");

                //reset the position of indicator for next fishing loop
                rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, 0f);

            }

            if(rectTransform.anchoredPosition.y >= maxY)
            {
                rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, maxY);
                movingUp = false;
                RopeBroke = true;
                print("Rope Broke Because You Pulled Too Hard");

                //reset the position of indicator for next fishing loop
                rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, 0f);
            }

            if(ScoreUpdater.progressScore >= 100f)
            {
                movingUp = false;
                FishCaught = true;
                print("YOU CAUGHT THE FISH");

                FishCaughtSuccess();

                FishCaughtCanvas.SetActive(true);


                //reset the position of indicator for next fishing loop
                rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, 0f);
            }

        }

        //***********************************************************


        // Moves the Indicator downwards while u hold the space key till the bottom end of progressBar or score is less than 100 and then stops.

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (rectTransform.anchoredPosition.y < maxY && ScoreUpdater.progressScore <= 100f && movingUp)
            {

                rectTransform.anchoredPosition += new Vector2(0, pullSpeed * Time.deltaTime);

            }
        }

        //******************************************************************************************************************.





    }




    //Runs the FishingRewardManager Script and Get random fish data from database

    private void FishCaughtSuccess()
    {
        

        // run the function from FishingRewardManager to get random fish from database
        FishData caughtFish = rewardManager.RollRandomFish();

        if (caughtFish != null)
        {
            // You now have access to data and its sprite directly from the Scriptable Object!
            Debug.Log($"Caught a {caughtFish.fishName}! Weight: {caughtFish.weight}, Sprite: {caughtFish.fishSprite.name}");

            //updates the variables for FishCaughtCanvas
            fishImage = caughtFish.fishSprite;
            fishName = caughtFish.fishName;
            fishRarity = caughtFish.rarity;
            fishPrice = caughtFish.price;
            fishWeight = caughtFish.weight;

            //adds the caught fish in ur inventory
            InventoryManager.Instance.AddItem(caughtFish);
            
        }

        // Call GameState to hide the screens
        //FindFirstObjectByType<GameState>().HideFishingCanvas2nd();
    }

    //*****************************************************************************


}
