using NUnit.Framework;
using Cooking_Project.Application.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using Cooking_Project_Tests.PortsTest;
using Cooking_Project.Application.Ports;
using Moq;
using Cooking_Project.Application.Services;
using NuGet.Frameworks;

namespace Cooking_Project_Tests.ServiceTests;

public class RecipeTest
{
    [SetUp]
    public void Setup()
    {
        // var mockIInputProvider = new Mock<IInputProvider>();
        // recipeManager = new RecipeManager(mockIInputProvider.Object);
        //private RecipeManager recipeManger = new RecipeManager(new IInputProviderTest());
    }

    [Test]
    public void AddIngredientsTest()
    {
        var moq = new Mock<IInputProvider>();
        moq.SetupSequence(ip => ip.ReadInput(It.IsAny<string>()))
            .Returns("Thai Green Curry")
            .Returns("2");
            
        //Creating instance and then adding a recipe 
        //RecipeManager recipeManager = new RecipeManager(new IInputProviderTest(""));
        RecipeManager recipeManager = new RecipeManager(moq.Object);
        recipeManager.AddRecipe();
        
        // RecipeManager recipeManager = new RecipeManager(new IInputProviderTest("Thai Green Curry"));
        // recipeManager.AddRecipe();
        
        //creating a moq sequence so we can pass through many input for the loop in the add ingredients method
        var moq2 = new Mock<IInputProvider>();
        moq2.SetupSequence(ip => ip.ReadInput(It.IsAny<string>()))
            // .Returns("Thai Green Paste")
            // .Returns("Protein")
            // .Returns("Vegetables")
            .Returns("Thai Green Paste")
            .Returns("2")
            .Returns("tbsp")
            .Returns("Done");
        
        // var moq2 = new Mock<IInputProvider>();
        // moq2.SetupSequence(ip => ip.ReadInput(It.IsAny<string>()))
        //     .Returns("Thai Green Curry");

        recipeManager._inputProvider = new IInputProviderTest("Thai Green Curry");
        var recipeToCheck = recipeManager.FindRecipe(out string recipeName);
        recipeToCheck.InputProvider = moq2.Object;
        recipeToCheck.AddIngredients("Thai Green Curry");
       
        
        // Assert.AreEqual("Thai Green Curry", recipeToCheck.Name);
        // Assert.AreEqual(3, recipeToCheck.Ingredients.Count);
        // Assert.Contains("Thai Green Paste", recipeToCheck.Ingredients);
        // Assert.Contains("Protein", recipeToCheck.Ingredients);
        // Assert.Contains("Vegetables", recipeToCheck.Ingredients);
        
        Assert.AreEqual("Thai Green Curry", recipeToCheck.Name);
        // below is 2 as it counts done as a  ingredient which needs to be fixed
        Assert.AreEqual(1, recipeToCheck.Ingredients.Count);
        Assert.AreEqual("Thai Green Paste", recipeToCheck.Ingredients.First().Name);
        Assert.AreEqual(2, recipeToCheck.Ingredients.First().Amount);
        Assert.AreEqual("tbsp", recipeToCheck.Ingredients.First().Unit);
        

    }

    [Test]
    public void DeleteIngredientsTest()
    {
        // RecipeManager recipeManager = new RecipeManager(new IInputProviderTest("Pizza"));
        // recipeManager.AddRecipe();
        
        var moq = new Mock<IInputProvider>();
        moq.SetupSequence(ip => ip.ReadInput(It.IsAny<string>()))
            .Returns("Pizza")
            .Returns("2");
            
        //Creating instance and then adding a recipe 
        //RecipeManager recipeManager = new RecipeManager(new IInputProviderTest(""));
        RecipeManager recipeManager = new RecipeManager(moq.Object);
        recipeManager.AddRecipe();

        
        var moq2 = new Mock<IInputProvider>();
        
        moq2.SetupSequence(ip => ip.ReadInput(It.IsAny<string>()))
            .Returns("Tomato Sause")
            .Returns("500")
            .Returns("grams")
            .Returns("Mozorella")
            .Returns("1000")
            .Returns("grams")
            .Returns("Done"); 
        
        recipeManager._inputProvider = new IInputProviderTest("Pizza");
        var recipeToCheck = recipeManager.FindRecipe(out string recipeName);
        recipeToCheck.InputProvider = moq2.Object;
        recipeToCheck.AddIngredients("Pizza");
        
        var moq3 = new Mock<IInputProvider>();
        
        //Creating a second moq as it seems once the sequence is used once it cant be used again?
         moq3.SetupSequence(ip => ip.ReadInput(It.IsAny<string>()))
             .Returns("Tomato Sause")
             .Returns("Done");
        recipeToCheck.InputProvider = moq3.Object;
        recipeToCheck.IngredientDelete("Pizza");
        
        Assert.AreEqual("Pizza", recipeToCheck.Name);
        Assert.AreEqual(1, recipeToCheck.Ingredients.Count);
        Assert.AreEqual("Mozorella", recipeToCheck.Ingredients.First().Name);


    }

    [Test]
    public void DeleteAllIngredientsTest()
    {
        var moq = new Mock<IInputProvider>();
        moq.SetupSequence(ip => ip.ReadInput(It.IsAny<string>()))
            .Returns("Pizza")
            .Returns("2");
        
        RecipeManager recipeManager = new RecipeManager(moq.Object);
        recipeManager.AddRecipe();
        
        var moq2 = new Mock<IInputProvider>();
        
        moq2.SetupSequence(ip => ip.ReadInput(It.IsAny<string>()))
            .Returns("Tomato Sause")
            .Returns("500")
            .Returns("grams")
            .Returns("Mozorella")
            .Returns("1000")
            .Returns("grams")
            .Returns("Done");
        
        recipeManager._inputProvider = new IInputProviderTest("Pizza");
        var recipeToCheck = recipeManager.FindRecipe(out string recipeName);
        recipeToCheck.InputProvider = moq2.Object;
        recipeToCheck.AddIngredients("Pizza");
        
        recipeToCheck.InputProvider = new IInputProviderTest("all");
        recipeToCheck.IngredientDelete("Pizza");
        
        Assert.AreEqual("Pizza", recipeToCheck.Name);
        Assert.AreEqual(0, recipeToCheck.Ingredients.Count);

    }

    [Test]
    public void AddStepsTest()
    {
        var moq = new Mock<IInputProvider>();
        moq.SetupSequence(ip => ip.ReadInput(It.IsAny<string>()))
            .Returns("Pizza")
            .Returns("2");
        
        RecipeManager recipeManager = new RecipeManager(moq.Object);
        recipeManager.AddRecipe();
        
        var moq2 = new Mock<IInputProvider>();
        
        moq2.SetupSequence(ip => ip.ReadInput(It.IsAny<string>()))
            .Returns("Hello friend, step one is to cook")
            .Returns("bery nice, now taste")
            .Returns("now serve!")
            .Returns("Done");
        
        recipeManager._inputProvider = new IInputProviderTest("Pizza");
        var recipeToCheck = recipeManager.FindRecipe(out string recipeName);
        recipeToCheck.InputProvider = moq2.Object;
        recipeToCheck.AddSteps("Pizza");
        Assert.AreEqual("Pizza", recipeToCheck.Name);
        Assert.Contains("Hello friend, step one is to cook",recipeToCheck.Steps);
        Assert.Contains("now serve!",recipeToCheck.Steps);
        //Assert.Contains("Done",recipeToCheck.Steps);
        
    }
    
    [Test]
    public void DeleteStepsTest()
    {
        var moq = new Mock<IInputProvider>();
        moq.SetupSequence(ip => ip.ReadInput(It.IsAny<string>()))
            .Returns("Pizza")
            .Returns("2");
        
        RecipeManager recipeManager = new RecipeManager(moq.Object);
        recipeManager.AddRecipe();
        
        var moq2 = new Mock<IInputProvider>();
        
        moq2.SetupSequence(ip => ip.ReadInput(It.IsAny<string>()))
            .Returns("Hello friend, step one is to cook")
            .Returns("bery nice, now taste")
            .Returns("now serve!")
            .Returns("Done");
        
        recipeManager._inputProvider = new IInputProviderTest("Pizza");
        var recipeToCheck = recipeManager.FindRecipe(out string recipeName);
        recipeToCheck.InputProvider = moq2.Object;
        recipeToCheck.AddSteps("Pizza");

        recipeToCheck.InputProvider = new IInputProviderTest("yes");
        recipeToCheck.StepsDelete("Pizza");
       
        
        
        Assert.AreEqual("Pizza", recipeToCheck.Name);
        Assert.IsEmpty(recipeToCheck.Steps);
        
    }




}