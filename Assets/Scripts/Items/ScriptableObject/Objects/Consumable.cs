using UnityEngine;

[CreateAssetMenu(fileName = "NewConsumable", menuName = "Inventory/Item/Objects/Consumable")]

public class Consumable : ItemData, IUsableItem
{
    public int HealAmount; // Amount of health this potion restores

    private void OnEnable()
    {
        _type = ItemType.Objects; // Set the item type to Objects for potion items
    }

    public UseResult Use(UseContext context)
    {
        // Assume consumable to be a health potion
        PlayerHealth playerHealth = context.User.GetComponentInChildren<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.Heal(HealAmount); // Heal the player by the specified amount
            return new UseResult { Success = true, consumedQuantity = 1 };
        }
        return new UseResult { Success = false, consumedQuantity = 0 };
    }
}