using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartKitchen.Contracts;
using SmartKitchen.Services;
using SmartKitchen.Services.Authentication;

namespace SmartKitchen.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authenticationService;
    private readonly IConfigurationRoot _config;
    
    public AuthController(IAuthService authenticationService)
    {
        _authenticationService = authenticationService;
        _config = new ConfigurationBuilder()
            .AddUserSecrets<AuthController>()
            .Build();;
    }

    [HttpPost("Register")]
    public async Task<ActionResult<RegistrationResponse>> RegisterUser(RegistrationRequest request)
    {
        var result = await _authenticationService.RegisterAsync(request.Email, request.Username, request.Password, _config["UserRole"] != null
            ? _config["UserRole"] : Environment.GetEnvironmentVariable("USERROLE"));
        Console.WriteLine(result);
        if (!result.Success)
        {
            AddErrors(result);
            return BadRequest(ModelState);
        }

        return CreatedAtAction(nameof(RegisterUser), new RegistrationResponse(result.Email, result.UserName));

    }

    private void AddErrors(AuthResult result)
    {
        foreach (var error in result.ErrorMessages)
        {
            ModelState.AddModelError(error.Key, error.Value);
        }
    }
    
    [HttpPost("Login")]
    public async Task<ActionResult> Authenticate([FromBody] AuthRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _authenticationService.LoginAsync(request.Email, request.Password);

        if (!result.Success)
        {
            AddErrors(result);
            return BadRequest(ModelState);
        }
        
        Response.Cookies.Append("User", result.Token, new CookieOptions() { HttpOnly = false, SameSite = SameSiteMode.Strict });

        return Ok();
    }
    
    [Authorize(Roles = "User, Admin")]
    [HttpPost("Logout")]
    public IActionResult Logout()
    {
        HttpContext.Response.Cookies.Delete("User");
        return Ok();
    }
}