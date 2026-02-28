using UnityEngine;
public class InventoryUIController : MonoBehaviour, IUIPageController
{
    [SerializeField] private Inventory _inventory;
    private InventoryUI _inventoryUI;
    private ItemDescriptionUI _itemDescription;

    void Awake()
    {
        _inventoryUI = GetComponentInChildren<InventoryUI>();
        _itemDescription = GetComponentInChildren<ItemDescriptionUI>();
        _inventoryUI.Start();
        _inventoryUI.OnSlotClicked += UpdateSelectedItemDescription;
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
