using UnityEngine;

public enum ItemType { Resources, Tools, Objects, Seeds }

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public int ID;
    public string ItemName;
    public ItemType ItemType;
    public Sprite ItemIcon;
    public bool IsStackable;
    public int MaxStackSize;
    public UseAction UseAction;
}