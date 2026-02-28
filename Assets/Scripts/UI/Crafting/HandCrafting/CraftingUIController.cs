using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// This class controlling hand crafting UI lifecycle and interactions
public class CraftingUIController : MonoBehaviour, IUIPageController
{
    [SerializeField] private Inventory _inventory;

    [SerializeField] private CraftListUI _craftList;
    [SerializeField] private CraftPreviewUI _craftPreviewUI;
    [SerializeField] private ItemDescriptionUI _itemDescription;
    [SerializeField] private HandCraftStation _handCraftStationProvider;
    private ICraftingProvider _craftingProvider;

    void Start()
    {
        _craftingProvider ??= _handCraftStationProvider as ICraftingProvider;
        _craftList.Initialize(_craftingProvider, _inventory);
        _craftPreviewUI.Initialize(_inventory);
        _craftPreviewUI.SelectedRecipe = _craftingProvider.AvailableRecipes[0];
        _itemDescription.ItemData = _craftingProvider.AvailableRecipes[0].ResultItem.item;
        _craftList.RefreshUI();
        _craftPreviewUI.RefreshUI();
        _itemDescription.RefreshUI();
        _craftList.OnChangeSelectedRecipe += UpdatePreview;
    }

    public void Bind(ICraftingProvider craftingProvider)
    {
        _craftingProvider = craftingProvider;
        _craftList.Initialize(_craftingProvider, _inventory);
        _craftPreviewUI.Initialize(_inventory);
        _craftPreviewUI.SelectedRecipe = _craftingProvider.AvailableRecipes[0];
        _itemDescription.ItemData = _craftingProvider.AvailableRecipes[0].ResultItem.item;
        _craftList.RefreshUI();
        _craftPreviewUI.RefreshUI();
        _itemDescription.RefreshUI();
    }

    public void Unbind(ICraftingProvider craftingProvider)
    {
        if (_craftingProvider == craftingProvider)
        {
            _craftingProvider = _handCraftStationProvider as ICraftingProvider;
            _craftList.Initialize(_craftingProvider, _inventory);
            _craftPreviewUI.Initialize(_inventory);
            _craftPreviewUI.SelectedRecipe = _craftingProvider.AvailableRecipes[0];
            _itemDescription.ItemData = _craftingProvider.AvailableRecipes[0].ResultItem.item;
            _craftList.RefreshUI();
            _craftPreviewUI.RefreshUI();
            _itemDescription.RefreshUI();
        }
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
        _craftingProvider.Craft(_craftPreviewUI.SelectedRecipe, _inventory);
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
