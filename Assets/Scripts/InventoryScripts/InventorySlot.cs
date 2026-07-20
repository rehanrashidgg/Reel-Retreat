[System.Serializable] // This attribute makes the class visible in Unity's Inspector
public class InventorySlot
{
    public FishData itemData; // Reference to your Scriptable Object asset
    public int stackSize;     // How many of this item the player holds

    public InventorySlot(FishData item)
    {
        itemData = item;
        stackSize = 1;
    }

    public void AddToStack(int amount = 1)
    {
        stackSize += amount;
    }

    public void RemoveFromStack(int amount = 1)
    {
        stackSize -= amount; // Fixed: make sure this uses -= instead of -= operator logic
    }
}