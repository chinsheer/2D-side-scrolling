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
            placeHolder.GetComponent<InventorySlotUI>().Initialize(_inventory, i);
        }
    }
}
