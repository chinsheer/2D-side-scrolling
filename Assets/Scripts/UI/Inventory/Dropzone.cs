using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dropzone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        var dragged = eventData.pointerDrag?.GetComponent<DraggableItemUI>();
        if (dragged == null) return;
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
