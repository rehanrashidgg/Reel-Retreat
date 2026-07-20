using UnityEngine;

public class InventoryDisplayManager : MonoBehaviour
{
    [Header("UI Grid Settings")]
    [SerializeField] private Transform itemContainer; // Your MainWindow / Grid Panel
    [SerializeField] private GameObject slotPrefab;   // Your ItemSlotPrefab asset

    // Call this method whenever InventoryCanvas becomes active/visible
    private void OnEnable()
    {
        RefreshInventoryUI();
    }

    public void RefreshInventoryUI()
    {
        // 1. Clear existing slots to prevent duplicate stacks on re-opening
        foreach (Transform child in itemContainer)
        {
            Destroy(child.gameObject);
        }

        // 2. Guard check against empty inventory manager reference
        if (InventoryManager.Instance == null) return;

        // 3. Instantiate a UI slot prefab for every active InventorySlot entry
        foreach (var slotData in InventoryManager.Instance.inventory)
        {
            GameObject newSlotObj = Instantiate(slotPrefab, itemContainer);
            ItemSlotPrefab slotUI = newSlotObj.GetComponent<ItemSlotPrefab>();

            if (slotUI != null && slotData.itemData != null)
            {
                
                slotUI.SetSlotData(slotData.itemData, slotData.stackSize);
            }
        }
    }
}