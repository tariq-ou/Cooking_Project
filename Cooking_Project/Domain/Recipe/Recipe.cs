using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Cooking_Project.Application.Adaptors;
using Cooking_Project.Application.Ports;

namespace Cooking_Project.Application.Services
{
    public class Recipe : IRecipe
    {
      
        // Delcaring Recipe properties
        
        public int Id { get; set; } // Primary key (required apprently)

        public string Name { get; set; }
        
        public List<Ingredient> Ingredients { get; set; }

        public string StepsSerialized
        {
            get => string.Join("||", Steps ?? new List<string>());
            set => Steps = string.IsNullOrEmpty(value)
                ? new List<string>()
                : value.Split("||", StringSplitOptions.None).ToList();
        }
        
        [NotMapped]
        public List<string> Steps { get; set; }

        [NotMapped]
        public IInputProvider InputProvider{ get; set; }
        
        public int Servings { get; set; }
        
        public string? ImagePath { get; set; }

        //Initalsing the properties
        
        //another constructor of entity framework as it will use the parameterless one and it wont current work  due to not being able to mapp custom object inputprovider
        public Recipe()
        {
            
            Ingredients = new List<Ingredient>();
            Steps = new List<string>();

        }
        public Recipe(string name, IInputProvider inputProvider, string? imagePath = null)
        {

            Name = name;
            // probably need to add servings here and to unit tests for when initilaising but its fine in API as it maps it so will ignore for now i think
            //Servings = servings;
            Ingredients = new List<Ingredient>();
            Steps = new List<string>();
            //[NotMapped]
            InputProvider = inputProvider;
            ImagePath = imagePath;

        }

        //Loops through and add ingredients to a recipe which is parsed through by a string
        public void AddIngredients(string recipeName)
        {
            // if (Ingredients.FirstOrDefault() == null)
            // {
            //     Ingredient? ingredientObject;
            // }
            // else
            // {
            //     
            // }
            
            Ingredient? ingredientObject;
            
            Console.WriteLine("Please Enter The ingredients one by one, when you are finished, enter 'done");


            do
            {

                //ingredientAdd = Console.ReadLine();   
                //ingredientAdd = InputProvider.ReadInput("-----");
                
                ingredientObject = new Ingredient(InputProvider);
                ingredientObject = ingredientObject.AddIngredient();
                
                if (ingredientObject == null)
                {
                    Console.WriteLine("Thank you");
                }
                else
                {
                    //ingredientObject.AddIngredient();


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


            } while (ingredientObject != null);

            

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
 

            Console.WriteLine($"Please Enter The Steps for the recipe: {recipeName} | when you are finished, enter 'done'");

            do
            {
                stepsAdd = InputProvider.ReadInput("---");
                if(!(stepsAdd.ToUpper() == "DONE"))
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
