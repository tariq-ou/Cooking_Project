using Cooking_Project.Application.Adaptors;
using Cooking_Project.Application.Domain;
using Cooking_Project.Application.Ports;
using Cooking_Project.Application.Services;

namespace CookingAPI_Tests;

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using CookingAPI.Controllers;
using Moq;

public class Tests
{
    private RecipeManager recipeManager;
    
    [SetUp]
    public void Setup()
    {
        var moq = new Mock<IInputProvider>();
        moq.SetupSequence(ip => ip.ReadInput(It.IsAny<string>()))
            .Returns("Pizza")
            .Returns("2");
            
        //Creating instance and then adding a recipe 
        //RecipeManager recipeManager = new RecipeManager(new IInputProviderTest(""));
        recipeManager = new RecipeManager(moq.Object, new OutputProviderTest()) ;
        recipeManager.AddRecipe();
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
        var controller = new RecipeController(recipeManager);

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