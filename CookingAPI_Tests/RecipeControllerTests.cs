namespace CookingAPI_Tests;

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using CookingAPI.Controllers;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    //[Test]
    public void Test1()
    {
        Assert.Pass();
    }
    
    [Test]
    public void GetRecipes_ReturnsOk_WithRecipeList()
    {
        // arrange
        var controller = new RecipeController();

        // act
        ActionResult<IEnumerable<string>> result = controller.GetRecipes();

        // assert
        Assert.IsInstanceOf<OkObjectResult>(result.Result);

        var ok = result.Result as OkObjectResult;
        Assert.IsNotNull(ok);

        var recipes = ok.Value as IEnumerable<string>;
        Assert.IsNotNull(recipes);

        CollectionAssert.Contains(recipes, "Spaghetti Bolognese");
        CollectionAssert.IsNotEmpty(recipes);
    }
}