using Cooking_Project.Application.Services;
using AutoMapper;
using CookingAPI.DTO;
using System.Collections.ObjectModel;


namespace CookingAPI.Mapping;

// class to use automapper to create dto
public class RecipeAutoMapper: Profile, IRecipeMapper
{
    internal readonly IMapper _mapper;
    // creates the mappings for <target -> destination>
    public RecipeAutoMapper(IMapper mapper)
    {
        // CreateMap<Recipe, RecipeDTO>();
        // CreateMap<Ingredient, IngredientDTO>();
        _mapper = mapper;
    }

    public List<RecipeDTO> CreateRecipeListDTO(IEnumerable<IRecipe> recipes)
    {
        _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        var recipesDTO = new List<RecipeDTO>();

        foreach (IRecipe recipe in recipes)
        {
            recipesDTO.Add(_mapper.Map<RecipeDTO>(recipe));
        }
        
        return recipesDTO;
    }


    public List<IngredientDTO> CreateIngredientListDTO(IEnumerable<Ingredient> ingredients)
    {
        _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        var ingredientDTO = new List<IngredientDTO>();

        foreach (Ingredient ingredient in ingredients)
        {
            ingredientDTO.Add(_mapper.Map<IngredientDTO>(ingredient));
        }

        return ingredientDTO;

    }
    
    public RecipeDTO CreateRecipeDTO(IRecipe recipe)
    {
        _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        RecipeDTO recipeDTO = _mapper.Map<RecipeDTO>(recipe);

        

        return recipeDTO;

    }
    
}