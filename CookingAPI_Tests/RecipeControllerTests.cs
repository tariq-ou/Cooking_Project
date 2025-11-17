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
using IInputProviderTest = Cooking_Project.Application.Adaptors.IInputProviderTest;

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
    private IRecipeService recipeService;
    private RecipeDTO recipeInputDto;
    
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
       
        
        
        
        recipeManagerAPI = new RecipeManagerAPI(recipeManager);
        
        var logger = new Mock<ILogger>();
        var config = new MapperConfiguration(cfg => { cfg.AddProfile<RecipeProfile>();},LoggerFactory.Create(builder => builder.AddConsole())
            //LoggerFactory.Create()  ;   // or cfg.AddMaps(typeof(RecipeProfile).Assembly);
        );

        recipeMapper = new RecipeAutoMapper(config.CreateMapper());
        // creates the mapper object
        //recipeMapper._mapper = config.CreateMapper();

        recipeService = new RecipeServiceTest();
        
        recipeController = new RecipeController(recipeManager, recipeMapper, recipeManagerAPI, recipeService);
        
        //creating Recipe DTO
        recipeInputDto = new RecipeDTO();
        recipeInputDto.Name = "Curry";
        recipeInputDto.Servings = 3;
        IngredientDTO ingredientDtoOne = new IngredientDTO
        {
            Name = "Flour",
            Unit = "grams",
            Amount = 50
        };
        
        IngredientDTO ingredientDtoTwo = new IngredientDTO
        {
            Name = "Curry Powder",
            Unit = "tbs",
            Amount = 2
        };
        recipeInputDto.Ingredients = new List<IngredientDTO>{ingredientDtoOne, ingredientDtoTwo};
        
        string stepOne = "1";
        string stepTwo = "2";
        string stepThree = "3";
        recipeInputDto.Steps = new List<string>{stepOne, stepTwo, stepThree};
        
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
        Assert.IsTrue(recipes.Any(r => r.Ingredients.First().Name == "Mozzarella"));
        Assert.IsTrue(recipes.Any(r => r.Ingredients.First().Amount == 2));
        Assert.IsTrue(recipes.Any(r => r.Ingredients.First().Unit == "whole"));
        
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
    
    [Test]
    public void GetRecipe_ReturnsOk()
    {
       
        // act
        RecipeDTO recipeDTO = new RecipeDTO();
        recipeDTO.Name = "Pizza";
        
        var moq = new Mock<IInputProvider>();
        moq.SetupSequence(ip => ip.ReadInput(It.IsAny<string>()))
            .Returns("Pizza")
            .Returns("2");
        
        ((RecipeManager)recipeManager)._inputProvider = moq.Object;
        
        ActionResult<IEnumerable<IngredientDTO>> result = recipeController.GetRecipe(recipeDTO);

        // assert
        Assert.IsInstanceOf<OkObjectResult>(result.Result);

        var ok = result.Result as OkObjectResult;
        Assert.IsNotNull(ok);

        var recipeDto = ok.Value as RecipeDTO;
        Assert.IsNotNull(recipeDTO);
        
        Assert.IsTrue(recipeDto.Name == "Pizza");
        Assert.IsTrue(recipeDto.Ingredients.Any(r => r.Name == "Mozzarella"));
        Assert.IsTrue(recipeDto.Ingredients.Any(r => r.Amount == 2));
        Assert.IsTrue(recipeDto.Ingredients.Any(r => r.Unit == "whole"));
        Assert.IsTrue(recipeDto.Steps.First() == "1.First do");
        Assert.IsTrue(recipeDto.Steps.Last() == "3.Finally do");
        
        
    }
    
    [Test]
    public void PutRecipe_ReturnsOk()
    {
       
        // act
        recipeController.AddRecipe(recipeInputDto);
        
        ((RecipeManager)recipeManager)._inputProvider = new IInputProviderTest("Curry");
        var recipeToCheck = recipeManager.FindRecipe(out recipeInputDto.Name);
        
        Assert.IsTrue(recipeToCheck.Name == "Curry");
        Assert.IsTrue(recipeToCheck.Ingredients.Any(r => r.Name == "Curry Powder"));
        Assert.IsTrue(recipeToCheck.Ingredients.Any(r => r.Amount == 2));
        Assert.IsTrue(recipeToCheck.Ingredients.Any(r => r.Unit == "tbs"));
        Assert.IsTrue(recipeToCheck.Steps.First() == "1");
        Assert.IsTrue(recipeToCheck.Steps.Last() == "3");
        
        
    }
    
    [Test]
    public void DeleteRecipe_ReturnsOk()
    {
       
        //first add recipe using previous add recipe method then delete it
        recipeController.AddRecipe(recipeInputDto);
        
        ((RecipeManager)recipeManager)._inputProvider = new IInputProviderTest("Curry");
         var recipeToCheck = recipeManager.FindRecipe(out recipeInputDto.Name);
        
        Assert.IsTrue(recipeToCheck.Name == "Curry");
        Assert.IsTrue(recipeToCheck.Ingredients.Any(r => r.Name == "Curry Powder"));
        Assert.IsTrue(recipeToCheck.Ingredients.Any(r => r.Amount == 2));
        Assert.IsTrue(recipeToCheck.Ingredients.Any(r => r.Unit == "tbs"));
        Assert.IsTrue(recipeToCheck.Steps.First() == "1");
        Assert.IsTrue(recipeToCheck.Steps.Last() == "3");
        
        // time to delete
        
         recipeController.DeleteRecipe(recipeInputDto);
        var recipeToDelete = recipeManager.FindRecipe(out recipeInputDto.Name);
        //
         Assert.IsNull(recipeToDelete);
        
        // Assert.IsFalse(recipeToDelete.Name == "Curry");
        // Assert.IsFalse(recipeToDelete.Ingredients.Any(r => r.Name == "Curry Powder"));
        // Assert.IsFalse(recipeToDelete.Ingredients.Any(r => r.Amount == 2));
        // Assert.IsFalse(recipeToDelete.Ingredients.Any(r => r.Unit == "tbs"));
        // Assert.IsFalse(recipeToDelete.Steps.First() == "1");
        // Assert.IsFalse(recipeToDelete.Steps.Last() == "3");
        
        
    }
    
    [Test]
    public void DeleteAllRecipeIngredients_ReturnsOk()
    {
       
        //first add recipe using previous add recipe method then delete it
        recipeController.AddRecipe(recipeInputDto);
        
        ((RecipeManager)recipeManager)._inputProvider = new IInputProviderTest("Curry");
        var recipeToCheck = recipeManager.FindRecipe(out recipeInputDto.Name);
        
        Assert.IsTrue(recipeToCheck.Name == "Curry");
        Assert.IsTrue(recipeToCheck.Ingredients.Any(r => r.Name == "Curry Powder"));
        Assert.IsTrue(recipeToCheck.Ingredients.Any(r => r.Amount == 2));
        Assert.IsTrue(recipeToCheck.Ingredients.Any(r => r.Unit == "tbs"));
        Assert.IsTrue(recipeToCheck.Steps.First() == "1");
        Assert.IsTrue(recipeToCheck.Steps.Last() == "3");
        
        // time to delete
        
        recipeController.DeleteAllRecipeIngredients(recipeInputDto);
        var recipeToDeleteIngredients = recipeManager.FindRecipe(out recipeInputDto.Name);
        //
        Assert.IsNotNull(recipeToDeleteIngredients);
        
        Assert.IsTrue(recipeToDeleteIngredients.Name == "Curry");
        Assert.IsFalse(recipeToDeleteIngredients.Ingredients.Any(r => r.Name == "Curry Powder"));
        Assert.IsFalse(recipeToDeleteIngredients.Ingredients.Any(r => r.Amount == 2));
        Assert.IsFalse(recipeToDeleteIngredients.Ingredients.Any(r => r.Unit == "tbs"));
        Assert.IsTrue(recipeToDeleteIngredients.Steps.First() == "1");
        Assert.IsTrue(recipeToDeleteIngredients.Steps.Last() == "3");
        
        
    }

    
    [Test]
    public void DeleteRecipeSteps_ReturnsOk()
    {
       
        //first add recipe using previous add recipe method then delete it
        recipeController.AddRecipe(recipeInputDto);
        
        ((RecipeManager)recipeManager)._inputProvider = new IInputProviderTest("Curry");
        var recipeToCheck = recipeManager.FindRecipe(out recipeInputDto.Name);
        
        Assert.IsTrue(recipeToCheck.Name == "Curry");
        Assert.IsTrue(recipeToCheck.Ingredients.Any(r => r.Name == "Curry Powder"));
        Assert.IsTrue(recipeToCheck.Ingredients.Any(r => r.Amount == 2));
        Assert.IsTrue(recipeToCheck.Ingredients.Any(r => r.Unit == "tbs"));
        Assert.IsTrue(recipeToCheck.Steps.First() == "1");
        Assert.IsTrue(recipeToCheck.Steps.Last() == "3");
        
        // time to delete
        
        recipeController.DeleteRecipeSteps(recipeInputDto);
        var recipeToDeleteSteps = recipeManager.FindRecipe(out recipeInputDto.Name);
        //
        Assert.IsNotNull(recipeToDeleteSteps);
        
        Assert.IsTrue(recipeToDeleteSteps.Name == "Curry");
        Assert.IsTrue(recipeToDeleteSteps.Ingredients.Any(r => r.Name == "Curry Powder"));
        Assert.IsTrue(recipeToDeleteSteps.Ingredients.Any(r => r.Amount == 2));
        Assert.IsTrue(recipeToDeleteSteps.Ingredients.Any(r => r.Unit == "tbs"));
        Assert.IsEmpty(recipeToDeleteSteps.Steps);


    }
}