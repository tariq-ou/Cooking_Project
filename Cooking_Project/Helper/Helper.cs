namespace Cooking_Project.Helper;
using Microsoft.Extensions.Configuration;

public static class Config
{

    public static IConfigurationRoot ConfigReader()
    {
        return new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
    }

}