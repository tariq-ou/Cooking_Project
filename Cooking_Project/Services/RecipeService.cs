using System.Xml.Linq;
using Cooking_Project.Application.Ports;
using Cooking_Project.Application.Domain;
using Cooking_Project.Application.Services;
using Cooking_Project.Application.Adaptors;


namespace Cooking_Project.Application.Services;

public class RecipeService : IRecipeService, IDBService
{
    IRecipeRepository _recipeRepository;

    public RecipeService(IRecipeRepository recipeRepository)
    {
        _recipeRepository = recipeRepository;
    }

    public void AddRecipeSave(Recipe recipe)
    {
        
        _recipeRepository.Save(recipe);
    }

    public List<Recipe> ReadAllRecipe()
    {
        return _recipeRepository.ReadAll();
    }

    public void AddIngredientSave(string recipeName, List<Ingredient> ingredients)
    {
        
        _recipeRepository.SaveIngredient(recipeName, ingredients);
    }
    
    public void DeleteRecipeIngredients(Recipe recipe)
    {
        
        _recipeRepository.Delete(recipe);
    }

    public void ReadExportDB()
    {
        _recipeRepository.ExportDB();
    }

    public void ImportToDB()
    {
        _recipeRepository.ImportDB();
    }

    public void SyncMemory(IRecipeManager recipeManager, Func<IInputProvider> inputProviderFactory)
    {
        //syncing db recipes with recipemanager list
        recipeManager.Recipes = this.ReadAllRecipe();
        Console.WriteLine("Recipes syced");
        //Give each recipe an InputProvider as those are not mapped
        foreach (var recipe in recipeManager.Recipes)
            recipe.InputProvider = inputProviderFactory();
    }
}