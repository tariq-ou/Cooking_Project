using Cooking_Project.Application.Ports;

namespace Cooking_Project.Application.Services;

public interface IRecipe
{
    int Id { get; set; } // Primary key (required apprently)
    string Name { get; set; }
    List<Ingredient> Ingredients { get; set; }
    IInputProvider InputProvider{ get; set; }
    string StepsSerialized { get; set; }
    List<string> Steps { get; set; }
    int Servings { get; set; }
    
    public string? ImagePath { get; set; }
    void AddIngredients(string recipeName);
    void ViewIngredients(string recipeName);
    int IngredientsCount();
    void IngredientDelete(string recipeName);
    void AddSteps(string recipeName);
    void ViewSteps(string recipeName);
    void StepsDelete(string recipeName);
    int StepsCount();
}