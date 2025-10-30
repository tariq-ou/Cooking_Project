using Cooking_Project.Application.Services;
using CookingAPI.DTO;

namespace CookingAPI.Mapping;

public interface IRecipeMapper
{
    List<RecipeDTO> CreateRecipeList(IEnumerable<IRecipe> recipes);
    
    List<IngredientDTO> CreateIngredientList(IEnumerable<Ingredient> ingredients);
}