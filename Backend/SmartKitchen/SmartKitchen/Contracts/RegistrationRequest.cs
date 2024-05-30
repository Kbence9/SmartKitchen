using System.ComponentModel.DataAnnotations;

namespace SmartKitchen.Services;

public record RegistrationRequest(
    [Required]string Email, 
    [Required]string Username, 
    [Required]string Password);