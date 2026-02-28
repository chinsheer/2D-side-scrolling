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
        for (int i = 0; i < Mathf.Max(SelectedRecipe.Ingredients.GetLength(0), transform.childCount); i++)
        {
            Transform placeHolder;

            if(i >= SelectedRecipe.Ingredients.GetLength(0))
            {
                // If there are more UI elements than ingredients, disable the extra ones
                placeHolder = transform.GetChild(i);
                placeHolder.gameObject.SetActive(false);
                continue;
            }

            if (i < transform.childCount)
            {
                placeHolder = transform.GetChild(i);
                placeHolder.gameObject.SetActive(true);
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
