namespace CookingAPI.DTO;

public class CreateRecipeDTO
{
   
        public string Name { get; set; }
        public int Servings { get; set; }
        public List<CreateIngredientDTO> Ingredients { get; set; }
        public List<string> Steps { get; set; }
        
        public string? ImagePath { get; set; }
    
}