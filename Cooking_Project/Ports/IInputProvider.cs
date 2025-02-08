using Cooking_Project.Application.Adaptors;

namespace Cooking_Project.Application.Ports
{
    public interface IInputProvider
    {
        string ReadInput(string promt);
    }
}