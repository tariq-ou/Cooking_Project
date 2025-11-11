using Cooking_Project.Application.Services;

namespace Cooking_Project.Application.Ports;
using System.Collections.Generic;

public interface IRepository<T>
{
    public void Save(T recipe);
    
    public List<T> ReadAll();
    
    public void Delete(T recipe);
    
    public void SaveNestedItem(string recipeName, List<Ingredient> ingredients);
}