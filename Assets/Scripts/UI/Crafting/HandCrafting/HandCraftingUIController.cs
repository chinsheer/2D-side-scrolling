using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class controlling hand crafting UI lifecycle and interactions
public class HandCraftingUIController : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;

    private CraftListUI _craftList;
    private CraftPreviewUI _craftPreviewUI;
    private ICraftingProvider _craftingProvider;

    void Awake()
    {
        _craftingProvider = GetComponentInParent<ICraftingProvider>();
        _craftList = GetComponentInChildren<CraftListUI>();
        _craftPreviewUI = GetComponentInChildren<CraftPreviewUI>();
        _craftList.Initialize(_craftingProvider, _inventory);
        _craftPreviewUI.Initialize(_inventory);
        _craftPreviewUI.SelectedRecipe = _craftingProvider.AvailableRecipes[0];
        _craftList.RefreshUI();
        _craftPreviewUI.RefreshUI();
        _craftList.OnChangeSelectedRecipe += UpdatePreview;
    }

    public void UpdatePreview(int selectedRecipeIndex)
    {
        var recipes = _craftingProvider.AvailableRecipes;
        if (recipes.Count == 0) return;

        _craftPreviewUI.SelectedRecipe = recipes[selectedRecipeIndex];
        _craftPreviewUI.RefreshUI();
    }
}
