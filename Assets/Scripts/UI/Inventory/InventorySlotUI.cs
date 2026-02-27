using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotUI : MonoBehaviour, IDropHandler
{
    public int SlotIndex { get; private set; }
    public InventoryUI ParentInventoryUI { get; private set; }

    public void Awake()
    {
        ParentInventoryUI = GetComponentInParent<InventoryUI>();
        // This is UI hard coding
        SlotIndex = transform.GetSiblingIndex();
    }
    
    public void OnDrop(PointerEventData eventData)
    {
        var dragged = eventData.pointerDrag?.GetComponent<DraggableItemUI>();
        if (dragged == null) return;

        ParentInventoryUI.TryDrop(dragged, SlotIndex);
    }

    public void SetItem(ItemData itemData, int quantity)
    {
        var itemIcon = transform.Find("Icon").GetComponent<UnityEngine.UI.Image>();

        if (itemData != null)
        {
            itemIcon.sprite = itemData.ItemIcon;
            itemIcon.enabled = true;
        }
        else
        {
            itemIcon.sprite = null;
            itemIcon.enabled = false;
        }
    }
}
