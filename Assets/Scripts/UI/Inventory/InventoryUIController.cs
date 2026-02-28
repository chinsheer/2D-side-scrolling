using UnityEngine;
public class InventoryUIController : MonoBehaviour, IUIPageController
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Inventory _hotbarInventory;
    [SerializeField] private InventoryUI _inventoryUI;
    [SerializeField] private InventoryUI _hotbarInventoryUI;
    [SerializeField] private ItemDescriptionUI _itemDescription;

    void Start()
    {
        _inventoryUI.OnSlotClicked += UpdateSelectedItemDescription;
        _inventoryUI.Initialize(_inventory);
        _hotbarInventoryUI.Initialize(_hotbarInventory);
        _itemDescription.RefreshUI();
    }

    public void UpdateSelectedItemDescription(ItemData itemData)
    {
        _itemDescription.ItemData = itemData;
        _itemDescription.RefreshUI();
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
