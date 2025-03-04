using Cooking_Project.Application.Ports;

namespace Cooking_Project.Application.Services;

public class Ingredient
{
    public string Name { get; set; }
    public int Amount { get; set; }
    public string Unit { get; set; }
    
    internal IInputProvider InputProvider { get; set; }

    public Ingredient(IInputProvider _inputProvider)
    {
        // Name = name;
        // Amount = amount;
        // Unit = unit;
        InputProvider = _inputProvider;
    }

    public Ingredient AddIngredient()
    {
        Console.WriteLine("Enter ingredient name, amount & unit of amount");

        
            this.Name = InputProvider.ReadInput("Enter ingredient name");
            
            if (int.TryParse(InputProvider.ReadInput("Enter ingredient name"), out int amount))
                this.Amount = amount;
            else
            {
                Console.WriteLine("Please enter a valid number");
                return null;
            }
            
            this.Unit = InputProvider.ReadInput("Enter ingredient amounts unit");
            
            return this;
        

    }
    
}