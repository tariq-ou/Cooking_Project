using Cooking_Project.Application.Services;
using AutoMapper;
using CookingAPI.DTO;
using System.Collections.ObjectModel;


namespace CookingAPI.Mapping;

// class to use automapper to create dto
public class RecipeAutoMapper: IRecipeMapper
{
    internal readonly IMapper _mapper;

    private readonly ILogger<RecipeAutoMapper> _logger;
    // creates the mappings for <target -> destination>
    public RecipeAutoMapper(IMapper mapper, ILogger<RecipeAutoMapper> logger)
    {
        // CreateMap<Recipe, RecipeDTO>();
        // CreateMap<Ingredient, IngredientDTO>();
        _mapper = mapper;
        _logger = logger;
    }

    public List<RecipeDTO> CreateRecipeListDTO(IEnumerable<IRecipe> recipes)
    {
        _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        var recipesDTO = new List<RecipeDTO>();

        foreach (IRecipe recipe in recipes)
        {
            recipesDTO.Add(_mapper.Map<RecipeDTO>(recipe));
        }
        _logger.LogInformation($"RecipesDTO list Created, Count: {recipesDTO.Count}");
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
        
        _logger.LogInformation($"IngredientsDTO list Created, Count: {ingredientDTO.Count}");
        return ingredientDTO;

    }
    
    public RecipeDTO CreateRecipeDTO(IRecipe recipe)
    {
        _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        RecipeDTO recipeDTO = _mapper.Map<RecipeDTO>(recipe);
        
        return recipeDTO;

    }
    
    public IRecipe CreateRecipeFromDTO(RecipeDTO recipeDTO)
    {
        _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        var recipeCreated = _mapper.Map<Recipe>(recipeDTO);
        

        return recipeCreated;

    }
    
    public List<Ingredient> CreateIngredientList(IEnumerable<IngredientDTO> ingredientsInput)
    {
        _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        var ingredients = new List<Ingredient>();

        foreach (IngredientDTO ingredient in ingredientsInput)
        {
            ingredients.Add(_mapper.Map<Ingredient>(ingredient));
        }
        
        _logger.LogInformation($"Ingredients list Created, Count: {ingredients.Count}");
        
        return ingredients;

    }
    
}