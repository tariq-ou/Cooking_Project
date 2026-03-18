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
    
    public RecipeDTO MapRecipeDTOFromCreateId(CreateRecipeDTO recipe, int id)
    {
        _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        _logger.LogInformation($"CreateRecipeDTO image path: {recipe.ImagePath}");
        //RecipeDTO recipeDTO = _mapper.Map<RecipeDTO>(recipe);
        var recipeDTO = new RecipeDTO()
        {
            Id = id,
            Name = recipe.Name,
            Ingredients = new List<IngredientDTO>(),
            Servings = recipe.Servings,
            Steps = recipe.Steps,
            ImagePath = recipe.ImagePath
        };
        
        _logger.LogInformation($"RecipeDTO image path: {recipe.ImagePath}");
        //recipeDTO.Id = id;

        return recipeDTO;
    }
    
    //mabye not used?
    public IRecipe CreateRecipeFromDTO(CreateRecipeDTO recipeDTO)
    {
        _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        var recipeCreated = _mapper.Map<Recipe>(recipeDTO);
        

        return recipeCreated;

    }
    
    public IRecipe CreateRecipeFromExistingDTO(RecipeDTO recipeDTO)
    {
        _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        var recipeCreated = _mapper.Map<Recipe>(recipeDTO);
        

        return recipeCreated;

    }
    
    public List<Ingredient> CreateIngredientList(IEnumerable<CreateIngredientDTO> ingredientsInput)
    {
        _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        var ingredients = new List<Ingredient>();
        
        //_logger.LogInformation($"Ingredients list recived, Count: {ingredientsInput.Count}");
        
        foreach (CreateIngredientDTO ingredient in ingredientsInput)
        {
            //ingredients.Add(_mapper.Map<Ingredient>(ingredient));
            ingredients.Add(new Ingredient()
            {
                Name = ingredient.Name,
                Amount = ingredient.Amount,
                Unit = ingredient.Unit,
            });
        }
        
        _logger.LogInformation($"Ingredients list Created, Count: {ingredients.Count}");
        
        return ingredients;

    }
    
    public List<Ingredient> CreateFromExistingIngredientList(IEnumerable<IngredientDTO> ingredientsInput)
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