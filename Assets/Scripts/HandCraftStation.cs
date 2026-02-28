using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCraftStation : MonoBehaviour, ICraftingProvider
{
    [SerializeField] private List<RecipeData> _recipes;
    [SerializeField] private Inventory _playerInventory;

    private RecipeData _currentRecipe;

    public List<RecipeData> AvailableRecipes => _recipes;

    public List<RecipeData> GetCraftableRecipes()
    {
        List<RecipeData> craftableRecipes = new List<RecipeData>();
        foreach (var recipe in _recipes)
        {
            if (CraftingCore.CanCraft(recipe, _playerInventory))
            {
                craftableRecipes.Add(recipe);
            }
        }
        return craftableRecipes;
    }

    public void Craft()
    {
        if (_currentRecipe == null)
        {
            Debug.Log("No recipe selected!");
            return;
        }

        if (!CraftingCore.CanCraft(_currentRecipe, _playerInventory))
        {
            Debug.Log("Not enough ingredients!");
            return;
        }

        // Remove ingredients from inventory
        if (!CraftingCore.ConsumeIngredients(_currentRecipe, _playerInventory))
        {
            Debug.LogError("Failed to consume ingredients!");
            return;
        }

        // Add crafted item to inventory
        if (!CraftingCore.ReturnItem(_currentRecipe, _playerInventory))
        {
            Debug.LogError("Failed to return crafted item!");
            return;
        }
        Debug.Log("Crafted: " + _currentRecipe.ResultItem.item.name);
    }

}
