using System.Collections.ObjectModel;
using Cooking_Project.Application.Domain;
using Cooking_Project.Application.Ports;
using Cooking_Project.Application.Services;
using CookingAPI.DTO;
using CookingAPI.Mapping;
using Microsoft.Extensions.Internal;

namespace CookingAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AutoMapper;



[ApiController]
[Route("api/v1/[controller]")]
public class RecipeController: ControllerBase
{
    readonly IRecipeManager _recipeManager;
    readonly IRecipeMapper _recipeMapper;
    readonly  IRecipeManagerAPI _recipeManagerAPI;
    readonly IRecipeDBService _recipeRepositoryService;
    private readonly ILogger<RecipeController> _logger;
    public RecipeController(IRecipeManager recipeManager, IRecipeMapper recipeMapper, IRecipeManagerAPI recipeManagerAPI, IRecipeDBService recipeRepositoryService, ILogger<RecipeController> logger)
    {
        _recipeManager = recipeManager;
        _recipeMapper = recipeMapper;
        _recipeManagerAPI = recipeManagerAPI;
        _recipeRepositoryService = recipeRepositoryService;
        _logger = logger;
    }
    
    [HttpGet("GetRecipes")]
    public ActionResult<IEnumerable<RecipeDTO>> GetRecipes()
    {
        
        _logger.LogInformation("GetRecipes called");
        
        var recipes = _recipeManagerAPI.GetAllRecipes();
        
        _logger.LogInformation($"Retrieved {recipes.Count()} recipes from manager");
        
        var recipesDTO =_recipeMapper.CreateRecipeListDTO(recipes);
        
        _logger.LogDebug("Mapping complete, returning DTOs");
        
        return Ok(recipesDTO);
    }
    
    [HttpGet("{recipeInput}/ingredients")]
    public ActionResult<IEnumerable<IngredientDTO>> GetIngredients(string recipeInput)
    {
       // string recipeName;
    
        var recipe = _recipeManagerAPI.FindRecipeAPI(recipeInput);
        
        var ingredients = _recipeManagerAPI.GetAllIngredients(recipe);

        var ingredientDTO = _recipeMapper.CreateIngredientListDTO(ingredients);
        return Ok(ingredientDTO);
    }
    
    [HttpGet("{recipeInput}")]
    public ActionResult<IEnumerable<IngredientDTO>> GetRecipe(string recipeInput)
    {
        string recipeName;
    
        var recipe = _recipeManagerAPI.FindRecipeAPI(recipeInput);
        
        var recipeDTO = _recipeMapper.CreateRecipeDTO(recipe);
        
        return Ok(recipeDTO);
    }
    
    [HttpPost("addrecipe")]
    public IActionResult AddRecipe([FromBody] RecipeDTO recipeInput)
    {
        string recipeName;
        
        //var recipeObjectCreated = _recipeMapper.CreateRecipeFromDTO(recipeInput);
        var ingredients = _recipeMapper.CreateIngredientList(recipeInput.Ingredients);
        
        var recipe = _recipeManagerAPI.CreateRecipe(recipeInput, ingredients);
        _recipeRepositoryService.AddItemSave(recipe);
      
        //REcipe obkect createed validation?
        
        return Ok();
    }
    
    [HttpDelete("{recipeInput}")]
    public IActionResult DeleteRecipe(string recipeInput)
    {
        string recipeName;
        
        var recipe = _recipeManagerAPI.FindRecipeAPI(recipeInput);
        _recipeRepositoryService.DeleteItemandNested(recipe);
        _recipeManager.DeleteRecipe(recipe);
      
        //REcipe obkect createed validation?
        
        return Ok();
    }
    
    [HttpDelete("deleteallingredients")]
    public IActionResult DeleteAllRecipeIngredients([FromBody] RecipeDTO recipeInput)
    {
        //string recipeName;
        //Keep this method
        var recipe = _recipeManagerAPI.FindRecipeAPI(recipeInput.Name);
        _recipeManagerAPI.DeleteAllIngredientAPI(recipe, recipeInput.Ingredients);
        _recipeRepositoryService.AddNestedSave(recipe.Name, recipe.Ingredients);
      
        //REcipe obkect createed validation?
        
        return Ok();
    }
    
    [HttpDelete("{recipeName}/steps")]
    public IActionResult DeleteRecipeSteps(string recipeInput)
    {
        //string recipeName;
        
        var recipe = _recipeManagerAPI.FindRecipeAPI(recipeInput);
        _recipeManagerAPI.DeleteStepsAPI(recipe);
        _recipeRepositoryService.AddItemSave(recipe);
      
        //REcipe obkect createed validation?
        
        return Ok();
    }
    
    [HttpDelete("{recipeName}/ingredients")]
    public IActionResult DeleteRecipeIngredients(string recipeName, IEnumerable<string> ingredientsToDelete)
    {
        //string recipeName;
        //Keep this method
        var recipe = _recipeManagerAPI.FindRecipeAPI(recipeName);
        _recipeManagerAPI.DeleteIngredientAPI(recipe, ingredientsToDelete);
        _recipeRepositoryService.AddNestedSave(recipe.Name, recipe.Ingredients);
      
        //REcipe obkect createed validation?
        
        return Ok();
    }
    
    [HttpPost("{recipeName}/ingredients")]
    public IActionResult AddRecipeIngredient(string recipeName, [FromBody] IEnumerable<IngredientDTO> ingredientsInput)
    {
        //string recipeName;
        
        //var recipeObjectCreated = _recipeMapper.CreateRecipeFromDTO(recipeInput);
        var recipe = _recipeManagerAPI.FindRecipeAPI(recipeName);
        //var recipe = _recipeManagerAPI.FindRecipeAPI(recipeName);
        var ingredients = _recipeMapper.CreateIngredientList(ingredientsInput);

        _recipeManagerAPI.IngredientAddAPI(recipe,ingredients);
        _recipeRepositoryService.AddNestedSave(recipe.Name, recipe.Ingredients);
      
        //REcipe obkect createed validation?
        
        return Ok();
    }
    
    [HttpPost("{recipeName}/steps")]
    public IActionResult AddRecipeStep(string recipeName, string stepsInput)
    {
        //string recipeName;
        
        //var recipeObjectCreated = _recipeMapper.CreateRecipeFromDTO(recipeInput);
        var recipe = _recipeManagerAPI.FindRecipeAPI(recipeName);
        //var recipe = _recipeManagerAPI.FindRecipeAPI(recipeName);
        //var ingredients = _recipeMapper.CreateIngredientList(ingredientsInput);

        _recipeManagerAPI.AddStepsAPI(recipe, stepsInput);
        _recipeRepositoryService.AddItemSave(recipe);
      
        //REcipe obkect createed validation?
        
        return Ok();
    }

    [HttpPost("export")]
    public IActionResult ExportJsonRecipes()
    {
        _recipeRepositoryService.DBToExport();
        return Ok();
    }
    
    [HttpPost("import")]
    public IActionResult ImportJsonRecipes()
    {
        _recipeRepositoryService.ImportToDB();
        return Ok();
    }
    
    
    
}