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
            // check current code and see if you can switch anything out with a lamda expression
                
            
            // fix funtionaluty to add steps, you want a loop for readline as it reads it once at a time and have it stop reading once it reads up to done turining it all into a list
            // add view steps to the view recipe option on program
            // add delelte functionalut or steps
            // add ingredient amount , how much you need fuunction? add as part of ingredients or a new property 
            // delete function of those amounds
            // unit test for those amounts
            
            // okay so only the the adaptor should have acceess to the databse? / ttry to connect with it to decouple it from the recipe manager. we can use event handling to save it after the method runs in recipe decoupling the saving action from the method
            //need to look into this more. so far adapror is seperate to recipe manager , event handling to save but need to design in a way that changing the inputs and outputs is fluid and deosnt require code changes to exisiting methods
            // add the database functionality now early on so code make sesnse with it ?

            string choice;
            string choice2;
            string recipeName;
            RecipeManager recipeManager = new RecipeManager(new ConsoleInputProvider());
            Recipe checkedRecipe;

            do
            {


                Console.WriteLine("Welcome to the Cooking Academy");
                Console.WriteLine("Menu");
                Console.WriteLine("1. View Recipes");
                Console.WriteLine("2. Add Recipe");
                Console.WriteLine("3. Delete Recipe");
                Console.WriteLine("4. Add Ingredients to a Recipe");
                Console.WriteLine("5. Delete Ingredients from Recipe");
                Console.WriteLine("6. Add steps to a Recipe");
                



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

                        checkedRecipe = recipeManager.CheckRecipe(out recipeName);

                        if (checkedRecipe is null)
                        {
                            break;
                        }

                        checkedRecipe.AddIngredients(recipeName);
                        break;
                    
                    case "5":
                        
                        checkedRecipe = recipeManager.CheckRecipe(out recipeName);

                        if (checkedRecipe is null)
                        {
                            break;
                        }

                        checkedRecipe.IngredientDelete(recipeName);
                        break;
                    
                    case "6":

                        // turn this check recipe stuff into a method and change it out in your code
                        //Recipe checkRecipe = recipeManager.FindRecipe(out recipeName);

                        //if (checkRecipe is null){
                        //    Console.WriteLine("Recipe doesnt exisit");
                        //    break;
                        //}

                        checkedRecipe = recipeManager.CheckRecipe(out recipeName);

                        if (checkedRecipe is null)
                        {
                            break;
                        }

                        checkedRecipe.AddSteps(recipeName);
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                    
                        
                        
                        
                }

            } while ( choice == "1" || choice == "2" || choice == "3" || choice == "4" || choice == "5" || choice == "6");

        }

    }
}
