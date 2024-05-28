using Microsoft.AspNetCore.Identity;

namespace SmartKitchen.Services.Authentication;

public interface ITokenService
{
    public string CreateToken(IdentityUser user, string role);
}