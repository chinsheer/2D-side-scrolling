using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotUI : MonoBehaviour, IDropHandler
{
    private Inventory ownerInventory;
    private int slotIndex;

    public event Action<int> OnSlotClicked; // Event for slot click, passing the slot index

    public void Initialize(Inventory inventory, int index)
    {
        ownerInventory = inventory;
        slotIndex = index;
        SetItem(inventory.Slots[index]);
        var button = GetComponent<UnityEngine.UI.Button>();
        button.onClick.AddListener(() => OnSlotClicked?.Invoke(slotIndex)); // Invoke event on click
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

    public void SetSelected(bool selected)
    {
        var selectedHighlight = transform.Find("Select").gameObject;
        selectedHighlight.SetActive(selected);
    }
}
