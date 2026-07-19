using UnityEditor;
using UnityEngine;


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


    private RectTransform rectTransform;

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






}
