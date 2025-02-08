using Cooking_Project.Application.Ports;

namespace Cooking_Project_Tests.PortsTest;

public class IInputProviderTest : IInputProvider
{
    string recipeName;

    public IInputProviderTest(string promt)
    {
        recipeName = promt;
    }
    public string ReadInput(string ignore)
    {
        
        return recipeName;
        //throw new System.NotImplementedException();
    }
}