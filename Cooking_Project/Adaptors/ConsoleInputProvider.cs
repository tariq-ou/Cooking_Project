using System;
using Cooking_Project.Application.Ports;

namespace Cooking_Project.Application.Adaptors
{
    public class ConsoleInputProvider : IInputProvider
    {
        public ConsoleInputProvider()
        {
        }

        public string ReadInput(string promt)
        {
            Console.WriteLine($"{promt}");
            return Console.ReadLine();
        }
        
        public string ReadInput()
        {
            return Console.ReadLine();
        }
    }
}
