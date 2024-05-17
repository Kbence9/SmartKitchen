using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartKitchen.Model;

namespace SmartKitchen.Data;

public class SmartKitchenContext : IdentityDbContext<User, IdentityRole, string>
{
    public DbSet<User> Users { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Meal> Meals { get; set; }

    public SmartKitchenContext()
    {
    }

    public SmartKitchenContext(DbContextOptions<SmartKitchenContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<User>()
            .HasOne(u => u.Refrigerator)
            .WithOne(r => r.user);
    }

}