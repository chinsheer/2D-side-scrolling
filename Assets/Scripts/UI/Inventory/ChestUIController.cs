using UnityEngine;
public class ChestUIController : MonoBehaviour, IUIPageController
{
    [SerializeField] private Inventory _inventory;
    private Inventory _chestInventory;
    [SerializeField] private InventoryUI _inventoryUI;
    [SerializeField] private InventoryUI _chestInventoryUI;

    public void Initialize()
    {
        _inventoryUI.Initialize(_inventory);
        _chestInventoryUI.Initialize(_chestInventory);
    }

    public void Bind(Inventory chestInventory)
    {
        _chestInventory = chestInventory;
        _chestInventoryUI.Initialize(_chestInventory);
    }

    public void Unbind(Inventory chestInventory)
    {
        if (_chestInventory == chestInventory)
        {
            _chestInventory = null;
            _chestInventoryUI.Initialize(null); // Clear chest inventory UI
        }
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
    public void RefreshUI()
    {
        // No dynamic content to refresh in inventory page for now
    }
}
