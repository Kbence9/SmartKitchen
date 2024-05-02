using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace SmartKitchen.Model;

[Table("users")]
public class User : IdentityUser
{
    public Refrigerator Refrigerator;
}