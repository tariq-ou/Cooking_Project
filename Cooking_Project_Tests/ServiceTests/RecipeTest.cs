using NUnit.Framework;
using Cooking_Project.Application.Domain;
using System;
using System.Collections.Generic;
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
        RecipeManager recipeManager = new RecipeManager(new IInputProviderTest("Thai Green Curry"));
        recipeManager.AddRecipe();
        
        var moq = new Mock<IInputProvider>();
        moq.SetupSequence(ip => ip.ReadInput(It.IsAny<string>()))
            .Returns("Thai Green Paste")
            .Returns("Protein")
            .Returns("Vegetables")
            .Returns("Done");
        
        
        
     
        var recipeToCheck = recipeManager.FindRecipe(out string recipeName);
        recipeToCheck.InputProvider = moq.Object;
        recipeToCheck.AddIngredients("Thai Green Curry");
       
        
        Assert.AreEqual("Thai Green Curry", recipeToCheck.Name);
        Assert.AreEqual(3, recipeToCheck.Ingredients.Count);
        Assert.Contains("Thai Green Paste", recipeToCheck.Ingredients);
        Assert.Contains("Protein", recipeToCheck.Ingredients);
        Assert.Contains("Vegetables", recipeToCheck.Ingredients);
        

    }

    [Test]
    public void DeleteIngredientsTest()
    {
        RecipeManager recipeManager = new RecipeManager(new IInputProviderTest("Pizza"));
        recipeManager.AddRecipe();
        
        var moq = new Mock<IInputProvider>();
        
        moq.SetupSequence(ip => ip.ReadInput(It.IsAny<string>()))
            .Returns("Tomato Sause")
            .Returns("Cheese")
            .Returns("Olives")
            .Returns("Done");
        var recipeToCheck = recipeManager.FindRecipe(out string recipeName);
        recipeToCheck.InputProvider = moq.Object;
        recipeToCheck.AddIngredients("Pizza");
        
        moq.SetupSequence(ip => ip.ReadInput(It.IsAny<string>()))
            .Returns("Tomato Sause")
            .Returns("Olives")
            .Returns("Done");
        recipeToCheck.InputProvider = moq.Object;
        recipeToCheck.IngredientDelete("Pizza");
        
        Assert.AreEqual("Pizza", recipeToCheck.Name);
        Assert.AreEqual(1, recipeToCheck.Ingredients.Count);
        Assert.Contains("Cheese", recipeToCheck.Ingredients);


    }

    [Test]
    public void DeleteAllIngredientsTest()
    {
        RecipeManager recipeManager = new RecipeManager(new IInputProviderTest("Pizza"));
        recipeManager.AddRecipe();
        
        var moq = new Mock<IInputProvider>();
        
        moq.SetupSequence(ip => ip.ReadInput(It.IsAny<string>()))
            .Returns("Tomato Sause")
            .Returns("Cheese")
            .Returns("Olives")
            .Returns("Done");
        var recipeToCheck = recipeManager.FindRecipe(out string recipeName);
        recipeToCheck.InputProvider = moq.Object;
        recipeToCheck.AddIngredients("Pizza");
        
        recipeToCheck.InputProvider = new IInputProviderTest("all");
        recipeToCheck.IngredientDelete("Pizza");
        
        Assert.AreEqual("Pizza", recipeToCheck.Name);
        Assert.AreEqual(0, recipeToCheck.Ingredients.Count);

    }




}