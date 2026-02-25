using Cooking_Project.Application.Adaptors;
using Cooking_Project.Application.Domain;
using Cooking_Project.Application.Ports;
using CookingAPI.DTO;
using CookingAPI.Mapping;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace CookingAPI_Tests;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using CookingAPI.Controllers;
using Moq;

[TestFixture]
public class RecipeManagerAPITests
{
    
    IRecipeManager _recipeManager;
    // ILogger<RecipeManagerAPI> _logger;
    IRecipeManagerAPI _recipeManagerAPI;
    Mock<IWebHostEnvironment> _webHostEnvironment;
    
    
    [SetUp]
    public void Setup()
    {
        
        var loggerController = new Mock<ILogger<RecipeController>>(); 
        var loggerManagerAPI = new Mock<ILogger<RecipeManagerAPI>>();
        var loggerAutoMapper = new Mock<ILogger<RecipeAutoMapper>>();
        //var loggerDBService = new Mock<ILogger<IRecipeDBService>>();
        
        var moq = new Mock<IInputProvider>();
        moq.SetupSequence(ip => ip.ReadInput(It.IsAny<string>()))
            .Returns("Pizza")
            .Returns("2");
            
        //Creating instance and then adding a recipe 
        //RecipeManager recipeManager = new RecipeManager(new IInputProviderTest(""));
        _recipeManager = new RecipeManager(moq.Object, new OutputProviderTest()) ;
        _recipeManager.AddRecipe();
        
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
        var recipeToCheck = _recipeManager.Recipes.First();
        //recipeManager.FindRecipe(out string recipeName);
        recipeToCheck.InputProvider = moq2.Object;
        recipeToCheck.AddIngredients("Pizza");
        
        //adding steps
        //string stepsInput = "\"1.Gently heat the milk and salt in a medium saucepan over a low heat for about 10 mins, stirring often, until it reaches \" +\n                     \"2.93 on a sugar thermometer. \" +\n                     \"3.Alternatively, watch the mixture carefully:\" +\n                     \"4.the milk should be consistently foaming and steaming but should not begin to boil and bubble, as this will scald it and af\" +\n                     \"5.fect the flavour.\"";
        var moq3 = new Mock<IInputProvider>();
        moq3.SetupSequence(ip => ip.ReadInput(It.IsAny<string>()))
            // .Returns("Thai Green Paste")
            // .Returns("Protein")
            // .Returns("Vegetables")
            .Returns("1.First do")
            .Returns("2.The do")
            .Returns("3.Finally do")
            .Returns("Done");
        
        //adding steps
        recipeToCheck.InputProvider = moq3.Object;
        recipeToCheck.AddSteps(("Pizza"));
       
        
        
        _webHostEnvironment = new Mock<IWebHostEnvironment>();

        _webHostEnvironment.Setup(e => e.WebRootPath)
            .Returns(Path.Combine(Directory.GetCurrentDirectory(), "TestWebRoot"));
        
        _recipeManagerAPI = new RecipeManagerAPI(_recipeManager, loggerManagerAPI.Object, _webHostEnvironment.Object);
    }
    
    [Test]
    public void FindIdRecipeAPI_ReturnsRecipe()
    {

        int idTest = 0;
        
        // var moq = new Mock<IInputProvider>();
        // moq.SetupSequence(ip => ip.ReadInput(It.IsAny<string>()))
        //     .Returns("Pizza")
        //     .Returns("2");
        
        // ((RecipeManager)recipeManager)._inputProvider = moq.Object;
        
        // ActionResult<IEnumerable<IngredientDTO>> result = recipeController.GetRecipe(recipeDTO.Name);
        var result = _recipeManagerAPI.FindIdRecipeAPI(idTest);

        // assert
        //Assert.IsInstanceOf<OkObjectResult>(result.Result);

        //var ok = result.Result as OkObjectResult;
        //Assert.IsNotNull(ok);

        //var recipeDto = ok.Value as RecipeDTO;
        Assert.IsNotNull(result);
        
        Assert.IsTrue(result.Name == "Pizza");
        Assert.IsTrue(result.Ingredients.Any(r => r.Name == "Mozzarella"));
        Assert.IsTrue(result.Ingredients.Any(r => r.Amount == 2));
        Assert.IsTrue(result.Ingredients.Any(r => r.Unit == "whole"));
        Assert.IsTrue(result.Steps.First() == "1.First do");
        Assert.IsTrue(result.Steps.Last() == "3.Finally do");
        
        
    }
    
}