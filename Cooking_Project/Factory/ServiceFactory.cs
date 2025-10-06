using Cooking_Project.Application.Adaptors;
using Cooking_Project.Application.Domain;
using Cooking_Project.Application.Ports;
using Cooking_Project.Application.Services;
using Microsoft.Extensions.Configuration;
using Cooking_Project.Helper;

namespace Cooking_Project.Factory;

public static class ServiceFactory
{

    public static IRecipeDBService Create(IServiceProvider serviceProvider)
    {
        var config = Helper.Config.ConfigReader();
        var backend = config["BackEnd"];
        
        switch ("Database")
        {
            case"Database":
                var service = new RecipeService(RepositoryFactory.Create(serviceProvider));
                return service;
                
                break;
            default:
                return null;
                break;
        }
    }
    
}