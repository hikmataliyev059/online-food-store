using Microsoft.AspNetCore.Identity;

namespace FoodStore.Core.Entities;

public class AppUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? ConfirmKey { get; set; }
}