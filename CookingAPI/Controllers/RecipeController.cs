using System.Collections.ObjectModel;
using Cooking_Project.Application.Domain;
using Microsoft.Extensions.Internal;

namespace CookingAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;



[ApiController]
[Route("api/v1/[RecipeController]")]
public class RecipeController: ControllerBase
{
    IRecipeManager _recipeManager;
    public RecipeController(IRecipeManager recipeManager)
    {
        _recipeManager = recipeManager;
    }
    [HttpGet]
    public ActionResult<IEnumerable<string>> GetRecipes()
    {
        var recipes = _recipeManager.GetAllRecipesAPI();
        // var recipes = new List<string>
        // {
        //     "Spaghetti Bolognese",
        //     "Chicken Curry",
        //     "Beef Stroganoff"
        // };
        return Ok(recipes);
    }
}