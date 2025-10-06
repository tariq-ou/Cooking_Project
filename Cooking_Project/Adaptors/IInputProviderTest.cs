using Cooking_Project.Application.Ports;

namespace Cooking_Project.Application.Adaptors;

public class IInputProviderTest : IInputProvider
{
    private string input;

     public IInputProviderTest(string promt)
     {
         input = promt;
     }
    public string ReadInput(string ignore)
    {
        
        return input;
        //throw new System.NotImplementedException();
    }
    
    public string ReadInput()
    {
        
        return input;
        //throw new System.NotImplementedException();
    }
    
    
}