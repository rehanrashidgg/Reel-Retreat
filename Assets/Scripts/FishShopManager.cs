using UnityEngine;

using TMPro;

public class FishShopManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI playerMoneyText;

    [SerializeField] private InventoryDisplayManager InventoryDisplayManager;

    //used this to block player camera and movements while fishing.
    public PlayerMovement playerMovement;




    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        // Update the money display every time we open the shop
        UpdateMoneyUI();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        playerMovement.IsFrozen = true;
    }

    private void OnDisable()
    {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerMovement.IsFrozen = false;
    }

    public void SellAllFish()
    {
        // 1. Safety check: Ensure inventory exists and isn't empty
        if (InventoryManager.Instance == null || InventoryManager.Instance.inventory.Count == 0)
        {
            Debug.Log("No fish to sell!");
            return;
        }

        int totalEarnings = 0;

        // 2. Loop through every slot in the inventory
        foreach (var slot in InventoryManager.Instance.inventory)
        {
            if (slot.itemData != null)
            {
                // Multiply the fish price by how many are in the stack
                totalEarnings += slot.itemData.price * slot.stackSize;
            }
        }

        // 3. Add the earnings to the player's wallet
        GameState.Wallet += totalEarnings;
        UpdateMoneyUI();

        // 4. Clear the inventory completely
        InventoryManager.Instance.inventory.Clear();

        InventoryDisplayManager.RefreshInventoryUI();

        Debug.Log($"Sold all inventory for ${totalEarnings}!");


    }

    private void UpdateMoneyUI()
    {
        if (playerMoneyText != null)
        {
            playerMoneyText.text = $"Wallet: ${GameState.Wallet}";
        }
    }
}