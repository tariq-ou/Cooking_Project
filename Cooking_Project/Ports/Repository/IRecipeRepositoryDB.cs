using Cooking_Project.Application.Services;

namespace Cooking_Project.Application.Ports;

public interface IRecipeRepositoryDB : IRepository<Recipe>, IRepositoryDB
{
    
    // public void Save(Recipe recipe);
    // public List<Recipe> ReadAll();
    //
    // public void SaveIngredient(string recipeName, List<Ingredient> ingredients);
    //
    // public void Delete(Recipe recipe);
    //
    // public void ExportDB();
    //
    // public void ImportDB();
    
    public void SaveNestedItem(string recipeName, List<Ingredient> ingredients);
    
    
}