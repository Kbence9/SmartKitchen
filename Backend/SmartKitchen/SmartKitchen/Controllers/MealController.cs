using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartKitchen.Model;
using SmartKitchen.Service.Repository;

namespace SmartKitchen.Controllers;
[ApiController]
[Route("[controller]/[action]")]
public class MealController : ControllerBase
{
    private readonly IMealRepository _mealRepository;
    private readonly UserManager<User> _userManager;
    private readonly IConfigurationRoot _config;

    public MealController(
        IMealRepository mealRepository, 
        UserManager<User> userManager)
    {
        _mealRepository = mealRepository;
        _userManager = userManager;
        _config = new ConfigurationBuilder()
            .AddUserSecrets<MealController>()
            .Build();
    }

    [HttpGet]
    public async Task<string> GetMeal(string mealName)
    {
        try
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{_config["BASE_URL_RECIPES"]}&q={mealName}&app_id={_config["APPLICATION_ID_RECIPES"]}&app_key={_config["APPLICATION_KEY_RECIPES"]}");
            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception e)
        {
            return "Meal not found";
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<Meal>>> GetMeals()
    {
        try
        {
            var meals = _mealRepository.GetAllMeal();
            return Ok(meals);
        }
        catch (Exception e)
        {
            return NotFound("Meals not found");
        }
    }

    [HttpPost]
    public async Task<ActionResult<int>> AddMeal(string mealName)
    {
        try
        {
            _mealRepository.AddMeal(new Meal(mealName));
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return Problem("Meal failed to add");
        }
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteMeal(string mealName)
    {
        try
        {
            var mealToDelete = _mealRepository.GetMeal(mealName);
            _mealRepository.DeleteMeal(mealToDelete);
            return Ok("Meal deleted");
        }
        catch (Exception e)
        {
            return Problem("Meal failed to delete");
        }
    }
    
    [HttpPatch]
    public async Task<ActionResult> UpdateMeal(Meal meal)
    {
        try
        {
            _mealRepository.UpdateMeal(meal);
            return Ok("Done.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }

}