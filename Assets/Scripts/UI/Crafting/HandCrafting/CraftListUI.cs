using System;
using UnityEngine;

public class CraftListUI : MonoBehaviour
{
    [SerializeField] private GameObject _CraftItemPrefab;

    private ICraftingProvider _craftingProvider;
    private Inventory _inventory;

    private int _selectedIndex = 0;

    public event Action<int> OnChangeSelectedRecipe;

    public void Initialize(ICraftingProvider craftingProvider, Inventory inventory)
    {
        _craftingProvider = craftingProvider;
        _inventory = inventory;
        _inventory.OnInventoryChanged += RefreshUI; // Subscribe to inventory changes
    }

    public void RefreshUI()
    {
        var recipes = _craftingProvider.AvailableRecipes;
        for (int i = 0; i < Mathf.Max(recipes.Count, transform.childCount); i++)
        {
            Transform placeHolder;

            if(i >= recipes.Count)
            {
                // If there are more UI elements than recipes, disable the extra ones
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
                placeHolder = Instantiate(_CraftItemPrefab, transform).transform;
            }
            var craftItemUI = placeHolder.GetComponent<CraftItemUI>();
            int craftableQuantity = CraftingCore.GetCraftableQuantity(recipes[i], _inventory);
            var data = new CraftItemUI.CraftItemData
            {
                RecipeData = recipes[i],
                CraftableQuantity = craftableQuantity,
                IsSelected = i == _selectedIndex
            };
            craftItemUI.SetItem(data, i);
            craftItemUI.OnSelected += RefreshSelectedRecipe;
        }
    }

    public void RefreshSelectedRecipe(int selectedIndex)
    {
        _selectedIndex = selectedIndex;
        var recipes = _craftingProvider.AvailableRecipes;
        if (recipes.Count == 0) return;

        CraftItemUI selectedCraftItemUI = transform.GetChild(_selectedIndex).GetComponent<CraftItemUI>();
        selectedCraftItemUI.toggleSelection(false);
        CraftItemUI newSelectedCraftItemUI = transform.GetChild(selectedIndex).GetComponent<CraftItemUI>();
        newSelectedCraftItemUI.toggleSelection(true);
        OnChangeSelectedRecipe?.Invoke(selectedIndex);
    }

}
