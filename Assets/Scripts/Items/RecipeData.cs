using UnityEngine;

[CreateAssetMenu(fileName = "NewRecipe", menuName = "Inventory/Crafting/Recipe")]

public class RecipeData : ScriptableObject
{
    public int ID;
    public string RecipeName;
    public ItemStack ResultItem;
    public ItemStack[] Ingredients;
}