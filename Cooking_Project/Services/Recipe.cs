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

        public List<Ingredient> Ingredients { get; set; }

        public List<string> Steps { get; set; }

        internal IInputProvider InputProvider;
        
        public int Servings { get; set; }

        //Initalsing the properties

        public Recipe(string name, IInputProvider inputProvider)
        {

            Name = name;
            Ingredients = new List<Ingredient>();
            Steps = new List<string>();
            InputProvider = inputProvider;

        }

        //Loops through and add ingredients to a recipe which is parsed through by a string
        public void AddIngredients(string recipeName)
        {
            //string ingredientAdd;
            Ingredient ingredientObject;

            Console.WriteLine("Please Enter The ingredients one by one, when you are finished, enter 'done");


            do
            {

                //ingredientAdd = Console.ReadLine();
                //ingredientAdd = InputProvider.ReadInput("-----");
                ingredientObject = new Ingredient(new ConsoleInputProvider());
                ingredientObject.AddIngredient();

                if (ingredientObject.Name == "")
                {
                    Console.WriteLine("Nothing has been entered ingredients to add.");
                    return;
                }
                else
                {
                    Console.WriteLine($"You have added the {ingredientObject.Name} to your recipe.");
                    Ingredients.Add(ingredientObject);
                }
                // else if (ingredientAdd.ToUpper() == "DONE")
                // {
                //     Console.WriteLine("Thank you for adding ingredients to your recipe.");
                // }
                // else if (ingredientAdd is string)
                // {
                //     this.Ingredientz.Add(ingredientAdd);
                //     Console.WriteLine($"You have added {ingredientAdd}");
                // }
                // else
                // {
                //     Console.WriteLine("Input type error");
                // }


            } while (ingredientObject.Name.ToUpper() != "DONE");

            

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

                foreach (Ingredient ingredient in Ingredients)
                {
                    Console.WriteLine($"{ingredient.Name}");
                    Console.WriteLine($"{ingredient.Amount}");
                    Console.WriteLine($"{ingredient.Unit}");
                    Console.WriteLine($"------------------");
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
            if (IngredientsCount() == 0)
                return;

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
                else if (Ingredients.Any(I => I.Name == ingredientRemove))
                {
                    Ingredients.RemoveAll(I => I.Name == ingredientRemove);
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
            string stepsAdd = "";
 

            Console.WriteLine($"Please Enter The Steps for the recipe: {recipeName}");

            do
            {
                stepsAdd = InputProvider.ReadInput("---");
                Steps.Add(stepsAdd);
            } while (stepsAdd.ToUpper() != "DONE");
            //Steps = stepsAdd.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            
            ViewSteps(recipeName);
            
        }

        public void ViewSteps(string recipeName)
        {
            if (Steps.Count() == 0)
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
        
        public void StepsDelete(string recipeName)
        {
            ViewSteps(recipeName);
            if (Steps.Count() == 0)
                return;
            
            string check = InputProvider.ReadInput($"Are you sure you want to delete the steps for {recipeName}? yes or no?");
            
            if (check.ToUpper() == "YES")
            {
                Steps.Clear();
                Console.WriteLine($"All steps deleted from {recipeName}.\n");
            }
            else
            {
                Console.WriteLine("Nothing Deleted.\n");
            }
            

        }

        public int StepsCount()
        {
            return Steps.Count;
        }



    }
}
