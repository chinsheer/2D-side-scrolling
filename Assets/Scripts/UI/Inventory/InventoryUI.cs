using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    public GameObject SlotPrefab;

    public void Start()
    {
        RefreshUI();
    }

    public void TryDrop(DraggableItemUI dragged, int toSlotIndex)
    {
        if (_inventory.SwapItem(dragged.FromSlotIndex, toSlotIndex))
        {
            RefreshUI();
        }
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
            placeHolder.GetComponent<InventorySlotUI>().SetItem(slot.item, slot.quantity);
        }
    }
}
