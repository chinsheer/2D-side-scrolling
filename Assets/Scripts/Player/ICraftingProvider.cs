using System.Collections.Generic;

public interface ICraftingProvider
{
    List<RecipeData> AvailableRecipes { get; }
}