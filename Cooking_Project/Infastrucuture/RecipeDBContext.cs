using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Cooking_Project.Application.Domain;
using Cooking_Project.Application.Services;

namespace Cooking_Project.Application.Infastrucuture
{
    public class RecipeDbContext: DbContext
    {
        public DbSet<Recipe> Recipes { get; set; }

        public static void CreateDatabase()
        {
            using (var context = new RecipeDbContext())
            {
                context.Database.EnsureCreated();
                Console.WriteLine("Database created or already exists!");
            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=recipes.db");
        }
    }
}