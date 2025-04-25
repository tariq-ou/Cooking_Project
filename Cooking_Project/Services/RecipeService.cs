using System.Xml.Linq;
using Cooking_Project.Application.Ports;
using Cooking_Project.Application.Domain;
using Cooking_Project.Application.Services;


namespace Cooking_Project.Application.Services;

public class RecipeService
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
    
}