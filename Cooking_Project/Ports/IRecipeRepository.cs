using Cooking_Project.Application.Services;

namespace Cooking_Project.Application.Ports;

public interface IRecipeRepository
{
    
    public void Save(Recipe recipe);
    //public void Read(Recipe recipe);
    //public void Delete(Recipe recipe);
    
    
}