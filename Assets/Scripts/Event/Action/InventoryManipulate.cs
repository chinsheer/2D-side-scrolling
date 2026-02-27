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
    public ItemData Item;
    public int Quantity;
    public Inventory.InventoryType InventoryType; 

    public override void Execute(EventContext context)
    {
        ItemStack itemStack = new ItemStack { item = Item, quantity = Quantity };
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
                targetInventory.AddItem(itemStack);
                break;
            case ManipulateType.RemoveItem:
                targetInventory.RemoveItem(itemStack);
                break;
            case ManipulateType.ClearInventory:
                targetInventory.Clear();
                break;
        }
    }
}