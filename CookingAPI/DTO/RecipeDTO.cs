namespace CookingAPI.DTO;

public class RecipeDTO
{
    public string name;
    string serving;
    public List<IngredientDTO> ingredients;
    public List<string> steps;
}