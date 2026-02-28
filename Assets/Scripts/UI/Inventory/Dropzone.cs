using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dropzone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private InventoryUI _inventoryUI;
    private Inventory _inventory;

    public void Initialize(InventoryUI inventoryUI)
    {
        _inventoryUI = inventoryUI;
    }

    public void OnDrop(PointerEventData eventData)
    {
        var dragged = eventData.pointerDrag?.GetComponent<DraggableItemUI>();
        if (dragged == null) return;

        // Hard coded to remove the item from inventory when dropped in the dropzone, can be improved by using event system or callback        
        _inventory.RemoveItem(_inventory.Slots[dragged.Payload.fromIndex].Stack);
        // Could create event onChange to trigger UI refresh instead of directly calling it here
        _inventoryUI.RefreshUI();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        var dragged = eventData.pointerDrag?.GetComponent<DraggableItemUI>();
        if (dragged != null)
        {
            var image = GetComponent<UnityEngine.UI.Image>();
            var color = image.color;
            color.a = 0.5f; // Highlight the dropzone when an item is dragged over it
            image.color = color;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Remove highlight when the dragged item leaves the dropzone
        var image = GetComponent<UnityEngine.UI.Image>();
        var color = image.color;
        color.a = 0f; // Reset alpha
        image.color = color;
    }
}
