using Cooking_Project.Application.Services;

namespace Cooking_Project.Application.Ports;

public interface IRecipeRepository
{
    
    public void Save(Recipe recipe);
    public List<Recipe> ReadAll();
    
    public void SaveIngredient(string recipeName, List<Ingredient> ingredients);
    
    public void Delete(Recipe recipe);
    
    public void ExportDB();
    
    //importdb
    
    
    
}