namespace Cooking_Project.Application.Services;

public interface IRecipeService
{
    void AddRecipeSave(Recipe recipe);
    List<Recipe> ReadAllRecipe();
    void AddIngredientSave(string recipeName, List<Ingredient> ingredients);
    void DeleteRecipeIngredients(Recipe recipe);
}