using UnityEngine;

public class DistanceBarUpdater : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

// checks if the progressIndicator is in triggerbox and moves the distanceIndicator till score reaches 100.

        if (ScoreUpdater.progressStatus)
        {
            transform.Translate(Vector3.up * 0.4f * Time.deltaTime);
            if(ScoreUpdater.progressScore >= 100)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, 1f, transform.localPosition.z);
            }
        }

//*********************************************************************************************************.
        
    }
}
