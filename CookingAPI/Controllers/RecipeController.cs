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
    readonly IMapper _mapper;
    public RecipeController(IRecipeManager recipeManager, IMapper mapper)
    {
        _recipeManager = recipeManager;
        _mapper = mapper;
    }
    [HttpGet]
    public ActionResult<IEnumerable<RecipeDTO>> GetRecipes()
    {
        var recipes = _recipeManager.GetAllRecipesAPI();
   
        
        //validation of mapper
        _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        var recipesDTO = new List<RecipeDTO>();
        
        foreach (IRecipe recipe in recipes)
        {
            recipesDTO.Add(_mapper.Map<RecipeDTO>(recipe));
        }
        return Ok(recipesDTO);
    }
}