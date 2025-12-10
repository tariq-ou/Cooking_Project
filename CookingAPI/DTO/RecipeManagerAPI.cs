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

    public void DeleteAllIngredientAPI(IRecipe recipeToDeleteFrom, List<IngredientDTO> ingredientsToDelete)
    {
        //List<string> ingredientToDelete = new List<string>();
        
        foreach (var ingredient in ingredientsToDelete)
        {
            recipeToDeleteFrom.Ingredients.RemoveAll(r => r.Name == ingredient.Name);
        }
    }
    
    public void DeleteStepsAPI(IRecipe recipeToDeleteFrom)
    {
        //List<string> ingredientToDelete = new List<string>();
        
        recipeToDeleteFrom.Steps.Clear();
    }
    
    public void DeleteIngredientAPI(IRecipe recipeToDeleteFrom, IEnumerable<string> ingredientsToDelete)
    {
        //List<string> ingredientToDelete = new List<string>();
        
        foreach (var ingredient in ingredientsToDelete)
        {
            recipeToDeleteFrom.Ingredients.RemoveAll(r => r.Name == ingredient);
        }
    }

    public void IngredientAddAPI(IRecipe recipe, IEnumerable<Ingredient> ingredientsToAdd)
    {
        recipe.Ingredients.AddRange(ingredientsToAdd);
    }


    public IRecipe FindRecipeAPI(string recipeName)
    {
        if (!_recipeManager.Recipes.Any(r => r.Name == recipeName))
        {
            return null;
        }
        return _recipeManager.Recipes.FirstOrDefault(r => r.Name == recipeName);
    }

    public IRecipe AddStepsAPI(IRecipe recipe, string inputSteps)
    {
        if (inputSteps == null)
        {
            return null;
        }
        
         recipe.Steps = inputSteps.Split('\n').ToList();
         return recipe;
    }
   
    
}