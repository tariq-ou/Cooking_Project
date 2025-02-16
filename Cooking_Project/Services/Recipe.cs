using System;
using System.Collections.Generic;
using Cooking_Project.Application.Adaptors;
using Cooking_Project.Application.Ports;

namespace Cooking_Project.Application.Services
{
    public class Recipe
    {
      
        // Delcaring Recipe properties

        public string Name { get; set; }

        public List<string> Ingredients { get; set; }

        public List<string> Steps { get; set; }

        public IInputProvider InputProvider;

        //Initalsing the properties

        public Recipe(string name, IInputProvider inputProvider)
        {

            Name = name;
            Ingredients = new List<string>();
            Steps = new List<string>();
            InputProvider = inputProvider;

        }

        //Loops through and add ingredients to a recipe which is parsed through by a string
        public void AddIngredients(string recipeName)
        {
            string ingredientAdd;
 

            Console.WriteLine("Please Enter The ingredients one by one, when you are finished, enter 'done");


            do
            {

                //ingredientAdd = Console.ReadLine();
                ingredientAdd = InputProvider.ReadInput("What Recipe Name would you like to add ingredients to?");

                if (ingredientAdd is null)
                {
                    Console.WriteLine("Nothing has been entered ingredients to add.");
                    return;
                }
                else if (ingredientAdd.ToUpper() == "DONE")
                {
                    Console.WriteLine("Thank you for adding ingredients to your recipe.");
                }
                else if (ingredientAdd is string)
                {
                    this.Ingredients.Add(ingredientAdd);
                    Console.WriteLine($"You have added {ingredientAdd}");
                }
                else
                {
                    Console.WriteLine("Input type error");
                }


            } while (ingredientAdd.ToUpper() != "DONE");

            

            this.ViewIngredients(recipeName);


        }

        //loops through printing all the ingredients for a recipe, recipe name is parsed through as a string
        public void ViewIngredients(string recipeName)
        {
            if (this.IngredientsCount() == 0)
            {
                Console.WriteLine("There are no ingredients in the recipe.");
            }
            else
            {
                Console.WriteLine($"Below are the Ingredients for {recipeName}: \n");

                foreach (string ingredient in Ingredients)
                {
                    Console.WriteLine($"{ingredient}");
                }

                Console.WriteLine($"\n");
            }
        }

        public int IngredientsCount()
        {
            return Ingredients.Count;
        }

        public void IngredientDelete(string recipeName)
        {
            string ingredientRemove = "";
            ViewIngredients(recipeName);

            while (!(ingredientRemove.ToUpper() == "DONE"))
            {
                ingredientRemove = InputProvider.ReadInput("What Ingredient/s would you like to remove? enter all if you want to remove everything and done when you are finsihed deleteing");

                if (ingredientRemove is null || !(ingredientRemove is string))
                {
                    Console.WriteLine("Nothing has been entered ingredients to delete.");
                }
                else if (ingredientRemove.ToUpper() == "DONE")
                {
                    Console.WriteLine("Deletion complete.");
                }
                else if (ingredientRemove.ToUpper() == "ALL")
                {
                    
                    Ingredients.Clear();
                    
                    Console.WriteLine("All deleted.");
                    ingredientRemove = "DONE";
                    
                }
                else if (Ingredients.Contains(ingredientRemove))
                {
                    Ingredients.Remove(ingredientRemove);
                    Console.WriteLine($"Ingredient {ingredientRemove} Deleted.");
                }
                else
                {
                    Console.WriteLine("Ingredient doesn't exist.");
                }

            }
            
            ViewIngredients(recipeName);
        }

        public void AddSteps(string recipeName)
        {
            string stepsAdd;
 

            Console.WriteLine($"Please Enter The Steps for the recipe: {recipeName}");

            stepsAdd = InputProvider.ReadInput($"Please Enter The Steps for the recipe: {recipeName}");
            Steps = stepsAdd.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            
            ViewSteps(recipeName);
            
        }

        public void ViewSteps(string recipeName)
        {
            if (this.Steps.Count() == 0)
            {
                Console.WriteLine("There are no Steps in the recipe.");
            }
            else
            {
                Console.WriteLine($"Below are the Steps for {recipeName}: \n");

                foreach (string step in Steps)
                {
                    Console.WriteLine($"{step}");
                }

                Console.WriteLine($"\n");
            }
        }




    }
}
