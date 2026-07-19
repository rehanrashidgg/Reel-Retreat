using UnityEngine;

public class ScoreUpdater : MonoBehaviour
{

    public static float progressScore = 0f;
    public static bool progressStatus = false;



// Updates score when the ProgressIndicator is inside the box collider  and set the progressStatus to True or false.

    public void OnTriggerStay2D(Collider2D other)
    {
        progressStatus = true;

        if (other.CompareTag("ScoreArea"))
        {
            
            if (progressScore < 100f)
            {
                
                progressScore += 10f * Time.deltaTime;
                print(progressScore);
            }
            
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        
        progressStatus =false;
    }

//***********************************************************************.




}
