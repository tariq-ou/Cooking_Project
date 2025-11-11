using Cooking_Project.Application.Services;

namespace Cooking_Project.Application.Adaptors;

public class RecipeServiceTest : IRecipeService
{
    public void AddItemSave(IRecipe recipe)
    {
        //throw new NotImplementedException();
    }

    public List<Recipe> ReadAllItem()
    {
        throw new NotImplementedException();
    }

    public void AddNestedSave(string recipeName, List<Ingredient> ingredients)
    {
        throw new NotImplementedException();
    }

    public void DeleteItemandNested(IRecipe recipe)
    {
        throw new NotImplementedException();
    }
}