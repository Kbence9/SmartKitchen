using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartKitchen.Model;
using SmartKitchen.Service.Repository;

namespace SmartKitchen.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class IngredientController : ControllerBase
{
    private readonly IIngredientRepository _ingredientRepository;
    private readonly UserManager<User> _userManager;
    private readonly IConfigurationRoot _config;

    public IngredientController(
        IIngredientRepository ingredientRepository, 
        UserManager<User> userManager)
    {
        _ingredientRepository = ingredientRepository;
        _userManager = userManager;
        _config = new ConfigurationBuilder()
            .AddUserSecrets<IngredientController>()
            .Build();
    }

    [HttpGet]
    public async Task<string> GetIngredient(string ingredientName)
    {
        try
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{_config["BASE_URL_FOOD"]}?app_id={_config["APPLICATION_ID_FOOD"]}&app_key={_config["APPLICATION_KEY_FOOD"]}&ingr={ingredientName}&nutrition-type=cooking");
            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception e)
        {
            return "Ingredient not found";
        }
    }

    [HttpPost]
    public async Task<ActionResult<int>> AddIngredient(string name, float enercKcal, float procnt, float fat, float chocdf, float fibtg, string? image)
    {
        try
        {
            _ingredientRepository.AddIngredient(new Ingredient(name, enercKcal, procnt, fat, chocdf, fibtg, image));
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return Problem("Ingredient failed to add");
        }
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteIngredient(string ingredientName)
    {
        try
        {
            var ingredientToDelete = _ingredientRepository.GetIngredient(ingredientName);
            _ingredientRepository.DeleteIngredient(ingredientToDelete);
            return Ok("Ingredient deleted");
        }
        catch (Exception e)
        {
            return Problem("Ingredient failed to delete");
        }
    }
    
    [HttpPatch]
    public async Task<ActionResult> UpdateIngredient(Ingredient ingredient)
    {
        try
        {
            _ingredientRepository.UpdateIngredient(ingredient);
            return Ok("Done.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }

}