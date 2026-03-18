using Cooking_Project.Application.Infastrucuture;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Cooking_Project.Application.Ports;
using Cooking_Project.Application.Services;
using System.Text.Json;
using Cooking_Project.Application.Domain;
using Cooking_Project.Factory;
using Microsoft.Extensions.DependencyInjection;  

namespace Cooking_Project.Application.Adaptors;

public class ERecipeRepository: IRecipeRepositoryDB
{
    private IRecipeManager _recipeManager;
    public ERecipeRepository(IRecipeManager recipeManager)
    {
        _recipeManager = recipeManager;
    }
    
    
    public void SyncDBMemory()
    {
        //syncing db recipes with recipemanager list
        _recipeManager.Recipes = this.ReadAll();
        Console.WriteLine("Recipes syced");
        //Give each recipe an InputProvider as those are not mapped
        foreach (var recipe in _recipeManager.Recipes)
            recipe.InputProvider = InputProviderFactory.Create();
    }
    public void Save(Recipe recipe)
    {
        using (var context = new RecipeDbContext())
        {
            if (context.Recipes.Any(r => r.Id == recipe.Id))
            {
                context.Recipes.Update(recipe);
            }
            else
            {
                context.Recipes.Add(recipe);
            }

            context.SaveChanges();
        }
    }

    public List<Recipe> ReadAll()
    {
        using (var context = new RecipeDbContext())
        {
            return context.Recipes.Include(r => r.Ingredients).ToList();
        }
    }

    public void SaveNestedItem(string recipeName, List<Ingredient> incoming)
    {
        using (var context = new RecipeDbContext())
        {
            // //var recipe = context.Recipes.Include(r => r.Ingredients).FirstOrDefault(r => r.Name == recipeName);
            // var recipe = context.Recipes.FirstOrDefault(r => r.Name == recipeName);
            //
            // //recipe.Ingredients.AddRange(ingredients);
            //
            // foreach (var ingredient in ingredients)
            // {
            //     ingredient.RecipeId = recipe.Id; // make sure it's linked
            //     recipe.Ingredients.Add(ingredient);
            // }
            //
            // context.SaveChanges();
            
            var dbRecipe = context.Recipes
                .Include(r => r.Ingredients)
                .SingleOrDefault(r => r.Name == recipeName);

            if (dbRecipe is null)
                throw new InvalidOperationException($"Recipe '{recipeName}' not found.");
            
            // // REMOVE missing ingredients
            // var incomingIds = incoming
            //     .Where(i => i.Name != null)
            //     .Select(i => i.Name)
            //     .ToList();
            //
            // var toRemove = dbRecipe.Ingredients
            //     .Where(i => !incomingIds.Contains(i.Name))
            //     .ToList();

            //dbRecipe.Ingredients.RemoveRange(toRemove);
            // context.RemoveRange(toRemove);


            // 2) UPDATE or ADD incoming items
            foreach (var u in incoming)
            {
                if (u.IngredientId == 0)
                {
                    // New ingredient
                    u.RecipeId = dbRecipe.Id;      // ensure FK set
                    dbRecipe.Ingredients.Add(u);    // add directly (or dbRecipe.Ingredients.Add(u))
                }
                else
                {
                    // Existing: find & update tracked one
                    var ex = dbRecipe.Ingredients.FirstOrDefault(i => i.IngredientId == u.IngredientId);
                    if (ex is null)
                    {
                        // Not currently attached to collection (e.g., coming from elsewhere) -> treat as add
                        u.RecipeId = dbRecipe.Id;
                        dbRecipe.Ingredients.Add(u);
                    }
                    else
                    {
                        // Update scalar fields
                        ex.Name   = u.Name;
                        ex.Amount = u.Amount;
                        ex.Unit   = u.Unit;
                        // ... any other fields
                    }
                }
            }

            // 3) DELETE ones that were removed in memory
            var incomingIds = incoming.Where(i => i.IngredientId != 0).Select(i => i.IngredientId).ToHashSet();
            // var incomingIds = incoming.Where(i => i.Name != null).Select(i => i.Name).ToHashSet();
            var toRemove = dbRecipe.Ingredients
                .Where(i => i.IngredientId != 0 && !incomingIds.Contains(i.IngredientId))
                .ToList();
            // var toRemove = dbRecipe.Ingredients
            //     .Where(i => i.Name != null && !incomingIds.Contains(i.Name))
            //     .ToList();
         

// remove deleted ones
            // var toRemove = dbRecipe.Ingredients
            //     .Where(i => !incoming.Any(n => n.IngredientId == i.IngredientId))
            //     .ToList();


            context.RemoveRange(toRemove);

            // 4) Save
            context.SaveChanges();
            
        }
    }
    
    public void Delete(Recipe recipe)
    {
        using (var context = new RecipeDbContext())
        {
            context.Recipes.Remove(recipe);
            context.SaveChanges();
        }
    }
    
    public void DeleteAllNestedItem(int recipeId)
    {
        using (var context = new RecipeDbContext())
        {
            var dbRecipe = context.Recipes
                .Include(r => r.Ingredients)
                .SingleOrDefault(r => r.Id == recipeId);

           //  if (dbRecipe == null) return;
           //
           //  context.ingredients.RemoveRange(dbRecipe.Ingredients);
           //  
           // context.ingredients.RemoveRange(dbRecipe.Ingredients);
           
           //var incomingIds = incoming.Where(i => i.IngredientId != 0).Select(i => i.IngredientId).ToHashSet();
         
           var toRemove = dbRecipe.Ingredients
               .Where(i => i.IngredientId != -1 )
               .ToList();
           
           

           context.RemoveRange(toRemove);

            context.SaveChanges();
        }
        
    }
    public void ExportDB()
    {
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "recipes.json");
        
        using (var context = new RecipeDbContext())
        {
            var itemsToExport = context.Recipes.Include(r => r.Ingredients).ToList();
            
            var json = JsonSerializer.Serialize(
                itemsToExport,
                new JsonSerializerOptions
                {
                    WriteIndented = true // makes it human-readable
                });
            
            File.WriteAllText(filePath, json);

            Console.WriteLine($"Exported {itemsToExport.Count} recipes to {filePath}");
        }   
    }

    public void ImportDB()
    {
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "recipes.json");

        if (!File.Exists(filePath))
        {
            Console.WriteLine("No file found");
        }
        else
        {
            var recipeJson = File.ReadAllText(filePath);
            var itemsToImport = JsonSerializer.Deserialize<List<Recipe>>(recipeJson);

            using (var context = new RecipeDbContext())
            {
                foreach (var item in itemsToImport)
                    if (!context.Recipes.Any(r => r.Name == item.Name))
                    {
                        context.Recipes.Add(item);
                        Console.WriteLine($"Imported {item.Name} to DB");
                        context.SaveChanges();
                        
                    }
            }
            
        }
        
        
    }

}