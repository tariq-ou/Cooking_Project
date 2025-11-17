using Cooking_Project.Application.Services;

namespace CookingAPI.DTO;

public interface IRecipeManagerAPI
{
    IEnumerable<Recipe> GetAllRecipes();
    
    IEnumerable<Ingredient> GetAllIngredients(IRecipe recipe);

    void AddRecipe(IRecipe recipe);

    IRecipe CreateRecipe(RecipeDTO recipeDto, List<Ingredient> ingredientsMapped);
    
    void DeleteIngredientAPI(IRecipe recipe, List<IngredientDTO> ingredientsToDelete);
    
    void DeleteStepsAPI(IRecipe recipe);
}