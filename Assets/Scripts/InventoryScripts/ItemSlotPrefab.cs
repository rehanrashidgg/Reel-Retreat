using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlotPrefab : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image fishImage;
    [SerializeField] private TextMeshProUGUI fishNameText;
    [SerializeField] private TextMeshProUGUI fishPriceText;
    [SerializeField] private TextMeshProUGUI fishRarityText;
    [SerializeField] private TextMeshProUGUI fishCountText;

    public void SetSlotData(FishData data, int count)
    {
        if (data == null) return;

        // Set visual elements
        if (fishImage != null) fishImage.sprite = data.fishSprite;
        if (fishNameText != null) fishNameText.text = data.fishName;
        if (fishPriceText != null) fishPriceText.text = $"${data.price}";
        if (fishRarityText != null) fishRarityText.text = data.rarity.ToString();

        // Stack count badge logic
        if (fishCountText != null)
        {
            if (count > 1)
            {
                fishCountText.gameObject.SetActive(true);
                fishCountText.text = count.ToString();
            }
            else
            {
                fishCountText.gameObject.SetActive(false);
            }
        }
    }
}