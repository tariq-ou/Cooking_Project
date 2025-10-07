using Cooking_Project.Application.Ports;

namespace Cooking_Project.Application.Adaptors;

public class ConsoleOutputProvider: IOutputProvider
{

    public void Output(string input)
    {
        var output = new List<string>(); 
        output.Add(input);
    }
    
}