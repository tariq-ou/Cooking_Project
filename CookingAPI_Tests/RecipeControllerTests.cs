using AutoMapper;
using Cooking_Project.Application.Adaptors;
using Cooking_Project.Application.Domain;
using Cooking_Project.Application.Ports;
using Cooking_Project.Application.Services;
using CookingAPI.DTO;
using CookingAPI.Mapping;
using Microsoft.Extensions.Logging;
using NUnit.Framework.Internal;
using ILogger = NUnit.Framework.Internal.ILogger;
using System.Collections;
using System.Runtime.CompilerServices;
namespace CookingAPI_Tests;

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using CookingAPI.Controllers;
using Moq;

public class Tests
{
    private IRecipeManager recipeManager;
    private IRecipeMapper recipeMapper;
    private IRecipeManagerAPI recipeManagerAPI;
    
    RecipeController recipeController;
    
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
        
        // var logger = new Mock<ILogger>();
        // var config = new MapperConfiguration(cfg => { cfg.AddProfile<RecipeProfile>();},LoggerFactory.Create(builder => builder.AddConsole())
        //     //LoggerFactory.Create()  ;   // or cfg.AddMaps(typeof(RecipeProfile).Assembly);
        // );
        //
        // // creates the mapper object
        // mapper = config.CreateMapper();
        
        var moq2 = new Mock<IInputProvider>();
        moq2.SetupSequence(ip => ip.ReadInput(It.IsAny<string>()))
            // .Returns("Thai Green Paste")
            // .Returns("Protein")
            // .Returns("Vegetables")
            .Returns("Mozzarella")
            .Returns("2")
            .Returns("whole")
            .Returns("Done");
        
        //recipeManager._inputProvider = new IInputProviderTest("Piiza");
        var recipeToCheck = recipeManager.Recipes.First();
        //recipeManager.FindRecipe(out string recipeName);
        recipeToCheck.InputProvider = moq2.Object;
        recipeToCheck.AddIngredients("Pizza");
        
        recipeManagerAPI = new RecipeManagerAPI(recipeManager);
        
        var logger = new Mock<ILogger>();
        var config = new MapperConfiguration(cfg => { cfg.AddProfile<RecipeProfile>();},LoggerFactory.Create(builder => builder.AddConsole())
            //LoggerFactory.Create()  ;   // or cfg.AddMaps(typeof(RecipeProfile).Assembly);
        );

        recipeMapper = new RecipeAutoMapper(config.CreateMapper());
        // creates the mapper object
        //recipeMapper._mapper = config.CreateMapper();
        
        recipeController = new RecipeController(recipeManager, recipeMapper, recipeManagerAPI);
        
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
        

        // act
        ActionResult<IEnumerable<RecipeDTO>> result = recipeController.GetRecipes();

        // assert
        Assert.IsInstanceOf<OkObjectResult>(result.Result);

        var ok = result.Result as OkObjectResult;
        Assert.IsNotNull(ok);

        var recipes = ok.Value as List<RecipeDTO>;
        Assert.IsNotNull(recipes);

        Assert.IsTrue(recipes.Any(r => r.Name == "Pizza"));
        Assert.IsTrue(recipes.Any(r => r.ingredients.First().Name == "Mozzarella"));
        Assert.IsTrue(recipes.Any(r => r.ingredients.First().Amount == 2));
        Assert.IsTrue(recipes.Any(r => r.ingredients.First().Unit == "whole"));
        
        CollectionAssert.IsNotEmpty(recipes);
    }
    
    [Test]
    public void GetIngredients_ReturnsOk_WithIngredientList()
    {
       
        // act
        RecipeDTO recipeDTO = new RecipeDTO();
        recipeDTO.Name = "Pizza";
        
        var moq = new Mock<IInputProvider>();
        moq.SetupSequence(ip => ip.ReadInput(It.IsAny<string>()))
            .Returns("Pizza")
            .Returns("2");
        
        ((RecipeManager)recipeManager)._inputProvider = moq.Object;
        
        ActionResult<IEnumerable<IngredientDTO>> result = recipeController.GetIngredients(recipeDTO);

        // assert
        Assert.IsInstanceOf<OkObjectResult>(result.Result);

        var ok = result.Result as OkObjectResult;
        Assert.IsNotNull(ok);

        var ingredients = ok.Value as List<IngredientDTO>;
        Assert.IsNotNull(ingredients);

        //Assert.IsTrue(ingredients.Any(r => r.Name == "Pizza"));
        Assert.IsTrue(ingredients.Any(i => i.Name == "Mozzarella"));
        Assert.IsTrue(ingredients.Any(i => i.Amount == 2));
        Assert.IsTrue(ingredients.Any(i => i.Unit == "whole"));
        
        CollectionAssert.IsNotEmpty(ingredients);
    }
}