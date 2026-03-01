[System.Serializable]
public class ItemStack
{
    public ItemData item;
    public int quantity;

    public bool IsEmpty => item == null;
    public bool CanStack(ItemData newItem) => item != null && item == newItem && item.IsStackable;
}
