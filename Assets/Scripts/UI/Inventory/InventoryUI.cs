using System;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private Inventory _inventory;
    public GameObject SlotPrefab;

    public event Action<ItemData> OnSlotClicked; // Event for slot click, passing the item data

    private int _selectedSlotIndex = 0;
    public int SelectedSlotIndex => _selectedSlotIndex;

    public void Awake()
    {
        if (_inventory != null)
        {
            Initialize(_inventory);
        }
    }

    public void Initialize(Inventory inventory)
    {
        // Unsubscribe from previous inventory events if changing inventory
        if(_inventory != null && inventory == null)
        {
            _inventory.OnInventoryChanged -= RefreshUI; // Unsubscribe from previous inventory events
        }
        _inventory = inventory;
        if(_inventory == null) {
            DisableUI();
            return;
        }
        _inventory.OnInventoryChanged += RefreshUI;
        RefreshUI();
        RefreshSelectedSlot(_selectedSlotIndex); // Highlight the initially selected slot
    }

    public void RefreshUI()
    {
        if (_inventory == null) {
            DisableUI();
            return;
        }
        for (int i = 0; i < _inventory.Capacity; i++)
        {
            var slot = _inventory.Slots[i];

            Transform placeHolder;

            if (i < transform.childCount)
            {
                placeHolder = transform.GetChild(i);
            }
            else
            {
                placeHolder = Instantiate(SlotPrefab, transform).transform;
            }
            placeHolder.gameObject.SetActive(true);
            var slotUI = placeHolder.GetComponent<InventorySlotUI>();
            slotUI.Initialize(_inventory, i);
            slotUI.SetSelected(i == _selectedSlotIndex);
            slotUI.OnSlotClicked += RefreshSelectedSlot; // Subscribe to slot click event
        }
    }

    private void DisableUI()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    // Refresh only the selected slot highlight
    public void RefreshSelectedSlot(int selectIndex)
    {
        transform.GetChild(_selectedSlotIndex).GetComponent<InventorySlotUI>().SetSelected(false); // Deselect previous
        transform.GetChild(selectIndex).GetComponent<InventorySlotUI>().SetSelected(true); // Select new
        _selectedSlotIndex = selectIndex;
        OnSlotClicked?.Invoke(_inventory.Slots[selectIndex].item); // Invoke event with item data of selected slot
    }
}
