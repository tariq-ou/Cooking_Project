using System;
using System.Collections.Generic;
using Cooking_Project.Application.Adaptors;
using Cooking_Project.Application.Services;
using Cooking_Project.Application.Ports;

namespace Cooking_Project.Application.Domain
{
    public class RecipeManager
    {
        //Inilalises a list of recipes
        public List<Recipe> recipes = new List<Recipe>();
        //Initalises a IInputProvider so during runtime different inputs can be processed using dependency injection
        private readonly IInputProvider _inputProvider;


        
        //simple message telling us that RecipeManager object has been created
        public RecipeManager(IInputProvider inputProvider)
        {
            Console.WriteLine("Recipe Manager Created");
            _inputProvider = inputProvider;
        }

        //Promts user for a recipe name and creates a new recipe object with that name in the recipes list
        public void AddRecipe()
        {

            // Console.WriteLine("Reciepe Name?");
            // string recipeName = Console.ReadLine();
            
            string recipeName = _inputProvider.ReadInput("Recipe Name?");

            if (recipeName is string)
            {
                recipes.Add(new Recipe(recipeName, new ConsoleInputProvider()));

                Console.WriteLine($"Successfuly added {recipeName}");
                Console.WriteLine($"\n");
            }

            else
            {
                Console.WriteLine("Input type error");
            }

        }

        //Loops through the list recipes printing our the name of each recipe object stored
        public void ViewRecipe()
        {
            if ( this.RecipeCount() > 0)
            {
                foreach (Recipe item in recipes)
                {
                    Console.WriteLine($"{item.Name}");
                }

                Console.WriteLine($"\n");
            }
            else
            {
                Console.WriteLine("There are no inputed recipies, please add a recipe.");
            }
            
        }

        //Removes recipe from list by calling Find Recipe and then removing that object
        public void DeleteRecipe()
        {

            string recipeName;

            Recipe toDelete = CheckRecipe(out recipeName);

            if (toDelete is not null)
            {
                recipes.Remove(toDelete);

                Console.WriteLine($"Successfuly removed {recipeName}");
                Console.WriteLine($"\n");
            }
            else
            {
                Console.WriteLine("Cannot delete a recipe that has not been added.\n");
            }
            
        }

        //Promts user for a recipe name and returns a recipe object with the same name if exisits in recipes list
        public Recipe FindRecipe(out string recipeName)
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
         
            Recipe recipeFound = recipes.Find(r => r.Name == checkName);

            if (recipeFound is null)
             {
                Console.WriteLine("Not Found\n");
                return null;
             }

             return recipeFound;

            
        }

        //Checks to see if the recipe exisits and prints out a statement depending on if it was found or not
        public Recipe CheckRecipe(out string recipeName)
        {

            
            Recipe checkRecipe = this.FindRecipe(out recipeName);
            

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
            return recipes.Count();
        }

    }
}
