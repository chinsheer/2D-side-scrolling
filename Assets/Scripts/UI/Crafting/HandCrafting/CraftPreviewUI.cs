using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftPreviewUI : MonoBehaviour
{
    public GameObject PreviewSlotPrefab; // Prefab for the preview object
    public RecipeData SelectedRecipe;
    private Inventory _inventory; // Prefab for the preview object

    public void Initialize(Inventory inventory)
    {
        _inventory = inventory;
        _inventory.OnInventoryChanged += RefreshUI; // Subscribe to inventory changes
    }

    public void RefreshUI()
    {
        for (int i = 0; i < SelectedRecipe.Ingredients.GetLength(0); i++)
        {
            Transform placeHolder;

            if (i < transform.childCount)
            {
                placeHolder = transform.GetChild(i);
            }
            else
            {
                placeHolder = Instantiate(PreviewSlotPrefab, transform).transform;
            }
            var slotUI = placeHolder.GetComponent<CraftPreviewSlotUI>();
            var ingredient = SelectedRecipe.Ingredients[i];
            int ownedAmount = _inventory.GetItemQuantity(ingredient.item);
            var data = new CraftPreviewSlotUI.CraftPreviewSlotData
            {
                OwnedAmount = ownedAmount,
                RequiredAmount = ingredient.quantity,
                ItemIcon = ingredient.item.ItemIcon
            };
            slotUI.SetItem(data, i);
        }
    }

}
