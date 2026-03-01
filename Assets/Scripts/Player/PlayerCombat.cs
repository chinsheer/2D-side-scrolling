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
        var selectedSlot = _hotbarInventory.Slots[_hotbarInventoryUI.SelectedSlotIndex];
        if (selectedSlot.item != null)
        {

            if (selectedSlot.item is IPlaceableItem placeableItem)
            {
                // Handle placing the item in the world
                GameObject prefabToPlace = placeableItem.GetPlaceablePrefab();
                Instantiate(prefabToPlace, _handPosition, Quaternion.identity);
                _hotbarInventory.RemoveItem(new ItemStack { item = selectedSlot.item, quantity = 1 }); // Remove the item from inventory after placing
                _hotbarInventoryUI.RefreshUI();

            }

            else if (selectedSlot.item is IUsableItem usableItem)
            {
                UseResult result = usableItem.Use(new UseContext
                {
                    User = gameObject,
                    HandPosition = _handPosition,
                    inventory = _hotbarInventory,
                    ItemData = selectedSlot.item
                });

                if (result.Success)
                {
                    _hotbarInventory.RemoveItem(new ItemStack { item = selectedSlot.item, quantity = 1 }); // Reduce the quantity based on the use result
                    _hotbarInventoryUI.RefreshUI(); // Calling RefreshUI in combat is not practical
                }
            }
        }
    }

    public void SetHandPosition(Vector2 mousePosition)
    {
        _handPosition = (Vector2)transform.position + (mousePosition - (Vector2)transform.position).normalized; // Position the hand in the direction of aiming
    }

    public void StartAiming(Vector2 mousePosition)
    {
        var selectedSlot = _hotbarInventory.Slots[_hotbarInventoryUI.SelectedSlotIndex];
        if (selectedSlot.item is IAimableItem)
        {
            _currentAimIndicator = Instantiate(_aimIndicatorPrefab, _handPosition, Quaternion.identity); // Show the aiming indicator at the hand position
            SetHandPosition(mousePosition);
        }
    }

    public void StopAiming()
    {
        if (_currentAimIndicator != null)
        {
            Destroy(_currentAimIndicator);
            _currentAimIndicator = null;
            _handPosition = Vector2.right; // Reset hand position to default (can be adjusted as needed)
        }
    }

    public void SetSelectedSlot(int index)
    {
        _hotbarInventoryUI.RefreshSelectedSlot(index);
    }
}
