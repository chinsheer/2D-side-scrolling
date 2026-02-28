using UnityEngine;

public class WorldUIController : MonoBehaviour
{
    [SerializeField] private Inventory _hotbarInventory;
    [SerializeField] private InventoryUI _hotbarInventoryUI;

    public void Start()
    {
        _hotbarInventoryUI.Initialize(_hotbarInventory);
    }
}