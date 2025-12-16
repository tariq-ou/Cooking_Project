namespace CookingAPI.DTO;

public class RecipeDTO
{
    public string Name { get; set; }
    public int Servings{ get; set; }
    public List<IngredientDTO> Ingredients{ get; set; }
    public List<string> Steps{ get; set; }
}