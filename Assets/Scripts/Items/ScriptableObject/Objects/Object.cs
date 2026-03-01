using UnityEngine;

[CreateAssetMenu(fileName = "NewObject", menuName = "Inventory/Item/Objects/Object")]
public class ObjectItem : ItemData, IPlaceableItem
{
    public GameObject PlaceablePrefab; // Reference to the prefab that will be placed in the world

    private void OnEnable()
    {
        _type = ItemType.Objects; // Set the item type to Objects for object items
    }

    public GameObject GetPlaceablePrefab()
    {
        return PlaceablePrefab;
    }
}