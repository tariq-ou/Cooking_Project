using Cooking_Project.Application.Services;

namespace CookingAPI.DTO;

public interface IRecipeManagerAPI
{
    IEnumerable<Recipe> GetAllRecipes();
    
    IEnumerable<Ingredient> GetAllIngredients(IRecipe recipe);

    void AddRecipe(IRecipe recipe);

    IRecipe CreateRecipe(RecipeDTO recipeDto, List<Ingredient> ingredientsMapped);
    
    void DeleteAllIngredientAPI(IRecipe recipe);
    
    void DeleteStepsAPI(IRecipe recipe);
    
    void DeleteIngredientAPI(IRecipe recipe, IEnumerable<string> ingredientsToDelete);
    
    void IngredientAddAPI(IRecipe recipe, IEnumerable<Ingredient> ingredientsToAdd);
    
    IRecipe FindRecipeAPI(string recipeName);
    
    IRecipe AddStepsAPI(IRecipe recipeName, string inputSteps);
}