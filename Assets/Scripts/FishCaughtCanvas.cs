using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class FishCaughtCanvas : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image fishImage;
    [SerializeField] private TextMeshProUGUI fishNameText;
    [SerializeField] private TextMeshProUGUI fishPriceText;
    [SerializeField] private TextMeshProUGUI fishRarityText;
    [SerializeField] private TextMeshProUGUI fishWeightText;

    //used this variable to unlock player movement once this canvas is disabled
    public PlayerMovement playerMovement;


    //used this variable in GameState to control "IsFrozen"
    public static bool IsEnabled = false;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

private void OnEnable()
{
    

    SetDataOnCanvas();

    IsEnabled = true;
        
    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;

    
}

private void OnDisable()
{
    IsEnabled = false;

    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;

    playerMovement.IsFrozen = false;
}

    public void SetDataOnCanvas()
    {

        fishImage.sprite = ProgressBarUpdater.fishImage;
        fishNameText.text = ProgressBarUpdater.fishName;
        fishPriceText.text = $"Price: ${ProgressBarUpdater.fishPrice}";
        fishRarityText.text = $"Rarity: {ProgressBarUpdater.fishRarity}";
        fishWeightText.text = $"Weight: {ProgressBarUpdater.fishWeight}Kg";
    }

}
