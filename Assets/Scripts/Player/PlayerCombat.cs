using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private InventoryUI _hotbarInventoryUI; // Reference to the player's inventory UI
    [SerializeField] private Inventory _hotbarInventory; // Reference to the player's inventory data
    [SerializeField] private GameObject _aimIndicatorPrefab; // Prefab for the aiming indicator

    private Vector2 _handPosition;
    private GameObject _currentAimIndicator;

    public void Update()
    {
        if (_currentAimIndicator != null)
        {
            _currentAimIndicator.transform.position = _handPosition; // Keep the aiming indicator at the hand position
        }
    }

    public void TryUseItem()
    {
        var selectedSlot = _hotbarInventory.Slots[_hotbarInventory.SelectedSlotIndex];
        if (selectedSlot.item != null && selectedSlot.item.UseAction != null)
        {
            UseContext context = new UseContext
            {
                User = gameObject,
                HandPosition = _handPosition
            };
            selectedSlot.item.UseAction.Use(context);
        }
    }

    public void SetHandPosition(Vector2 mousePosition)
    {
        _handPosition = (Vector2)transform.position + (mousePosition - (Vector2)transform.position).normalized; // Position the hand in the direction of aiming
    }

    public void StartAiming(Vector2 mousePosition)
    {
        _currentAimIndicator = Instantiate(_aimIndicatorPrefab, _handPosition, Quaternion.identity); // Show the aiming indicator at the hand position
    }

    public void StopAiming()
    {
        if (_currentAimIndicator != null)
        {
            Destroy(_currentAimIndicator);
            _currentAimIndicator = null;
        }
    }

    public void SetSelectedSlot(int index)
    {
        _hotbarInventoryUI.RefreshSelectedSlot(index);
    }
}
