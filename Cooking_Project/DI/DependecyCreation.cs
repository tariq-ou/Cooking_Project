using Cooking_Project.Application.Domain;
using System;
using Cooking_Project.Application.Adaptors;
using Cooking_Project.Application.Ports;

namespace Cooking_Project.DI;
using Microsoft.Extensions.DependencyInjection;


public static class DependecyCreation
{

    public static IServiceProvider Configure()
    {
        // 1. Create the DI container
        var services = new ServiceCollection();

        // 2. Register dependencies
        services.AddSingleton<IInputProvider, ConsoleInputProvider>();
        services.AddSingleton<IOutputProvider, CollectionOutputProvider>();
        services.AddSingleton<IRecipeManager, RecipeManager>();
        //services.AddSingleton<RecipeManager>();

        // 3. Build the provider
        return services.BuildServiceProvider();

        //return serviceProvider;

        // 4. Resolve the entry point
        //var manager = serviceProvider.GetRequiredService<RecipeManager>();
    }

}