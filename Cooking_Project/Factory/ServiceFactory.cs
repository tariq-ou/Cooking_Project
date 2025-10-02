using Cooking_Project.Application.Ports;
using Cooking_Project.Application.Services;
using Microsoft.Extensions.Configuration;
using Cooking_Project.Helper;

namespace Cooking_Project.Factory;

public static class ServiceFactory
{

    public static IRecipeDBService Create()
    {
        var config = Helper.Config.ConfigReader();
        var backend = config["BackEnd"];
        
        switch ("Database")
        {
            case"Database":
                return new RecipeService(RepositoryFactory.Create());
                break;
            default:
                return null;
                break;
        }
    }
    
}