using Cooking_Project.Application.Services;
using CookingAPI.DTO;
using AutoMapper;

namespace CookingAPI.Mapping;

public class RecipeProfile:Profile
{
    public RecipeProfile()
    {
        // CreateMap<Recipe, RecipeDTO>();
        // CreateMap<Ingredient, IngredientDTO>();
        CreateMap<Recipe, RecipeDTO>().ReverseMap();
        CreateMap<Ingredient, IngredientDTO>().ReverseMap();

    }

}