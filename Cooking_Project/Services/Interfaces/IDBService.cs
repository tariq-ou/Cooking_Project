using Cooking_Project.Application.Domain;
using Cooking_Project.Application.Ports;

namespace Cooking_Project.Application.Services;

public interface IDBService
{
    void ReadExportDB();
    void ImportToDB();
    void SyncDBMemory(IRecipeManager recipeManager, Func<IInputProvider> inputProviderFactory);
}