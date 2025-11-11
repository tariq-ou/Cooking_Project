using Cooking_Project.Application.Ports;
using Cooking_Project.Application.Services;

namespace Cooking_Project.Application.Adaptors;

public class RepositoryTest: IRepository<Recipe>
{


    public void Save(Recipe recipe)
    {
        //throw new NotImplementedException();
    }

    public List<Recipe> ReadAll()
    {
        throw new NotImplementedException();
    }

    public void Delete(Recipe recipe)
    {
        throw new NotImplementedException();
    }

    public void ExportDB()
    {
        throw new NotImplementedException();
    }

    public void ImportDB()
    {
        throw new NotImplementedException();
    }

    public void SaveNestedItem(string recipeName, List<Ingredient> ingredients)
    {
        throw new NotImplementedException();
    }
}