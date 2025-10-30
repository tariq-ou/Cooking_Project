using System.Collections.ObjectModel;
using Cooking_Project.Application.Domain;
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
    public RecipeController(IRecipeManager recipeManager, IRecipeMapper recipeMapper, IRecipeManagerAPI recipeManagerAPI)
    {
        _recipeManager = recipeManager;
        _recipeMapper = recipeMapper;
        _recipeManagerAPI = recipeManagerAPI;
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
        
        var recipesDTO =_recipeMapper.CreateRecipeList(recipes);
        return Ok(recipesDTO);
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<IngredientDTO>> GetIngredients(RecipeDTO recipeInput)
    {
        string recipeName;
    
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

        var ingredientDTO = _recipeMapper.CreateIngredientList(ingredients);
        return Ok(ingredientDTO);
    }
}