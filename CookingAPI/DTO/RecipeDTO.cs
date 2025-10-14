namespace CookingAPI.DTO;

public class RecipeDTO
{
    public string recipeName;
    string serving;
    public List<IngredientDTO> ingredients;
    public List<string> Steps;
}