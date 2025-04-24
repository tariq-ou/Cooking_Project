using Cooking_Project.Application.Infastrucuture;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Cooking_Project.Application.Ports;
using Cooking_Project.Application.Services;

namespace Cooking_Project.Application.Adaptors;

public class ERecipeRepository: IRecipeRepository
{
    public void Save(Recipe recipe)
    {
        using (var context = new RecipeDbContext())
        {
            if (context.Recipes.Any(r => r.Name == recipe.Name))
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

    public void SaveIngredient(string recipeName, List<Ingredient> ingredients)
    {
        using (var context = new RecipeDbContext())
        {
            //var recipe = context.Recipes.Include(r => r.Ingredients).FirstOrDefault(r => r.Name == recipeName);
            var recipe = context.Recipes.FirstOrDefault(r => r.Name == recipeName);
            
            //recipe.Ingredients.AddRange(ingredients);
            
            foreach (var ingredient in ingredients)
            {
                ingredient.RecipeId = recipe.Id; // make sure it's linked
                recipe.Ingredients.Add(ingredient);
            }

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

}