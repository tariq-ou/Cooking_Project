using Cooking_Project.Application.Services;

namespace CookingAPI.DTO;

public interface IRecipeManagerAPI
{
    IEnumerable<Recipe> GetAllRecipes();
    
    IEnumerable<Ingredient> GetAllIngredients(IRecipe recipe);

    void AddRecipe(IRecipe recipe);
}