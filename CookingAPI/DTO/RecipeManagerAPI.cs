using Cooking_Project.Application.Domain;
using Cooking_Project.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CookingAPI.DTO;

public class RecipeManagerAPI : IRecipeManagerAPI
{

    public IRecipeManager _recipeManager { get; }

    public RecipeManagerAPI(IRecipeManager recipeManager)
    {
        _recipeManager = recipeManager;
    }
    
    // public IEnumerable<string> GetAllRecipes()
    // {
    //     var output = new List<string>(); 
    
    //     foreach (var item in _recipeManager.Recipes)
    //     {
    //         output.Add($"{item.Name} | Serving : {item.Servings}");
    //     }
    // }
    
    
    public IEnumerable<Recipe> GetAllRecipes()
    {
           
        return _recipeManager.Recipes;
    }
    
    public IEnumerable<Ingredient> GetAllIngredients(IRecipe recipe)
    {
           
        return recipe.Ingredients;
    }
    
    public void AddRecipe(IRecipe recipe)
    {
          var recipeToAdd = (Recipe)recipe;
        _recipeManager.Recipes.Add(recipeToAdd);
    }
    
    public IRecipe CreateRecipe(RecipeDTO recipeDto, List<Ingredient> ingredientsMapped)
    {
        
        // if (!int.TryParse(recipeDto.Servings, out int servingsMappedInt))
        // {
        //     
        // }
        // else
        // {
        //     throw new NotSupportedException();
        // }
        
        
        var recipe = new Recipe()
        {
            Name = recipeDto.Name,
            Ingredients = ingredientsMapped,
            Servings = recipeDto.Servings,
            Steps = recipeDto.Steps
            
        };
        
        _recipeManager.Recipes.Add(recipe);
        
        return recipe;

        //retrun recipe;
        //_recipeManager.Recipes.Add(recipeToAdd);
    }
    
   
    
}