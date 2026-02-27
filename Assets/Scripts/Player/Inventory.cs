using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int inventorySize = 20;
    [SerializeField] private List<InventorySlot> slots = new();

    public List<InventorySlot> Slots => slots;

    private void Awake()
    {
        // Can improve by using dict to track stackable items
        for (int i = slots.Count; i < inventorySize; i++)
        {
            slots.Add(new InventorySlot());
        }
    }

    public bool AddItem(ItemData newItem, int quantity = 1)
    {
        // Try to stack the item first
        foreach (var slot in slots)
        {
            if (slot.CanStack(newItem))
            {
                slot.quantity += quantity;
                return true;
            }
        }

        // If not stackable or no existing stack, find an empty slot
        foreach (var slot in slots)
        {
            if (slot.IsEmpty)
            {
                slot.item = newItem;
                slot.quantity = quantity;
                return true;
            }
        }

        // Inventory is full
        Debug.Log("Inventory is full!");
        return false;
    }

    public bool RemoveItem(ItemData itemToRemove, int quantity = 1)
    {
        foreach (var slot in slots)
        {
            if (slot.item != null && slot.item.ID == itemToRemove.ID)
            {
                if (slot.quantity >= quantity)
                {
                    slot.quantity -= quantity;
                    if (slot.quantity == 0)
                    {
                        slot.item = null; // Clear the slot if quantity reaches zero
                    }
                    return true;
                }
                else
                {
                    Debug.Log("Not enough items to remove!");
                    return false;
                }
            }
        }

        Debug.Log("Item not found in inventory!");
        return false;
    }

    public bool SwapItem(int fromIndex, int toIndex)
    {
        // For non-UI caller
        if (fromIndex < 0 || fromIndex >= inventorySize || toIndex < 0 || toIndex >= inventorySize)
        {
            Debug.LogError("Invalid inventory slot index!");
            return false;
        }

        var temp = slots[fromIndex];
        slots[fromIndex] = slots[toIndex];
        slots[toIndex] = temp;
        return true;
    }
}
