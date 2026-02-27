using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggableItemUI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform _rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private RectTransform _draggingPlane;

    private Image _ghostImage;

    public ItemPayload Payload;

    public void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void SetSource(Inventory fromInventory, int fromIndex)
    {
        Payload = new ItemPayload
        {
            fromInventory = fromInventory,
            fromIndex = fromIndex
        };
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _draggingPlane = canvas.transform as RectTransform;
        _ghostImage = new GameObject("Ghost").AddComponent<Image>();
        _ghostImage.transform.SetParent(canvas.transform, false);
        _ghostImage.sprite = GetComponent<Image>().sprite;
        _ghostImage.SetNativeSize();
        _ghostImage.raycastTarget = false; // Make the ghost image ignore raycasts        

        canvasGroup.blocksRaycasts = false; // Allow raycasts to pass through the dragged item
        canvasGroup.alpha = 0.6f; // Optional: make the original item semi-transparent while dragging
        SetDraggedPosition(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Move the item with the mouse cursor.
        SetDraggedPosition(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true; // Restore raycast blocking when the drag ends
        canvasGroup.alpha = 1f; // Restore original opacity
        if (_ghostImage != null)
        {
            Destroy(_ghostImage.gameObject);
        }
    }

    private void SetDraggedPosition(PointerEventData eventData)
    {
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(_draggingPlane, eventData.position, eventData.pressEventCamera, out globalMousePos))
        {
            _ghostImage.transform.position = globalMousePos;
        }
    }
}
