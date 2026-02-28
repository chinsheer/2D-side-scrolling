using System.Collections.Generic;
using UnityEngine;

public static class CraftingCore
{
    public static bool CanCraft(RecipeData recipe, Inventory inventory)
    {
        foreach (var ingredient in recipe.Ingredients)
        {
            if (inventory.GetItemQuantity(ingredient.item) < ingredient.quantity)
            {
                return false; // Not enough of this ingredient
            }
        }
        return true; // All ingredients are available
    }

    public static bool ConsumeIngredients(RecipeData recipe, Inventory inventory)
    {
        foreach (var ingredient in recipe.Ingredients)
        {
            if (!inventory.RemoveItem(ingredient))
            {
                Debug.LogError("Failed to consume ingredient: " + ingredient.item.name);
                return false; // Failed to consume an ingredient
            }
        }
        return true; // All ingredients consumed successfully
    }

    public static bool ReturnItem(RecipeData recipe, Inventory inventory)
    {
        if (inventory.AddItem(recipe.ResultItem))
        {
            return true; // Item added successfully
        }
        else
        {
            Debug.LogError("Failed to return crafted item: " + recipe.ResultItem.item.name);
            return false; // Failed to add the crafted item to inventory
        }
    }

    public static int GetCraftableQuantity(RecipeData recipe, Inventory inventory)
    {
        int maxCraftable = int.MaxValue;
        foreach (var ingredient in recipe.Ingredients)
        {
            int available = inventory.GetItemQuantity(ingredient.item);
            int craftableForIngredient = available / ingredient.quantity;
            if (craftableForIngredient < maxCraftable)
            {
                maxCraftable = craftableForIngredient; // Update max craftable based on this ingredient
            }
        }
        return maxCraftable; // Return the maximum number of times this recipe can be crafted
    }
}