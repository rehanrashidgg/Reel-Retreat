using System.Collections;
using UnityEditor;
using UnityEngine;

public class QualityBarTracker : MonoBehaviour
{
    public float speed = 5;
    private RectTransform rectTransform;
    private bool movingRight = true;
    private bool stopMoving = false;

    public static string QualityType;


    //ui variables
    public GameObject FishingCanvas1st;
    public static bool FishingStatus = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HideUi();
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!stopMoving)
        {
            if (movingRight && rectTransform.anchoredPosition.x >= 100f)
            {
                movingRight = false;
            }
            else if (!movingRight && rectTransform.anchoredPosition.x <= -100f)
            {
                movingRight = true;
            }

            float direction = movingRight ? 1f : -1f;

            rectTransform.anchoredPosition += new Vector2(direction * speed * Time.deltaTime, 0);
        }


        if (Input.GetKey(KeyCode.R))
        {
            print("Rpressed");
            HideUi();
            //StartCoroutine(ShowUiDelayed());
        }


    }



    public void OnTriggerStay2D(Collider2D other)
    {
        if (!stopMoving && Input.GetKey(KeyCode.Space))
        {
            
            if (other.CompareTag("Green"))
            {
                print("Green");
                stopMoving = true;
                QualityType = "Green";
            }
            else if (other.CompareTag("Yellow"))
            {
                print("Yellow");
                stopMoving = true;
                QualityType = "Yellow";
            }
            else if (other.CompareTag("Red"))
            {
                print("Red");
                stopMoving = true;
                QualityType = "Red";
            }
            else if (other.CompareTag("Black-Trash"))
            {
                print("Black Trash");
                stopMoving = true;
                QualityType = "Black-Trash";
            }
            StartCoroutine(ShowUiDelayed());
            
        }
    }

//Show Ui logic with delay. call the showuidelayed if u want delay.

    void ShowUi()
    {
        FishingCanvas1st.SetActive(true);
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
        FishingCanvas1st.SetActive(false);
        FishingStatus = false;
        stopMoving = false;
        ScoreUpdater.progressScore = 0f;
        ProgressBarUpdater.movingUp = true;
        ProgressBarUpdater.FishCaught = false;
        ProgressBarUpdater.RopeBroke = false;
        ProgressBarUpdater.FishEscaped = false;
    }
}
