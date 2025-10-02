using Cooking_Project.Application.Adaptors;
using Cooking_Project.Application.Ports;
using Microsoft.Extensions.Configuration;
using Cooking_Project.Helper;

namespace Cooking_Project.Factory;

public static class RepositoryFactory
{
    public static IRecipeRepositoryDB Create()
    {
        var config = Helper.Config.ConfigReader();
        var backend = config["Repository:BackEnd"];
        
        switch (backend)
        {
            case"Database":
                return new ERecipeRepository();
                break;
            default:
                return null;
                break;
        }
    }
}