using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// This class controlling hand crafting UI lifecycle and interactions
public class HandCraftingUIController : MonoBehaviour, IUIPageController
{
    [SerializeField] private Inventory _inventory;

    private CraftListUI _craftList;
    private CraftPreviewUI _craftPreviewUI;
    private ICraftingProvider _craftingProvider;
    private ItemDescriptionUI _itemDescription;

    void Start()
    {
        _craftingProvider = GetComponentInParent<ICraftingProvider>();
        _craftList = GetComponentInChildren<CraftListUI>();
        _craftPreviewUI = GetComponentInChildren<CraftPreviewUI>();
        _itemDescription = GetComponentInChildren<ItemDescriptionUI>();
        _craftList.Initialize(_craftingProvider, _inventory);
        _craftPreviewUI.Initialize(_inventory);
        _craftPreviewUI.SelectedRecipe = _craftingProvider.AvailableRecipes[0];
        _itemDescription.ItemData = _craftingProvider.AvailableRecipes[0].ResultItem.item;
        _craftList.RefreshUI();
        _craftPreviewUI.RefreshUI();
        _itemDescription.RefreshUI();
        _craftList.OnChangeSelectedRecipe += UpdatePreview;
    }

    public void UpdatePreview(int selectedRecipeIndex)
    {
        var recipes = _craftingProvider.AvailableRecipes;
        if (recipes.Count == 0) return;

        _craftPreviewUI.SelectedRecipe = recipes[selectedRecipeIndex];
        _craftPreviewUI.RefreshUI();
        _itemDescription.ItemData = recipes[selectedRecipeIndex].ResultItem.item;
        _itemDescription.RefreshUI();
    }

    public void RequestCraft()
    {
        _craftingProvider.Craft(_craftPreviewUI.SelectedRecipe);
        _craftList.RefreshUI();
        _craftPreviewUI.RefreshUI();
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void RefreshUI()
    {
        UpdatePreview(0);
    }
}
