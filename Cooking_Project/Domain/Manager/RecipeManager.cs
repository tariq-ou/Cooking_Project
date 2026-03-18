using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Cooking_Project.Application.Adaptors;
using Cooking_Project.Application.Services;
using Cooking_Project.Application.Ports;
[assembly: InternalsVisibleTo("CookingAPI_Tests")]

namespace Cooking_Project.Application.Domain
{
    public class RecipeManager : IRecipeManager
    {
        //Inilalises a list of recipes
        //public List<Recipe> recipes = new List<Recipe>();

        public List<Recipe> Recipes { get; set; }


        //Initalises a IInputProvider so during runtime different inputs can be processed using dependency injection
        internal IInputProvider _inputProvider;
        
        internal IOutputProvider _outputProvider;
        //Recipes = new List<Recipe>();

        
        //simple message telling us that RecipeManager object has been created
        public RecipeManager(IInputProvider inputProvider, IOutputProvider outputProvider)
        {
            Console.WriteLine("Recipe Manager Created");
            _inputProvider = inputProvider;
            _outputProvider = outputProvider;
            Recipes = new List<Recipe>();
        }

        //Promts user for a recipe name and creates a new recipe object with that name in the recipes list
        public IRecipe? AddRecipe()
        {

            // Console.WriteLine("Reciepe Name?");
            // string recipeName = Console.ReadLine();
            
            string recipeName = _inputProvider.ReadInput("Recipe Name?");
            
            int servingSize;

            
                //recipes.Add(new Recipe(recipeName, new ConsoleInputProvider()));

                if (int.TryParse(_inputProvider.ReadInput("For what serving size?"), out servingSize))
                {
                    Recipes.Add(new Recipe(recipeName, new ConsoleInputProvider()));
                    GetRecipe(recipeName).Servings = servingSize;
                }
                else
                {
                    Console.WriteLine("Please enter a valid number");
                    //recipes.RemoveAll(r => r.Name == recipeName);
                    return null;
                }
                //GetRecipe(recipeName).Servings = int.Parse(_inputProvider.ReadInput("For what serving size?"));
                Console.WriteLine($"Successfuly added {recipeName} for serving a size of {GetRecipe(recipeName).Servings}");
                Console.WriteLine($"\n");
                
                return GetRecipe(recipeName);
            
            

        }

        //Loops through the list recipes printing our the name of each recipe object stored
        public void ViewRecipe()
        {
            if ( this.RecipeCount() > 0)
            {
                foreach (IRecipe item in Recipes)
                {
                    _outputProvider.Output($"{item.Name} | Serving : {item.Servings}");
                }

                Console.WriteLine($"\n");
            }
            else
            {
                _outputProvider.Output("There are no inputed recipies, please add a recipe.");
            }
            

        }

        //Removes recipe from list by calling Find Recipe and then removing that object
        public void DeleteRecipe(IRecipe toDelete)
        {

            string recipeName = toDelete.Name;

            //Recipe toDelete = CheckRecipe(out recipeName);

            if (toDelete is not null)
            {
                Recipes.Remove((Recipe)toDelete);

                Console.WriteLine($"Successfuly removed {recipeName}");
                Console.WriteLine($"\n");
            }
            else
            {
                Console.WriteLine("Cannot delete a recipe that has not been added.\n");
            }
            
        }

        //Promts user for a recipe name and returns a recipe object with the same name if exisits in recipes list
        public IRecipe? FindRecipe(out string recipeName)
        {
            //Console.WriteLine("What recipe are you looking for?");
            
            //string recipeName = _inputProvider.ReadInput("What recipe are you looking for?");
            
            this.ViewRecipe();
            //string checkName = Console.ReadLine();
            string checkName = _inputProvider.ReadInput("What recipe are you looking for?");

            recipeName = checkName;

            if (string.IsNullOrWhiteSpace(checkName))
            {
                Console.WriteLine("Not Found\n");
                return null;
            }
         
            IRecipe recipeFound = Recipes.Find(r => r.Name == checkName);

            if (recipeFound is null)
             {
                Console.WriteLine("Not Found\n");
                return null;
             }

             return recipeFound;

            
        }

        //Checks to see if the recipe exisits and prints out a statement depending on if it was found or not
        public IRecipe CheckRecipe(out string recipeName)
        {

            
            IRecipe checkRecipe = this.FindRecipe(out recipeName);
            

            if (checkRecipe is null)
            {
                Console.WriteLine($"The {recipeName} doesnt exisit");
            }
            else
            {
                Console.WriteLine($"{recipeName} Found!");
            }


            return checkRecipe;


        }
        
        //Count nuumber of recipes
        public int RecipeCount()
        {
            return Recipes.Count();
        }

        public IRecipe GetRecipe(string recipeName)
        {
            //Console.WriteLine("What recipe are you looking for?");
            
            //string recipeName = _inputProvider.ReadInput("What recipe are you looking for?");
            
            //this.ViewRecipe();
            //string checkName = Console.ReadLine();
            //string checkName = _inputProvider.ReadInput("What recipe are you looking for?");

            

            if (string.IsNullOrWhiteSpace(recipeName))
            {
                Console.WriteLine("Not Found\n");
                return null;
            }
         
            IRecipe recipeFound = Recipes.Find(r => r.Name == recipeName);

            if (recipeFound is null)
            {
                Console.WriteLine("Not Found\n");
                return null;
            }   

            return recipeFound;

            
        }
        
        // public IEnumerable<Recipe> GetAllRecipesAPI()
        // {
        //    
        //     return Recipes;
        // }

        public void UpdateRecipeViaIndex(Recipe recipe)
        {
            var index = this.Recipes.FindIndex(r => r.Id == recipe.Id);
        
            if (index != -1)
            {
                this.Recipes[index] = recipe;
            }
        }
    }
}
