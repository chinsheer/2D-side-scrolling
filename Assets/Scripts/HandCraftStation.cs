using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCraftStation : MonoBehaviour, ICraftingProvider
{
    [SerializeField] private List<RecipeData> _recipes;
    [SerializeField] private Inventory _playerInventory;

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

    public void Craft(RecipeData recipe)
    {
        if (CraftingCore.CanCraft(recipe, _playerInventory))
        {
            CraftingCore.ConsumeIngredients(recipe, _playerInventory);
            CraftingCore.ReturnItem(recipe, _playerInventory);
        }
    }

}
