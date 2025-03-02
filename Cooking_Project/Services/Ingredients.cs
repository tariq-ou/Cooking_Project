namespace Cooking_Project.Application.Services;

public class Ingredients
{
    public string Name { get; set; }
    public int Amount { get; set; }
    public int Unit { get; set; }

    Ingredients(string name, int amount, int unit)
    {
        Name = name;
        Amount = amount;
        Unit = unit;
    }
}