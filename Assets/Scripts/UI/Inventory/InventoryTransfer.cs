public static class InventoryTransfer
{
    // Centralized method to handle item transfers between inventories. This can be called from the UI or from event actions, etc.
    public static bool Transfer(Inventory fromInventory, int fromIndex, Inventory toInventory, int toIndex)
    {
        if (fromInventory == null || toInventory == null) return false;
        if (fromIndex < 0 || fromIndex >= fromInventory.Slots.Count) return false;
        if (toIndex < 0 || toIndex >= toInventory.Slots.Count) return false;

        if (fromInventory == toInventory && fromIndex == toIndex) return false; // No need to transfer if it's the same slot

        var fromSlot = fromInventory.Slots[fromIndex];
        var toSlot = toInventory.Slots[toIndex];

        ItemStack movingStack = fromSlot.Stack;
        if(movingStack.IsEmpty) return false; // No item to transfer(this shouldn't happen since the UI shouldn't allow dragging empty slots, but just in case)

        ItemStack remainingStack = toSlot.Place(movingStack);
        fromSlot.SetStack(remainingStack);
        fromInventory.InventoryChanged(); // Notify the from inventory that it has changed
        toInventory.InventoryChanged(); // Notify the to inventory that it has changed

        return true;
    }
}