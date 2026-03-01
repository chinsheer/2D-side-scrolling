using UnityEngine;

[CreateAssetMenu(fileName = "NewMaterial", menuName = "Inventory/Item/Resource/Material")]
public class MaterialItem : ItemData
{
    private void OnEnable()
    {
        _type = ItemType.Resources; // Set the item type to Resources for materials
    }
}