using System;
using System.Collections.Generic;
using Cooking_Project.Application.Adaptors;
using Cooking_Project.Application.Services;
using Cooking_Project.Application.Domain;

namespace Cooking_Project
{
    class Program
    {
        public static void Main(string[] args)
        {


            // turn this check recipe stuff into a method and change it out in your code, line 73
            // start writing unit tests so you dont need to waste time enter rubbish every time. remember you are going to have to change all the methods to deal with dependency injection now for iinputprovider
            // add the database functionality now early on so code make sesnse with it ?

            string choice;
            string choice2;
            string recipeName;
            RecipeManager recipeManager = new RecipeManager(new ConsoleInputProvider());

            do
            {


                Console.WriteLine("Welcome to the Cooking Academy");
                Console.WriteLine("Menu");
                Console.WriteLine("1. View Recipes");
                Console.WriteLine("2. Add Recipe");
                Console.WriteLine("3. Delete Recipe");
                Console.WriteLine("4. Add Ingredients to a Recipe");



                choice = Console.ReadLine();

                if (!(choice is string)) {

                    Console.WriteLine("Input type error.");
                    continue;
                }


                

                switch (choice)
                {
                    case "1":
                        recipeManager.ViewRecipe();

                        Console.WriteLine("Would to view any of thier ingredients?");
                        choice2 = Console.ReadLine();

                        switch (choice2.ToUpper())
                        {

                            case "YES":

                                recipeManager.FindRecipe(out recipeName).ViewIngredients(recipeName);
                                break;

                            default:

                                Console.WriteLine("You will be returned to the menu.\n");
                                break;
                        }

                        break;
                    case "2":
                        recipeManager.AddRecipe();
                        break;
          
                    case "3":
                        recipeManager.DeleteRecipe();
                        break;

                    case "4":

                        // turn this check recipe stuff into a method and change it out in your code
                        //Recipe checkRecipe = recipeManager.FindRecipe(out recipeName);

                        //if (checkRecipe is null){
                        //    Console.WriteLine("Recipe doesnt exisit");
                        //    break;
                        //}

                        Recipe checkedRecipe = recipeManager.CheckRecipe(out recipeName);

                        if (checkedRecipe is null)
                        {
                            break;
                        }

                        checkedRecipe.AddIngredients(recipeName);
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                        
                }

            } while ( choice == "1" || choice == "2" || choice == "3" || choice == "4" );

        }

    }
}
