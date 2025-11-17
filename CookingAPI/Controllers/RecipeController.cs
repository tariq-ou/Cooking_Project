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
[Route("api/v1/[RecipeController]")]
public class RecipeController: ControllerBase
{
    readonly IRecipeManager _recipeManager;
    readonly IRecipeMapper _recipeMapper;
    readonly  IRecipeManagerAPI _recipeManagerAPI;
    readonly IRecipeService _recipeRepositoryService;
    public RecipeController(IRecipeManager recipeManager, IRecipeMapper recipeMapper, IRecipeManagerAPI recipeManagerAPI, IRecipeService recipeRepositoryService)
    {
        _recipeManager = recipeManager;
        _recipeMapper = recipeMapper;
        _recipeManagerAPI = recipeManagerAPI;
        _recipeRepositoryService = recipeRepositoryService;
    }
    [HttpGet]
    public ActionResult<IEnumerable<RecipeDTO>> GetRecipes()
    {
        var recipes = _recipeManagerAPI.GetAllRecipes();
   
        
        //validation of mapper
        // _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        // var recipesDTO = new List<RecipeDTO>();
        //
        // foreach (IRecipe recipe in recipes)
        // {
        //     recipesDTO.Add(_mapper.Map<RecipeDTO>(recipe));
        // }
        
        var recipesDTO =_recipeMapper.CreateRecipeListDTO(recipes);
        return Ok(recipesDTO);
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<IngredientDTO>> GetIngredients([FromBody] RecipeDTO recipeInput)
    {
       // string recipeName;
    
        var recipe = _recipeManager.FindRecipe(out recipeInput.Name);
        var ingredients = _recipeManagerAPI.GetAllIngredients(recipe);
        
        //validation of mapper
        // _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        // var ingredientDTO = new List<IngredientDTO>();
        //
        // foreach (Ingredient ingredient in Ingredients)
        // {
        //     ingredientDTO.Add(_mapper.Map<IngredientDTO>(ingredient));
        // }

        var ingredientDTO = _recipeMapper.CreateIngredientListDTO(ingredients);
        return Ok(ingredientDTO);
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<IngredientDTO>> GetRecipe([FromBody] RecipeDTO recipeInput)
    {
        string recipeName;
    
        var recipe = _recipeManager.FindRecipe(out recipeInput.Name);
        var recipeDTO = _recipeMapper.CreateRecipeDTO(recipe);
        
        //validation of mapper
        // _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        // var ingredientDTO = new List<IngredientDTO>();
        //
        // foreach (Ingredient ingredient in Ingredients)
        // {
        //     ingredientDTO.Add(_mapper.Map<IngredientDTO>(ingredient));
        // }

        //var stepsDTO = _recipeMapper.CreateIngredientList(ingredients);
        return Ok(recipeDTO);
    }
    
    [HttpPost]
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
    
    [HttpDelete]
    public IActionResult DeleteRecipe([FromBody] RecipeDTO recipeInput)
    {
        string recipeName;
        
        var recipe = _recipeManager.FindRecipe(out recipeInput.Name);
        _recipeRepositoryService.DeleteItemandNested(recipe);
        _recipeManager.DeleteRecipe(recipe);
      
        //REcipe obkect createed validation?
        
        return Ok();
    }
    
    [HttpDelete]
    public IActionResult DeleteAllRecipeIngredients([FromBody] RecipeDTO recipeInput)
    {
        //string recipeName;
        //Keep this method
        var recipe = _recipeManager.FindRecipe(out recipeInput.Name);
        _recipeManagerAPI.DeleteIngredientAPI(recipe, recipeInput.Ingredients);
        _recipeRepositoryService.AddNestedSave(recipe.Name, recipe.Ingredients);
      
        //REcipe obkect createed validation?
        
        return Ok();
    }
    
    [HttpDelete]
    public IActionResult DeleteRecipeSteps([FromBody] RecipeDTO recipeInput)
    {
        //string recipeName;
        
        var recipe = _recipeManager.FindRecipe(out recipeInput.Name);
        _recipeManagerAPI.DeleteStepsAPI(recipe);
        _recipeRepositoryService.AddItemSave(recipe);
      
        //REcipe obkect createed validation?
        
        return Ok();
    }
    
}