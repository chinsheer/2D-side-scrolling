using System;
using UnityEngine;

[Serializable]
public class InventorySlot
{
    public ItemData item;
    public int quantity;

    public ItemStack Stack => new ItemStack { item = item, quantity = quantity };
    public void SetStack(ItemStack newStack)
    {
        item = newStack.item;
        quantity = newStack.quantity;
    }
    public bool IsEmpty => item == null;

    public ItemStack Place(ItemStack incomingStack)
    {
        if (IsEmpty)
        {
            SetStack(incomingStack);
            return new ItemStack(); // Return empty stack
        }
        else if (Stack.CanStack(incomingStack.item))
        {
            int totalQuantity = quantity + incomingStack.quantity;
            int maxStackSize = item.MaxStackSize;

            if (totalQuantity <= maxStackSize)
            {
                quantity = totalQuantity;
                return new ItemStack(); // All items placed, return empty stack
            }
            else
            {
                quantity = maxStackSize; // Fill to max
                return new ItemStack { item = incomingStack.item, quantity = totalQuantity - maxStackSize }; // Return remaining items
            }
        }
        else
        {
            // Can't stack, swap items
            ItemData tempItem = item;
            int tempQuantity = quantity;

            SetStack(incomingStack); // Place incoming stack in this slot

            return new ItemStack { item = tempItem, quantity = tempQuantity }; // Return the original stack that was in this slot
        }
    }

}