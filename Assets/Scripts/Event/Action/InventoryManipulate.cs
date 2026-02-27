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

    public override void Execute(EventContext context)
    {
        switch (Type)
        {
            case ManipulateType.AddItem:
                context.PlayerInventory.AddItem(Item, Quantity);
                break;
            case ManipulateType.RemoveItem:
                context.PlayerInventory.RemoveItem(Item, Quantity);
                break;
            case ManipulateType.ClearInventory:
                context.PlayerInventory.Clear();
                break;
        }
    }
}