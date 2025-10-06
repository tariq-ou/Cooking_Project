using Cooking_Project.Application.Adaptors;
using Cooking_Project.Application.Domain;
using Cooking_Project.Application.Ports;
using Microsoft.Extensions.Configuration;
using Cooking_Project.Helper;
using Microsoft.Extensions.DependencyInjection;

namespace Cooking_Project.Factory;

public static class RepositoryFactory
{
    public static IRecipeRepositoryDB Create(IServiceProvider serviceProvider)
    {
        var config = Helper.Config.ConfigReader();
        var backend = config["Repository:BackEnd"];
        
        switch (backend)
        {
            case"Database":
                var repository = new ERecipeRepository(serviceProvider.GetRequiredService<IRecipeManager>());
                repository.SyncDBMemory();
                return repository;
                
                break;
            default:
                return null;
                break;
        }
    }
}