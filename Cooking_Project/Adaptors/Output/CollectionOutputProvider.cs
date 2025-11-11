using Cooking_Project.Application.Ports;

namespace Cooking_Project.Application.Adaptors;

public class CollectionOutputProvider : IOutputProvider
{
    public void Output(string input)
    {
        Console.WriteLine(input);
    }
}