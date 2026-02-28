using System;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    public GameObject SlotPrefab;

    public event Action<ItemData> OnSlotClicked; // Event for slot click, passing the item data

    private int selectedSlotIndex = 0;

    public void Start()
    {
        RefreshUI();
        _inventory.OnInventoryChanged += RefreshUI;
    }

    public void RefreshUI()
    {
        for (int i = 0; i < _inventory.Slots.Count; i++)
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
            var slotUI = placeHolder.GetComponent<InventorySlotUI>();
            slotUI.Initialize(_inventory, i);
            slotUI.SetSelected(i == selectedSlotIndex);
            slotUI.OnSlotClicked += RefreshSelectedSlot; // Subscribe to slot click event
        }
    }

    // Refresh only the selected slot highlight
    public void RefreshSelectedSlot(int selectIndex)
    {
        transform.GetChild(selectedSlotIndex).GetComponent<InventorySlotUI>().SetSelected(false); // Deselect previous
        transform.GetChild(selectIndex).GetComponent<InventorySlotUI>().SetSelected(true); // Select new
        selectedSlotIndex = selectIndex;
        OnSlotClicked?.Invoke(_inventory.Slots[selectIndex].item); // Invoke event with item data of selected slot
    }
}
