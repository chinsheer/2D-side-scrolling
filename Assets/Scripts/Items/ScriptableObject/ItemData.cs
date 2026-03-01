using UnityEngine;

public enum ItemType { Resources, Tools, Objects, Seeds }

public class ItemData : ScriptableObject
{
    public int ID;
    public string ItemName;
    public Sprite ItemIcon;
    public bool IsStackable;
    public int MaxStackSize;
    public string ItemDescription;

    protected ItemType _type;
    public ItemType Type => _type;
}