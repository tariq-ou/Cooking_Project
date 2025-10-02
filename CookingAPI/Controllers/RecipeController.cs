using System.Collections.ObjectModel;
using Microsoft.Extensions.Internal;

namespace CookingAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


[ApiController]
[Route("api/v1/[RecipeController]")]
public class RecipeController: ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<string>> GetRecipes()
    {
        var recipes = new List<string>
        {
            "Spaghetti Bolognese",
            "Chicken Curry",
            "Beef Stroganoff"
        };
        return Ok(recipes);
    }
}