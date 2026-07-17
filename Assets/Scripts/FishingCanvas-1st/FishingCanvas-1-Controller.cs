using DG.Tweening;
using UnityEngine;

public class FishingCanvas1Controller : MonoBehaviour
{
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
        if (Input.GetKeyDown(KeyCode.Space) && Canvas1SpaceBarListener)
        {
            if(QualityBarTracker.QualityType == "Green")
            {
                Canvas1SpaceBarListener = false;

                ShowPerfect();
                print("Perfect-spaceBarPressed");
            }
            
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
        seq.Append(PerfectImageRect.DOAnchorPosY(215f, 0.3f).SetEase(EaseType));

        // Wait on screen for 0.3 second
        seq.AppendInterval(0.3f);

        // Move it to 316f
        seq.Append(PerfectImageRect.DOAnchorPosY(415f, 0.3f).SetEase(EaseType));

        // Turn it off when the entire sequence is complete
        seq.OnComplete(() => PerfectImage.SetActive(false));

        

    }



}
