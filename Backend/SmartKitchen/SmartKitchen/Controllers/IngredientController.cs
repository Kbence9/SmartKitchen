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

    public IngredientController(
        IIngredientRepository ingredientRepository, 
        UserManager<User> userManager)
    {
        _ingredientRepository = ingredientRepository;
        _userManager = userManager;
    }

    [HttpGet]
    public ActionResult<Ingredient> GetIngredient(string ingredientName)
    {
        try
        {
            var ingredient = _ingredientRepository.GetIngredient(ingredientName);
            return Ok(ingredient);
        }
        catch (Exception e)
        {
            return NotFound("Ingredient not found");
        }
    }

    [HttpPost]
    public async Task<ActionResult<int>> AddIngredient(Ingredient ingredientToAdd)
    {
        try
        {
            _ingredientRepository.AddIngredient(ingredientToAdd);
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