namespace CookingAPI.DTO;

public class RecipeDTO
{
    public string Name;
    string serving;
    public List<IngredientDTO> ingredients;
    public List<string> Steps;
}