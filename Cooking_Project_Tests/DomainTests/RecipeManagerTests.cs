using NUnit.Framework;
using Cooking_Project.Application.Domain;
using System;
using System.Collections.Generic;
using Cooking_Project_Tests.PortsTest;
using Cooking_Project.Application.Ports;
using Moq;
using Cooking_Project.Application.Services;

namespace Cooking_Project_Tests
{
    public class RecipeManager_test
    {

        //private RecipeManager recipeManager = null!;

        [SetUp]
        public void Setup()
        {
            // var mockIInputProvider = new Mock<IInputProvider>();
            // recipeManager = new RecipeManager(mockIInputProvider.Object);
            //private RecipeManager recipeManger = new RecipeManager(new IInputProviderTest());
        }

        [Test]
        public void AddRecipeTest()
        {
           
            //Creating instance and then adding a recipe 
            RecipeManager recipeManager = new RecipeManager(new IInputProviderTest("Pizza"));
            recipeManager.AddRecipe();
            
            
            Assert.NotNull(recipeManager.recipes.Find(r => r.Name == "Pizza"));
            //Assert.Pass();
        }
        
        [Test]
        public void DeleteRecipeTest()
        {
            RecipeManager recipeManager = new RecipeManager(new IInputProviderTest("Pizza"));
            recipeManager.AddRecipe();
            recipeManager.DeleteRecipe();
            Assert.IsNull(recipeManager.recipes.Find(r => r.Name == "Pizza"));
        }

        [Test]
        public void FindRecipeTest_NotFound()
        {
            RecipeManager recipeManager = new RecipeManager(new IInputProviderTest("Pizza"));
            Recipe checkedRecipe = recipeManager.FindRecipe(out string recipeName);
            Assert.IsNull(checkedRecipe);
        }
        
        [Test]
        public void FindRecipeTest_Found()
        {
            RecipeManager recipeManager = new RecipeManager(new IInputProviderTest("Pizza"));
            recipeManager.AddRecipe();
            Recipe checkedRecipe = recipeManager.FindRecipe(out string recipeName);
            Assert.AreSame(checkedRecipe,recipeManager.recipes[0]);
        }
    }
}
