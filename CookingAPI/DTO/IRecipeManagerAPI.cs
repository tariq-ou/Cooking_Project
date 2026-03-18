using Cooking_Project.Application.Services;

namespace CookingAPI.DTO;

public interface IRecipeManagerAPI
{
    IEnumerable<Recipe> GetAllRecipes();
    
    IEnumerable<Ingredient> GetAllIngredients(IRecipe recipe);

    void AddRecipe(IRecipe recipe);

    IRecipe CreateRecipe(RecipeDTO recipeDto, List<Ingredient> ingredientsMapped);
    
    IRecipe CreateRecipeNoId(CreateRecipeDTO recipeDto, List<Ingredient> ingredientsMapped, IFormFile? imageFile);
    IRecipe CreateRecipeWithId(RecipeDTO recipeDto, List<Ingredient> ingredientsMapped, IFormFile? imageFile);
    
    void DeleteAllIngredientAPI(IRecipe recipe);
    
    void DeleteStepsAPI(IRecipe recipe);
    
    void DeleteIngredientAPI(IRecipe recipe, IEnumerable<string> ingredientsToDelete);
    
    void IngredientAddAPI(IRecipe recipe, IEnumerable<Ingredient> ingredientsToAdd);
    
    IRecipe FindRecipeAPI(string recipeName);

    IRecipe FindIdRecipeAPI(int Id);
    
    IRecipe AddStepsAPI(IRecipe recipeName, string inputSteps);
    
    Task CopyImageSetPathAPI (IRecipe recipe, IFormFile imageFile);
}