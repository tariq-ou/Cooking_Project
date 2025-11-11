using Cooking_Project.Application.Services;
using CookingAPI.DTO;

namespace CookingAPI.Mapping;

public interface IRecipeMapper
{
    List<RecipeDTO> CreateRecipeListDTO(IEnumerable<IRecipe> recipes);
    
    List<IngredientDTO> CreateIngredientListDTO(IEnumerable<Ingredient> ingredients);
    
    RecipeDTO CreateRecipeDTO(IRecipe recipe);

    IRecipe CreateRecipeFromDTO(RecipeDTO recipeDTO);

    List<Ingredient> CreateIngredientList(IEnumerable<IngredientDTO> ingredientsInput);
}