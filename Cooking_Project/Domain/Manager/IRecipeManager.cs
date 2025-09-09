using Cooking_Project.Application.Services;

namespace Cooking_Project.Application.Domain;

public interface IRecipeManager
{
    public List<Recipe> Recipes { get; set; }
    IRecipe? AddRecipe();
    void ViewRecipe();
    void DeleteRecipe(IRecipe toDelete);
    IRecipe FindRecipe(out string recipeName);
   IRecipe CheckRecipe(out string recipeName);
    int RecipeCount();
    IRecipe GetRecipe(string recipeName);
}