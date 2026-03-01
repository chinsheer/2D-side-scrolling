using UnityEngine;

[CreateAssetMenu(fileName = "New InventoryManipulate", menuName = "InventoryManipulate")]
public class InventoryManipulate : EventAction
{
    public enum ManipulateType
    {
        AddItem,
        RemoveItem,
        ClearInventory,
    }
    
    public ManipulateType Type;
    public ItemStack[] Items;
    public Inventory.InventoryType InventoryType; 

    public override void Execute(EventContext context)
    {
        Inventory targetInventory = null;
        foreach (var inv in context.PlayerInventory)
        {
            if (inv.Type == InventoryType)
            {
                targetInventory = inv;
                break;
            }
        }
        switch (Type)
        {
            case ManipulateType.AddItem:
                foreach (var itemStack in Items)
                {
                    targetInventory.AddItem(itemStack);
                }
                break;
            case ManipulateType.RemoveItem:
                foreach (var itemStack in Items)
                {
                    targetInventory.RemoveItem(itemStack);
                }
                break;
            case ManipulateType.ClearInventory:
                targetInventory.Clear();
                break;
        }
    }
}