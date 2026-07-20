using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    // 1. The static instance variable that other scripts will call
    public static InventoryManager Instance { get; private set; }

    [Header("Inventory Data")]
    public List<InventorySlot> inventory = new List<InventorySlot>();

    private void Awake()
    {
        // 2. Enforce the Singleton pattern (only 1 can exist at a time)
        if (Instance != null && Instance != this)
        {
            // If a copy already exists (e.g. from reloading a scene), destroy this duplicate
            Destroy(gameObject);
            return;
        }

        // Assign the static reference to this object
        Instance = this;

        // 3. Keep this object alive across scene changes
        DontDestroyOnLoad(gameObject);
    }

    public void AddItem(FishData itemToAdd)
    {
        if (itemToAdd == null) return;

        // Check if we already have this Scriptable Object in our list
        InventorySlot existingSlot = inventory.Find(slot => slot.itemData == itemToAdd);

        if (existingSlot != null)
        {
            existingSlot.AddToStack(1);
            Debug.Log($"Stacked {itemToAdd.fishName}. Total: {existingSlot.stackSize}");
        }
        else
        {
            InventorySlot newSlot = new InventorySlot(itemToAdd);
            inventory.Add(newSlot);
            Debug.Log($"Added new fish: {itemToAdd.fishName}");
        }
    }

    public void RemoveItem(FishData itemToRemove)
    {
        InventorySlot existingSlot = inventory.Find(slot => slot.itemData == itemToRemove);

        if (existingSlot != null)
        {
            existingSlot.RemoveFromStack(1);
            if (existingSlot.stackSize <= 0)
            {
                inventory.Remove(existingSlot);
            }
        }
    }
}