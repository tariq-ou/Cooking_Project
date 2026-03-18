namespace Cooking_Project.Application.Services;

public interface IRecipeService
{
    void AddItemSave(IRecipe recipe);
    List<Recipe> ReadAllItem();
    
    void AddNestedSave(string recipeName, List<Ingredient> ingredients);
    void DeleteItemandNested(IRecipe recipe);

    void RemoveAllNestedItem(int recipeId);




}