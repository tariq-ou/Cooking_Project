using System.Xml.Linq;
using Cooking_Project.Application.Ports;
using Cooking_Project.Application.Domain;
using Cooking_Project.Application.Services;
using Cooking_Project.Application.Adaptors;


namespace Cooking_Project.Application.Services;

public class RecipeService : IRecipeDBService
{
    IRecipeRepositoryDB  _recipeRepository;

    public RecipeService( IRecipeRepositoryDB recipeRepository)
    {
        _recipeRepository = recipeRepository;
        
    }

    public void AddItemSave(IRecipe recipe)
    {
        
        _recipeRepository.Save((Recipe)recipe);
    }
    

    public List<Recipe> ReadAllItem()
    {
        return _recipeRepository.ReadAll();
    }

    public void AddNestedSave(string recipeName, List<Ingredient> ingredients)
    {
        
        _recipeRepository.SaveNestedItem(recipeName, ingredients);
    }
    
    public void DeleteItemandNested(IRecipe recipe)
    {
        
        _recipeRepository.Delete((Recipe)recipe);
    }

    public void RemoveAllNestedItem(int recipeId)
    {
        _recipeRepository.DeleteAllNestedItem(recipeId);
    }

    public void DBToExport()
    {
        _recipeRepository.ExportDB();
    }

    public void ImportToDB()
    {
        _recipeRepository.ImportDB();
    }

    public void SyncDBMemory(IRecipeManager recipeManager, Func<IInputProvider> inputProviderFactory)
    {
        //syncing db recipes with recipemanager list
        recipeManager.Recipes = this.ReadAllItem();
        Console.WriteLine("Recipes syced");
        //Give each recipe an InputProvider as those are not mapped
        foreach (var recipe in recipeManager.Recipes)
            recipe.InputProvider = inputProviderFactory();
    }
}