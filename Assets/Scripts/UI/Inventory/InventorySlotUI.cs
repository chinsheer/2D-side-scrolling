using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotUI : MonoBehaviour, IDropHandler
{
    private Inventory ownerInventory;
    private int slotIndex;

    public void Initialize(Inventory inventory, int index)
    {
        ownerInventory = inventory;
        slotIndex = index;
        SetItem(inventory.Slots[index]);
    }
    
    public void OnDrop(PointerEventData eventData)
    {
        var dragged = eventData.pointerDrag?.GetComponent<DraggableItemUI>();
        if (dragged == null) return;

        Inventory fromInventory = dragged.Payload.fromInventory;
        int fromIndex = dragged.Payload.fromIndex;
        InventoryTransfer.Transfer(fromInventory, fromIndex, ownerInventory, slotIndex);
    }

    public void SetItem(InventorySlot slot)
    {
        var itemIcon = transform.Find("Icon").GetComponent<UnityEngine.UI.Image>();
        var quantityText = transform.Find("Quantity").GetComponent<TextMeshProUGUI>();

        if (slot.item != null)
        {
            itemIcon.sprite = slot.item.ItemIcon;
            itemIcon.enabled = true;
            quantityText.text = slot.quantity.ToString();
            quantityText.enabled = true;
        }
        else
        {
            itemIcon.sprite = null;
            itemIcon.enabled = false;
            quantityText.text = "";
            quantityText.enabled = false;
        }

        var draggable = GetComponentInChildren<DraggableItemUI>();
        if (draggable != null)
        {
            draggable.SetSource(ownerInventory, slotIndex);
        }
    }
}
