using UnityEngine;

public class QualityBarTracker : MonoBehaviour
{
    public float speed = 5;
    private RectTransform rectTransform;
    private bool movingRight = true;
    private bool stopMoving = false;

    public static string QualityType;

   


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //HideUi();
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


    }



    public void OnTriggerStay2D(Collider2D other)
    {
       
        qualityBarInput(other);
    }

    void qualityBarInput(Collider2D other)
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

            

            //StartCoroutine(ShowUiDelayed());

        }
    }

}
