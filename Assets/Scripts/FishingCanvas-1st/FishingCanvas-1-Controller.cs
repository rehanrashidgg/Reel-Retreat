using DG.Tweening;
using UnityEngine;
using System.Collections;

public class FishingCanvas1Controller : MonoBehaviour
{

    public GameObject FishingCanvas2;
    public GameObject PerfectImage;


    [SerializeField] private DG.Tweening.Ease EaseType;


    //Boolean Variables Below Used as switches.
    public static bool Canvas1SpaceBarListener = true;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) && Canvas1SpaceBarListener)
        {
            if(QualityBarTracker.QualityType == "Green")
            {
                //spacebarlistener gets true again in GameState.
                Canvas1SpaceBarListener = false;
                ShowPerfect();
                print("Perfect-spaceBarPressed");
                
            }
            ProgressBarUpdater.movingUp = true;

            //Show Second Fishing canvas
            StartCoroutine(ShowFishingCanvas2Delayed());
            



        }
    }

    //------------------------------------------------------------------------------------------------------



    public void ShowPerfect()
    {
        RectTransform PerfectImageRect = PerfectImage.GetComponent<RectTransform>();
        PerfectImage.SetActive(true);

        // Create a new sequence timeline
        Sequence seq = DOTween.Sequence();

        // Move it up to 150f
        seq.Append(PerfectImageRect.DOAnchorPosY(415f, 0.3f).SetEase(EaseType));

        // Wait on screen for 0.3 second
        seq.AppendInterval(0.3f);

        // Move it to 316f
        seq.Append(PerfectImageRect.DOAnchorPosY(630f, 0.3f).SetEase(EaseType));

        // Turn it off when the entire sequence is complete
        seq.OnComplete(() => PerfectImage.SetActive(false));

        

    }




    //Enables 2nd Canvas with 2 sec delay.
    public void showFishingCanvas2()
    {
        FishingCanvas2.SetActive(true);

        //sets movingUp to true cuz i set it to false when i disabled the (GameState hidecanvas2nd function).
        ProgressBarUpdater.movingUp = true;
        //****************************************************************************************************
    }



    public IEnumerator ShowFishingCanvas2Delayed()
    {

        yield return new WaitForSeconds(2.0f);


        showFishingCanvas2();
    }
    //*************************************.

}
