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
    
}