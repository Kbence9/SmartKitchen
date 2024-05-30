using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartKitchen.Model;

namespace SmartKitchen.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly UserManager<User> _userManager;

    public UserController(
        ILogger<UserController> logger, 
        UserManager<User> userManager)
    {
        _logger = logger;
        _userManager = userManager;
    }

    [HttpGet("GetUser"), Authorize(Roles = "Customer, Company, Admin")]
    public async Task<ActionResult<User>> GetUser()
    {
        try
        {
            var user = await _userManager.Users
                .SingleOrDefaultAsync(user1 => user1.UserName == User.Identity.Name);
            return Ok(user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("GetAllUsers")]
    public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
    {
        try
        {
            var users =  _userManager.Users
                .AsEnumerable();
            
            return Ok(users);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("GetRole")]
    public async Task<ActionResult<string>> GetRole(string id )
    {
        if (id != null)
        {
            var user = await _userManager.FindByIdAsync(id);
            
            var roles = await _userManager.GetRolesAsync(user);
            return roles[0];
        }

        return "nothing";
    }
    
    [HttpDelete("DeleteUserForAdmin")]
    public async Task<ActionResult> DeleteUser(string id)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(id);
            
            if (user == null)
            {
                return NotFound("User was not found.");
            }
            
            var identityResult = await _userManager.DeleteAsync(user);
            

            if (!identityResult.Succeeded)
            {
                return BadRequest(identityResult.Errors);
            }
            
            return Ok("Successfully deleted user.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpDelete("DeleteUser")]
    public async Task<ActionResult> DeleteUser()
    {
        try
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            
            if (user == null)
            {
                return NotFound("User was not found.");
            }
            
            var identityResult = await _userManager.DeleteAsync(user);
            
            if (!identityResult.Succeeded)
            {
                return BadRequest(identityResult.Errors);
            }
            
            return Ok("Successfully deleted user.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPatch("UpdateUser")]
    public async Task<ActionResult> UpdateCustomer(string userName, string email, string phoneNumber)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            
            user.UserName = userName;
            user.Email = email;
            user.PhoneNumber = phoneNumber;
            var identityResult = await _userManager.UpdateAsync(user);
            
            if (!identityResult.Succeeded)
            {
                return BadRequest(identityResult.Errors);
            }
            
            return Ok("Successfully updated user.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
}