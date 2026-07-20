using UnityEngine;
using System.Collections.Generic;

public class FishingRewardManager : MonoBehaviour
{



    
    public List<FishData> fishDatabase = new List<FishData>();

    // Call this function when the score reaches 100
    public FishData RollRandomFish()
    {
        if (fishDatabase.Count == 0)
        {
            Debug.LogError("The Fish Database list is empty on FishRewardManager!");
            return null;
        }

        // Pick a completely random fish from your 20 assets
        int randomIndex = Random.Range(0, fishDatabase.Count);
        return fishDatabase[randomIndex];
    }












    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
