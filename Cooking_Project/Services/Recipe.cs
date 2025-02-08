using System;
using System.Collections.Generic;
using Cooking_Project.Application.Ports;

namespace Cooking_Project.Application.Services
{
    public class Recipe
    {
      
        // Delcaring Recipe properties

        public string Name { get; set; }

        public List<string> Ingredients { get; set; }

        public List<string> Steps { get; set; }

        public IInputProvider InputProvider { get; set; }

        //Initalsing the properties

        public Recipe(string name)
        {

            Name = name;
            Ingredients = new List<string>();
            Steps = new List<string>();
           // _InputProvider = inputProvider;

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
                    continue;
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


            } while (ingredientAdd.ToUpper() != "Done");

            

            this.ViewIngredients(recipeName);


        }

        //loops through printing all the ingredients for a recipe, recipe name is parsed through as a string
        public void ViewIngredients(string recipeName)
        {
            Console.WriteLine($"Below are the Ingredients for {recipeName}: \n");

            foreach (string ingredient in Ingredients)
            {
                Console.WriteLine($"{ingredient}");
            }
            Console.WriteLine($"\n");
        }


   
        
    }
}
