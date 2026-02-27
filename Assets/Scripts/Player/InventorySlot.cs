using System;
using UnityEngine;

[Serializable]
public class InventorySlot
{
    public ItemData item;
    public int quantity;

    public bool IsEmpty => item == null;
    public bool CanStack(ItemData newItem) => item != null && item.ID == newItem.ID && item.IsStackable;
}