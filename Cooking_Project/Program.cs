﻿using System;
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

            //look into lamda expressions, would be good to actively use them and understanad them more as i keep forgetting
            //learn more about dependency injection
            
            
            //Delete ingredients unit test
            // add addition funtionality for steps and view 
            // add delelte functionalut or steps 
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
                Console.WriteLine("5. Delete Ingredients from Recipe");



                choice = Console.ReadLine();

                if (!(choice is string)) {

                    Console.WriteLine("Input type error.");
                    continue;
                }


                

                switch (choice)
                {
                    case "1":
                        recipeManager.ViewRecipe();

                        if (recipeManager.RecipeCount() == 0)
                            break;
                        Console.WriteLine("Would to view any of thier ingredients?");
                        choice2 = Console.ReadLine();

                        switch (choice2.ToUpper())
                        {

                            case "YES":

                                recipeManager.CheckRecipe(out recipeName).ViewIngredients(recipeName);
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
                    
                    case "5":
                        
                        Recipe checkedRecipe2 = recipeManager.CheckRecipe(out recipeName);

                        if (checkedRecipe2 is null)
                        {
                            break;
                        }

                        checkedRecipe2.IngredientDelete(recipeName);
                        break;
                        

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                    
                        
                        
                        
                }

            } while ( choice == "1" || choice == "2" || choice == "3" || choice == "4" || choice == "5" );

        }

    }
}
