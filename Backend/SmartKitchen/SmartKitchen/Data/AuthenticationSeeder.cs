using Microsoft.AspNetCore.Identity;
using SmartKitchen.Model;

namespace ElProyecteGrandeBackend.Data;

public class AuthenticationSeeder
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<User> _userManager;
    private readonly IConfigurationRoot _config;
    
    public AuthenticationSeeder(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _config = new ConfigurationBuilder()
            .AddUserSecrets<AuthenticationSeeder>()
            .Build();
    }
    
    public void AddRoles()
    {
        var tAdmin = CreateAdminRole();
        tAdmin.Wait();

        var tUser = CreateUserRole();
        tUser.Wait();
    }

    async Task CreateAdminRole()
    {
        await _roleManager.CreateAsync(new IdentityRole(_config["AdminRole"]   != null ? _config["AdminRole"] : Environment.GetEnvironmentVariable("ADMINROLE")));
    }

    async Task CreateUserRole()
    {
        await _roleManager.CreateAsync(new IdentityRole(_config["UserRole"]  != null ? _config["UserRole"] : Environment.GetEnvironmentVariable("USERROLE")));
    }

    public void AddAdmin()
    {
        var tAdmin = CreateAdminIfNotExists();
        tAdmin.Wait();
    }

    async Task CreateAdminIfNotExists()
    {
        var adminInDb = await _userManager.FindByEmailAsync("admin@admin.hu");
        if (adminInDb == null)
        {
            var admin = new User { UserName = "admin", Email = "admin@admin.hu" };
            var adminCreated = await _userManager.CreateAsync(admin, _config["AdminPassword"] != null ? _config["AdminPassword"] : Environment.GetEnvironmentVariable("ADMINPASSWORD"));

            if (adminCreated.Succeeded)
            {
                await _userManager.AddToRoleAsync(admin, _config["AdminRole"] != null ? _config["AdminRole"] : Environment.GetEnvironmentVariable("ADMINROLE"));
            }
        }
    }
}