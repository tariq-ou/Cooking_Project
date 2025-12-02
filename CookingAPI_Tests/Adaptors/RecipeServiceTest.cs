using Cooking_Project.Application.Domain;
using Cooking_Project.Application.Ports;
using Cooking_Project.Application.Services;

namespace Cooking_Project.Application.Adaptors;


public class RecipeServiceTest : IRecipeDBService
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
       // throw new NotImplementedException();
    }

    public void DeleteItemandNested(IRecipe recipe)
    {
        //throw new NotImplementedException();
    }

    public void DBToExport()
    {
        //throw new NotImplementedException();
    }

    public void ImportToDB()
    {
        //throw new NotImplementedException();
    }

    public void SyncDBMemory(IRecipeManager recipeManager, Func<IInputProvider> inputProviderFactory)
    {
        //throw new NotImplementedException();
    }
    
}