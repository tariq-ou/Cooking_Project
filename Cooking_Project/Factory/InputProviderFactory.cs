using Cooking_Project.Application.Adaptors;
using Cooking_Project.Application.Ports;

namespace Cooking_Project.Factory;

public static class InputProviderFactory
{
    
    //mainly used for when syncing database and needing to create a inputprovider per synced object
    public static IInputProvider Create()
    {
        var config = Helper.Config.ConfigReader();
        var backend = config["Manager:Inputprovider"];
        
        switch (backend)
        {
            case"local":
                return new ConsoleInputProvider();
                break;
            case"web":
                return new IInputProviderTest("empty");
                break;
            default:
                return null;
                break;
        }
    }
}