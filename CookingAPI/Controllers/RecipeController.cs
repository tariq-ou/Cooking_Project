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
        
        _logger.LogInformation($"Retrieved {recipesDTO.First().Name} recipes from manager");
        
        _logger.LogDebug("Mapping complete, returning DTOs");
        
        return Ok(recipesDTO);
    }
    
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("{recipeInput}/ingredients")]
    public ActionResult<IEnumerable<IngredientDTO>> GetIngredients(string recipeInput)
    {
       // string recipeName;
    
        var recipe = _recipeManagerAPI.FindRecipeAPI(recipeInput);
        
        _logger.LogInformation($"Retrieved {recipe.Name} recipes from manager");
        
        var ingredients = _recipeManagerAPI.GetAllIngredients(recipe);
        
        _logger.LogInformation($"Retrieved {ingredients.Count()} ingredients");

        var ingredientDTO = _recipeMapper.CreateIngredientListDTO(ingredients);
        
        _logger.LogInformation("Mapping complete, returning DTOs");
        return Ok(ingredientDTO);
    }
    
    
    [HttpGet("{id}/ingredientsid")]
    public ActionResult<IEnumerable<IngredientDTO>> GetIngredientsId(int id)
    {
        // string recipeName;
    
        var recipe = _recipeManagerAPI.FindIdRecipeAPI(id);
        
        
        _logger.LogInformation($"Retrieved {recipe.Name} recipes from manager");
        
        var ingredients = _recipeManagerAPI.GetAllIngredients(recipe);
        
        _logger.LogInformation($"Retrieved {ingredients.Count()} ingredients");

        var ingredientDTO = _recipeMapper.CreateIngredientListDTO(ingredients);
        
        _logger.LogInformation("Mapping complete, returning DTOs");
        return Ok(ingredientDTO);
    }
    
    
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("{recipeInput}")]
    public ActionResult<IEnumerable<IngredientDTO>> GetRecipe(string recipeInput)
    {
        string recipeName;
    
        var recipe = _recipeManagerAPI.FindRecipeAPI(recipeInput);
        
        _logger.LogInformation($"Retrieved {recipe.Name} recipes from manager");
        
        var recipeDTO = _recipeMapper.CreateRecipeDTO(recipe);
        
        _logger.LogInformation($"Created{recipeDTO.Name} recipe DTO");
        
        return Ok(recipeDTO);
    }
    
    [HttpGet("{id}/recipeid")]
    public ActionResult<IEnumerable<IngredientDTO>> GetRecipe(int id)
    {
        string recipeName;
    
        var recipe = _recipeManagerAPI.FindIdRecipeAPI(id);
        
        _logger.LogInformation($"Retrieved {recipe.Name} recipes from manager");
        
        var recipeDTO = _recipeMapper.CreateRecipeDTO(recipe);
        
        _logger.LogInformation($"Created{recipeDTO.Name} recipe DTO");
        
        return Ok(recipeDTO);
    }
    
    [HttpPost("addrecipe")]
    public IActionResult AddRecipe([FromBody] CreateRecipeDTO recipeInput)
    {
        string recipeName;
        
        //var recipeObjectCreated = _recipeMapper.CreateRecipeFromDTO(recipeInput);
        var ingredients = _recipeMapper.CreateIngredientList(recipeInput.Ingredients);
        
        _logger.LogInformation($"Creating {ingredients.Count()} ingredients");
        
        var recipe = _recipeManagerAPI.CreateRecipeNoId(recipeInput, ingredients);
        
        _logger.LogInformation($"Created {recipe.Name} recipe");
        
        _recipeRepositoryService.AddItemSave(recipe);
        
        _logger.LogInformation($"Saving to database");
      
        //REcipe obkect createed validation?
        
        return Ok();
    }
    
    
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpDelete("{recipeInput}")]
    public IActionResult DeleteRecipe(string recipeInput)
    {
        string recipeName;
        
        var recipe = _recipeManagerAPI.FindRecipeAPI(recipeInput);
        
        _logger.LogInformation($"Retrieved {recipe.Name} recipe from manager");
        
        _recipeRepositoryService.DeleteItemandNested(recipe);
        
        _logger.LogInformation($"Deleted {recipe.Name} recipe from database");
        
        _recipeManager.DeleteRecipe(recipe);
        
        _logger.LogInformation($"Deleted {recipe.Name} recipe from in memeory");
      
        //REcipe obkect createed validation?
        
        return Ok();
    }
    
    [HttpDelete("{id}/deleteid")]
    public IActionResult DeleteRecipe(int id)
    {
        string recipeName;
        
        var recipe = _recipeManagerAPI.FindIdRecipeAPI(id);
        
        _logger.LogInformation($"Retrieved {recipe.Name} recipe from manager");
        
        _recipeRepositoryService.DeleteItemandNested(recipe);
        
        _logger.LogInformation($"Deleted {recipe.Name} recipe from database");
        
        _recipeManager.DeleteRecipe(recipe);
        
        _logger.LogInformation($"Deleted {recipe.Name} recipe from in memeory");
      
        //REcipe obkect createed validation?
        
        return Ok();
    }
    
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpDelete("{recipeInput}/deleteallingredients")]
    public IActionResult DeleteAllRecipeIngredients(string recipeInput)
    {
        
        var recipe = _recipeManagerAPI.FindRecipeAPI(recipeInput);
        
        _logger.LogInformation($"Retrieved {recipe.Name} recipe from manager");
        
        _recipeManagerAPI.DeleteAllIngredientAPI(recipe);
        _logger.LogInformation($"Deleted Ingredients for {recipe.Name}");
        
        _recipeRepositoryService.AddNestedSave(recipe.Name, recipe.Ingredients);
        _logger.LogInformation($"Saving delete to database");
      
     
        
        return Ok();
    }
    
    [HttpDelete("{id}/deleteallingredientsid")]
    public IActionResult DeleteAllRecipeIngredients(int id)
    {
        
        var recipe = _recipeManagerAPI.FindIdRecipeAPI(id);
        
        _logger.LogInformation($"Retrieved {recipe.Name} recipe from manager");
        
        _recipeManagerAPI.DeleteAllIngredientAPI(recipe);
        _logger.LogInformation($"Deleted Ingredients for {recipe.Name}");
        
        _recipeRepositoryService.AddNestedSave(recipe.Name, recipe.Ingredients);
        _logger.LogInformation($"Saving delete to database");
      
     
        
        return Ok();
    }
    
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpDelete("{recipeInput}/steps")]
    public IActionResult DeleteRecipeSteps(string recipeInput)
    {
        
        var recipe = _recipeManagerAPI.FindRecipeAPI(recipeInput);
        _logger.LogInformation($"Retrieved {recipe.Name} recipe from manager");
        
        _recipeManagerAPI.DeleteStepsAPI(recipe);
        _logger.LogInformation($"Deleted steps from {recipe.Name}");
        
        _recipeRepositoryService.AddItemSave(recipe);
        _logger.LogInformation($"Saving Delete to database");
      
        
        
        return Ok();
    }
    
    [HttpDelete("{id}/stepsid")]
    public IActionResult DeleteRecipeSteps(int id)
    {
        
        var recipe = _recipeManagerAPI.FindIdRecipeAPI(id);
        _logger.LogInformation($"Retrieved {recipe.Name} recipe from manager");
        
        _recipeManagerAPI.DeleteStepsAPI(recipe);
        _logger.LogInformation($"Deleted steps from {recipe.Name}");
        
        _recipeRepositoryService.AddItemSave(recipe);
        _logger.LogInformation($"Saving Delete to database");
      
        
        
        return Ok();
    }
    
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpDelete("{recipeName}/ingredients")]
    public IActionResult DeleteRecipeIngredients(string recipeName, IEnumerable<string> ingredientsToDelete)
    {
        
        var recipe = _recipeManagerAPI.FindRecipeAPI(recipeName);
        _logger.LogInformation($"Retrieved {recipe.Name} recipe from manager");
        
        _recipeManagerAPI.DeleteIngredientAPI(recipe, ingredientsToDelete);
        _logger.LogInformation($"Deleted {ingredientsToDelete} from {recipe.Name} ");
        
        _recipeRepositoryService.AddNestedSave(recipe.Name, recipe.Ingredients);
        _logger.LogInformation($"Saving delete to database");
        
        return Ok();
    }
    
    [HttpDelete("{id}/ingredientsid")]
    public IActionResult DeleteRecipeIngredients(int id, IEnumerable<string> ingredientsToDelete)
    {
        
        var recipe = _recipeManagerAPI.FindIdRecipeAPI(id);
        _logger.LogInformation($"Retrieved {recipe.Name} recipe from manager");
        
        _recipeManagerAPI.DeleteIngredientAPI(recipe, ingredientsToDelete);
        _logger.LogInformation($"Deleted {ingredientsToDelete} from {recipe.Name} ");
        
        _recipeRepositoryService.AddNestedSave(recipe.Name, recipe.Ingredients);
        _logger.LogInformation($"Saving delete to database");
        
        return Ok();
    }
    
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpPost("{recipeName}/ingredients")]
    public IActionResult AddRecipeIngredient(string recipeName, [FromBody] IEnumerable<CreateIngredientDTO> ingredientsInput)
    {
       
        var recipe = _recipeManagerAPI.FindRecipeAPI(recipeName);
        _logger.LogInformation($"Retrieved {recipe.Name} recipe from manager");
        
        var ingredients = _recipeMapper.CreateIngredientList(ingredientsInput);
        _logger.LogInformation($"Created {ingredients.Count()} ingredients");

        _recipeManagerAPI.IngredientAddAPI(recipe,ingredients);
        _logger.LogInformation($"Added {ingredients.Count()} ingredients to {recipe.Name}");
        
        _recipeRepositoryService.AddNestedSave(recipe.Name, recipe.Ingredients);
        _logger.LogInformation($"Saving database");
      
        
        
        return Ok();
    }
    
    [HttpPost("{id}/ingredientsid")]
    public IActionResult AddRecipeIngredient(int id, [FromBody] IEnumerable<CreateIngredientDTO> ingredientsInput)
    {
       
        var recipe = _recipeManagerAPI.FindIdRecipeAPI(id);
        _logger.LogInformation($"Retrieved {recipe.Name} recipe from manager");
        
        var ingredients = _recipeMapper.CreateIngredientList(ingredientsInput);
        _logger.LogInformation($"Created {ingredients.Count()} ingredients");

        _recipeManagerAPI.IngredientAddAPI(recipe,ingredients);
        _logger.LogInformation($"Added {ingredients.Count()} ingredients to {recipe.Name}");
        
        _recipeRepositoryService.AddNestedSave(recipe.Name, recipe.Ingredients);
        _logger.LogInformation($"Saving database");
      
        
        
        return Ok();
    }
    
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpPost("{recipeName}/steps")]
    public IActionResult AddRecipeStep(string recipeName, string stepsInput)
    {
        
        var recipe = _recipeManagerAPI.FindRecipeAPI(recipeName);
        _logger.LogInformation($"Retrieved {recipe.Name} recipe from manager");

        _recipeManagerAPI.AddStepsAPI(recipe, stepsInput);
        _logger.LogInformation($"Added steps to {recipe.Name}");
        
        _recipeRepositoryService.AddItemSave(recipe);
        _logger.LogInformation($"Saving steps added to database");
      
        return Ok();
    }
    
    [HttpPost("{id}/stepsid")]
    public IActionResult AddRecipeStep(int id, string stepsInput)
    {
        
        var recipe = _recipeManagerAPI.FindIdRecipeAPI(id);
        _logger.LogInformation($"Retrieved {recipe.Name} recipe from manager");

        _recipeManagerAPI.AddStepsAPI(recipe, stepsInput);
        _logger.LogInformation($"Added steps to {recipe.Name}");
        
        _recipeRepositoryService.AddItemSave(recipe);
        _logger.LogInformation($"Saving steps added to database");
      
        return Ok();
    }

    [HttpPost("export")]
    public IActionResult ExportJsonRecipes()
    {
        _recipeRepositoryService.DBToExport();
        _logger.LogInformation("Exported JSON Recipes");
        return Ok();
    }
    
    [HttpPost("import")]
    public IActionResult ImportJsonRecipes()
    {
        _recipeRepositoryService.ImportToDB();
        _logger.LogInformation("Imported JSON Recipes");
        return Ok();
    }
    
    
    
}