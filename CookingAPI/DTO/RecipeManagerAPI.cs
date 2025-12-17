using AutoMapper;
using Cooking_Project.Application.Domain;
using Cooking_Project.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CookingAPI.DTO;

public class RecipeManagerAPI : IRecipeManagerAPI
{

    public IRecipeManager _recipeManager;
    private readonly ILogger<RecipeManagerAPI> _logger;

    public RecipeManagerAPI(IRecipeManager recipeManager, ILogger<RecipeManagerAPI> logger)
    {
        _recipeManager = recipeManager;
        _logger = logger;
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
        _logger.LogInformation("GetAllRecipesAPI called");
        _logger.LogInformation($"{_recipeManager.Recipes.Count()} recipes from manager");
        //_logger.LogInformation($"{_recipeManager.Recipes.First().Name} recipes from manager");
        return _recipeManager.Recipes;
    }
    
    public IEnumerable<Ingredient> GetAllIngredients(IRecipe recipe)
    {
           
        return recipe.Ingredients;
    }
    
    public void AddRecipe(IRecipe recipe)
    {
          var recipeToAdd = (Recipe)recipe;
          _logger.LogInformation("AddRecipe called - Recipe Objected Created");
        _recipeManager.Recipes.Add(recipeToAdd);
    }
    
    public IRecipe CreateRecipe(RecipeDTO recipeDto, List<Ingredient> ingredientsMapped)
    {
        
        var recipe = new Recipe()
        {
            Name = recipeDto.Name,
            Ingredients = ingredientsMapped,
            Servings = recipeDto.Servings,
            Steps = recipeDto.Steps
            
        };
        
        _logger.LogInformation($"Recipe Objected Created: recipeName-{recipe.Name}, ingredientsMapped-{recipe.Ingredients.Count} ingredients, Servings-{recipe.Servings}, Steps (not included for length) ");
        
        _recipeManager.Recipes.Add(recipe);
        
        _logger.LogInformation($"Recipe Objected added to recipes list: recipeName-{recipe.Name}");
        
        return recipe;

        //retrun recipe;
        //_recipeManager.Recipes.Add(recipeToAdd);
    }

    public void DeleteAllIngredientAPI(IRecipe recipeToDeleteFrom)
    {
        
       
            recipeToDeleteFrom.Ingredients.Clear();
            //_logger.LogInformation($"Recipe {recipeToDeleteFrom.Name} Deleted Ingredient: {ingredient.Name}");
        
    }
    
    public void DeleteStepsAPI(IRecipe recipeToDeleteFrom)
    {
      
        
        recipeToDeleteFrom.Steps.Clear();
        _logger.LogInformation($"Recipe {recipeToDeleteFrom.Name} Deleted Steps");
    }
    
    public void DeleteIngredientAPI(IRecipe recipeToDeleteFrom, IEnumerable<string> ingredientsToDelete)
    {
        
        
        foreach (var ingredient in ingredientsToDelete)
        {
            recipeToDeleteFrom.Ingredients.RemoveAll(r => r.Name == ingredient);
        }
        
        _logger.LogInformation($"Recipe {recipeToDeleteFrom.Name} Deleted Ingredients from list shared");
    }

    public void IngredientAddAPI(IRecipe recipe, IEnumerable<Ingredient> ingredientsToAdd)
    {
        recipe.Ingredients.AddRange(ingredientsToAdd);
        _logger.LogInformation($"Recipe {recipe.Name} Added Ingredients");
    }


    public IRecipe FindRecipeAPI(string recipeName)
    {
        if (!_recipeManager.Recipes.Any(r => r.Name == recipeName))
        {
            _logger.LogInformation($"Recipe {recipeName} not found");
            return null;
            
        }
        
        return _recipeManager.Recipes.FirstOrDefault(r => r.Name == recipeName);
    }

    public IRecipe AddStepsAPI(IRecipe recipe, string inputSteps)
    {
        if (inputSteps == null)
        {
            _logger.LogInformation($"Recipe input steps not found or not provided");
            return null;
        }
        
         recipe.Steps = inputSteps.Split('\n').ToList();
         return recipe;
    }
   
    
}