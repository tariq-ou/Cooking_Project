using Cooking_Project.Application.Services;

namespace Cooking_Project.Application.Domain;

public interface IRecipeManager
{
    public List<Recipe> Recipes { get; set; }
    Recipe? AddRecipe();
    void ViewRecipe();
    void DeleteRecipe(Recipe toDelete);
    Recipe FindRecipe(out string recipeName);
    Recipe CheckRecipe(out string recipeName);
    int RecipeCount();
    Recipe GetRecipe(string recipeName);
}