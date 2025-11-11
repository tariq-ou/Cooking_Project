namespace CookingAPI.DTO;

public class RecipeDTO
{
    public string Name;
    public int Servings;
    public List<IngredientDTO> Ingredients;
    public List<string> Steps;
}