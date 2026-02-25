using Cooking_Project.Application.Adaptors;
using Cooking_Project.Application.Domain;
using Cooking_Project.Application.Ports;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Microsoft.Identity.Abstractions;
using Microsoft.Identity.Web.Resource;
using AutoMapper;
using Cooking_Project.Application.Infastrucuture;
using Cooking_Project.Application.Services;
using Cooking_Project.Factory;
using CookingAPI.DTO;
using CookingAPI.Mapping;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;


////////Next
/*
 
 
 -- fix unit test for add recipe to actually pass in Ifile and check if its stored location ect
 
 -- (can skip for now if feeling long)write unit tests for entity frame work mabye? repoitpory tests


-- add logger to recipeservice and anything else you think needs it like mabye the repository also?


----add asyncronous call back to your DB calls as dont need to wait for that thread right?
----llearn about generics and see if you need to add them in...
 */
var builder = WebApplication.CreateBuilder(args);

//intialising



// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAdB2C"));
builder.Services.AddAuthorization();
builder.Services.AddSingleton<IInputProvider, ConsoleInputProvider>();
builder.Services.AddSingleton<IOutputProvider, CollectionOutputProvider>();
builder.Services.AddSingleton<IRecipeManager, RecipeManager>();
builder.Services.AddSingleton<IRecipeManagerAPI, RecipeManagerAPI>();
builder.Services.AddAutoMapper(typeof(RecipeProfile));
builder.Services.AddSingleton<IRecipeMapper, RecipeAutoMapper>();
//builder.Services.AddSingleton<IRepository<Recipe>, ERecipeRepository>();
builder.Services.AddSingleton<IRecipeRepositoryDB, ERecipeRepository>();
builder.Services.AddSingleton<IRecipeDBService, RecipeService>();
//builder.Services.AddSingleton();




//create recipeservice and passthrough dependecy injection for type of output save
//var recipeService = new RecipeService(RepositoryFactory.Create());
//var recipeManager = serviceProvider.GetRequiredService<IRecipeManager>();

//var service = ServiceFactory.Create(serviceProvider);



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

var app = builder.Build();

//Creating DB
RecipeDbContext.CreateDatabase();
// syncing db


using (var scope = app.Services.CreateScope())
{
    var recipeRepo = scope.ServiceProvider.GetRequiredService<IRecipeRepositoryDB>();
    recipeRepo.SyncDBMemory();
}

//Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
Console.WriteLine(app.Environment.EnvironmentName);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.MapControllers(); 


//http://localhost:5000/swagger/index.html

// var scopeRequiredByApi = app.Configuration["AzureAd:Scopes"] ?? "";
// var summaries = new[]
// {
//     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
// };
//
// app.MapGet("/weatherforecast", (HttpContext httpContext) =>
//     {
//         httpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
//
//         var forecast = Enumerable.Range(1, 5).Select(index =>
//                 new WeatherForecast
//                 (
//                     DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//                     Random.Shared.Next(-20, 55),
//                     summaries[Random.Shared.Next(summaries.Length)]
//                 ))
//             .ToArray();
//         return forecast;
//     })
//     .WithName("GetWeatherForecast")
//     .WithOpenApi()
//     .RequireAuthorization();



app.Run();



// record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// }