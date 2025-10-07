using Cooking_Project.Application.Adaptors;
using Cooking_Project.Application.Domain;
using Cooking_Project.Application.Ports;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Microsoft.Identity.Abstractions;
using Microsoft.Identity.Web.Resource;



////////Next
/*

--- you need to create some DTO classes which you have and fill them out
    then - try get automapper from nuget and see if you can get it to do the creation of the objects 
    then thats what your code will pass back
    then add that into the return type for the unit test


add asyncronous call back to your DB calls as dont need to wait for that thread right?
llearn about generics and see if you need to add them in...
 */
var builder = WebApplication.CreateBuilder(args);

//intialising



// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAdB2C"));
builder.Services.AddAuthorization();
builder.Services.AddSingleton<IInputProvider, ConsoleInputProvider>();
builder.Services.AddSingleton<IRecipeManager, RecipeManager>();
//builder.Services.AddSingleton(typeof(RecipeManager));
//builder.Services.AddSingleton();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var scopeRequiredByApi = app.Configuration["AzureAd:Scopes"] ?? "";
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", (HttpContext httpContext) =>
    {
        httpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);

        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast")
    .WithOpenApi()
    .RequireAuthorization();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}