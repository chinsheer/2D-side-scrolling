using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public enum InventoryType
    {
        Backpack,
        Hotbar,
        Chest
    }
    [SerializeField] private int inventorySize = 20;
    [SerializeField] private List<InventorySlot> slots = new();
    [SerializeField] private InventoryType inventoryType;   

    public List<InventorySlot> Slots => slots;
    public InventoryType Type => inventoryType;

    public event Action OnInventoryChanged;

    public void InventoryChanged()
    {
        OnInventoryChanged?.Invoke();
    }

    private void Awake()
    {
        // Can improve by using dict to track stackable items
        for (int i = slots.Count; i < inventorySize; i++)
        {
            slots.Add(new InventorySlot());
        }
    }

    public bool AddItem(ItemStack newItemStack)
    {
        // Try to stack the item first
        foreach (var slot in slots)
        {
            if (slot.item == newItemStack.item)
            {
                ItemStack remaining = slot.Place(newItemStack);
                if (remaining.IsEmpty)
                {
                    OnInventoryChanged?.Invoke();
                    return true;
                }
            }
        }

        // If not stackable or no existing stack, find an empty slot
        foreach (var slot in slots)
        {
            if (slot.IsEmpty)
            {
                ItemStack remaining = slot.Place(newItemStack);
                if (remaining.IsEmpty)
                {
                    OnInventoryChanged?.Invoke();
                    return true;
                }
            }
        }

        // Inventory is full
        Debug.Log("Inventory is full!");
        return false;
    }

    public bool RemoveItem(ItemStack itemToRemove)
    {
        foreach (var slot in slots)
        {
            if (slot.item == itemToRemove.item)
            {
                if (slot.quantity >= itemToRemove.quantity)
                {
                    slot.quantity -= itemToRemove.quantity;
                    if (slot.quantity == 0)
                    {
                        slot.item = null; // Clear the slot if quantity reaches zero
                    }
                    OnInventoryChanged?.Invoke();
                    return true;
                }
            }
        }

        Debug.Log("Item not found or insufficient quantity to remove!");
        return false;
    }

    public void Clear()
    {
        foreach (var slot in slots)
        {
            slot.item = null;
            slot.quantity = 0;
        }
        OnInventoryChanged?.Invoke();
    }
}
