using System.Collections.Generic;
using UnityEngine;

public class TableCraftStation : MonoBehaviour, ICraftingProvider
{
    [SerializeField] private List<RecipeData> _recipes;

    public List<RecipeData> AvailableRecipes => _recipes;

    public List<RecipeData> GetCraftableRecipes(Inventory targetInventory)
    {
        List<RecipeData> craftableRecipes = new List<RecipeData>();
        foreach (var recipe in _recipes)
        {
            if (CraftingCore.CanCraft(recipe, targetInventory))
            {
                craftableRecipes.Add(recipe);
            }
        }
        return craftableRecipes;
    }

    public void Craft(RecipeData recipe, Inventory targetInventory)
    {
        if (CraftingCore.CanCraft(recipe, targetInventory))
        {
            CraftingCore.ConsumeIngredients(recipe, targetInventory);
            CraftingCore.ReturnItem(recipe, targetInventory);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponentInChildren<PageController>(true)?.BindCraftingProvider(this);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        collision.GetComponentInChildren<PageController>(true)?.UnbindCraftingProvider(this);
    }

}