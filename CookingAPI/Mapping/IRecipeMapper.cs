using Cooking_Project.Application.Services;
using CookingAPI.DTO;

namespace CookingAPI.Mapping;

public interface IRecipeMapper
{
    List<RecipeDTO> CreateRecipeListDTO(IEnumerable<IRecipe> recipes);
    
    List<IngredientDTO> CreateIngredientListDTO(IEnumerable<Ingredient> ingredients);
    
    RecipeDTO CreateRecipeDTO(IRecipe recipe);

    IRecipe CreateRecipeFromDTO(CreateRecipeDTO recipeDTO);

    List<Ingredient> CreateIngredientList(IEnumerable<CreateIngredientDTO> ingredientsInput);

    List<Ingredient> CreateFromExistingIngredientList(IEnumerable<IngredientDTO> ingredientsInput);

    IRecipe CreateRecipeFromExistingDTO(RecipeDTO recipeDTO);

    RecipeDTO MapRecipeDTOFromCreateId(CreateRecipeDTO recipe, int id);


}