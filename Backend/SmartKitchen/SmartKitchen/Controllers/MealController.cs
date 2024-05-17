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

    public MealController(
        IMealRepository mealRepository, 
        UserManager<User> userManager)
    {
        _mealRepository = mealRepository;
        _userManager = userManager;
    }

    [HttpGet]
    public ActionResult<Meal> GetMeal(string mealName)
    {
        try
        {
            var meal = _mealRepository.GetMeal(mealName);
            return Ok(meal);
        }
        catch (Exception e)
        {
            return NotFound("Meal not found");
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
    public async Task<ActionResult<int>> AddMeal(Meal mealToAdd)
    {
        try
        {
            _mealRepository.AddMeal(mealToAdd);
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