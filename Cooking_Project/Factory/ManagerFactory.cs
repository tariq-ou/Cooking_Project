using Cooking_Project.Application.Adaptors;
using Cooking_Project.Application.Domain;

namespace Cooking_Project.Factory;

public static class ManagerFactory
{
    
    // ATM not in use! using dependency injection
    public static IRecipeManager Create()
    {
        var config = Helper.Config.ConfigReader();
        var backend = config["Manager:Inputprovider"];
        
        switch (backend)
        {
            case"local":
                return new RecipeManager(new ConsoleInputProvider());
                break;
            case"web":
                return new RecipeManager(new IInputProviderTest("empty"));
                break;
            default:
                return null;
                break;
        }
    }
}