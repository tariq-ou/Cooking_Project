using System;
using System.Collections.Generic;
using Cooking_Project.Application.Adaptors;
using Cooking_Project.Application.Services;
using Cooking_Project.Application.Domain;
using Microsoft.EntityFrameworkCore;
using Cooking_Project.Application.Infastrucuture;

namespace Cooking_Project
{
    class Program
    {
        public static void Main(string[] args)
        {

            //look into lamda expressions, would be good to actively use them and understanad them more as i keep forgetting
            //learn more about dependency injection
            // check current code and see if you can switch anything out with a lamda expression


            
            
            // (cba for now as low prioroity)when adding reccipe if you put in a string for serbing size it still adds it but low prioroity i guess 
            // (dedleting the ingredient as a whole is enough this is not needed)delete function of those amounds
            // (dedleting the ingredient as a whole is enough this is not needed)unit test for those amounts
            
            //NExt
            // fix github repositories by merging databasefork into master somehow
            // then branch off and add irecipemanager ect and clean up exisiting code
            //then merge back 
            //then look into working on ASP.net

                
            //extra notes to consider
            // consider having a windsor castle DI for IOU so that you are able to decopuple your code better for things like console.writeline and stuff
            // mabye UI time
            // mabye time to update with your recipes?
            //consider adding a logger rather than the console.wrtiteline stuff for logging
            // okay so only the the adaptor should have acceess to the databse? / ttry to connect with it to decouple it from the recipe manager. we can use event handling to save it after the method runs in recipe decoupling the saving action from the method
            //need to look into this more. so far adapror is seperate to recipe manager , event handling to save but need to design in a way that changing the inputs and outputs is fluid and deosnt require code changes to exisiting methods
            // add the database functionality now early on so code make sesnse with it ?
            // mabye create a back up feature, you dont wanna lose your riceipes and steps mabye a way to save them to file formatt somehwere to be safe as a back up
            // (hold off for now - try and get working model first)Use IQueryable<T> rather than ienumerbaale/Lists as appently it filters the data on the database side rather than the application side and so its more effeicengt & faster
            
            // mabye add a proper logger class ect function in this code so that it creates a file and rather than just to console also writes out logs 
            
            //Your gunna have to refactor the code code out for the console.write line to be a dependecy injection as the UI will have to take this over at some point 
            //mabye angular for front end?
            
            string choice;
            string choice2;
            string recipeName;
            RecipeManager recipeManager = new RecipeManager(new ConsoleInputProvider());
            Recipe checkedRecipe;
            
            // creating the db
            RecipeDbContext.CreateDatabase();
            
            //create recipeservice and passthrough dependecy injection for type of output save
            var recipeService = new RecipeService(new ERecipeRepository());
            
            // //syncing db recipes with recipemanager list
            // recipeManager.recipes = recipeService.ReadAllRecipe();
            // Console.WriteLine("Recipes syced");
            // //Give each recipe an InputProvider as those are not mapped
            // foreach (var recipe in recipeManager.recipes)
            //     recipe.InputProvider = new ConsoleInputProvider();
            
            recipeService.SyncDBtoMemory(recipeManager, () => new ConsoleInputProvider());
            
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
                Console.WriteLine("7. Delete steps from Recipe");
                Console.WriteLine("8. Export from DB to JSON backup");
                Console.WriteLine("9. Import from JSON backup to DB");
                



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
                        Console.WriteLine("Would to view any of thier ingredients or steps?");
                        choice2 = Console.ReadLine();

                        switch (choice2.ToUpper())
                        {

                            case "INGREDIENTS":

                                recipeManager.CheckRecipe(out recipeName).ViewIngredients(recipeName);
                                break;
                            
                            case "STEPS":

                                recipeManager.CheckRecipe(out recipeName).ViewSteps(recipeName);
                                break;

                            default:

                                Console.WriteLine("You will be returned to the menu.\n");
                                break;
                        }

                        break;
                    case "2":
                        var recipe = recipeManager.AddRecipe();
                        recipeService.AddRecipeSave(recipe);
                        break;
          
                    case "3":
                        Recipe toDelete = recipeManager.CheckRecipe(out recipeName);
                        recipeService.DeleteRecipeIngredients(toDelete);
                        recipeManager.DeleteRecipe(toDelete);
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
                        recipeService.AddIngredientSave(checkedRecipe.Name, checkedRecipe.Ingredients);
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
                        recipeService.AddRecipeSave(checkedRecipe);
                        break;
                    
                    case "7":
                        
                        checkedRecipe = recipeManager.CheckRecipe(out recipeName);

                        if (checkedRecipe is null)
                        {
                            break;
                        }

                        checkedRecipe.StepsDelete(recipeName);
                        break;
                    
                    case "8":
                        
                        recipeService.ReadExportDB();
                        break;
                    
                    case "9":
                        
                        recipeService.ImportToDB();
                        recipeService.SyncDBtoMemory(recipeManager, () => new ConsoleInputProvider());
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                    
                        
                        
                        
                }

            } while ( choice == "1" || choice == "2" || choice == "3" || choice == "4" || choice == "5" || choice == "6" || choice == "7" || choice == "8" || choice == "9");

        }

    }
}
