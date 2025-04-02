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
            context.Recipes.Add(recipe);
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
            var recipe = context.Recipes.Include(r => r.Ingredients).FirstOrDefault(r => r.Name == recipeName);
            
            recipe.Ingredients.AddRange(ingredients);
            context.SaveChanges();
        }
    }

}