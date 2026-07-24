using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MainCanvasManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI WalletText;
    [SerializeField] private TextMeshProUGUI ObjectiveText;

 

    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateWallet();

        UpdateObjective();
    }



    //updates player money in main canvas
    void UpdateWallet()
    {
        WalletText.text = $"${GameState.Wallet}";
    }




    //Updates the objectives on MainCanvas
    void UpdateObjective()
    {
        if (InventoryManager.Instance.inventory.Count != 0)
        {
            ObjectiveText.text = "Go To The Market And Sell The Fishes In Your Inventory";
        }
        if(InventoryManager.Instance.inventory.Count == 0)
        {
            ObjectiveText.text = "Go to the Fishing Dock and try to catch some Fishes";
        }
    }
}
