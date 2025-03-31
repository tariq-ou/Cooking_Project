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

    

}