using Cooking_Project.Application.Services;
using AutoMapper;
using CookingAPI.DTO;

namespace CookingAPI.Mapping;

// class to use automapper to create dto
public class RecipeProfile: Profile
{

    // creates the mappings for <target -> destination>
    public RecipeProfile()
    {
        CreateMap<Recipe, RecipeDTO>();
        CreateMap<Ingredient, IngredientDTO>();
    }
    
    
    
}