using Microsoft.AspNetCore.Identity;
using TodoApp.Core.Entities;
using TodoApp.Core.Enums;
using TodoApp.Core.Services;

namespace TodoApp.Data;

public static class SeedData
{
    private static IUnitOfWork _uow;

    public static async Task SeedUsers(IUnitOfWork uow, UserManager<AppUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _uow = uow;


        var usersToCreate = new List<SeedUserModel>
        {
            new()
            {
                Email = "user@mysite.com", Password = "Password@123", Role = UserRoleEnum.UserRole.Name
            },
            new()
            {
                Email = "admin@mysite.com", Password = "Password@123", Role = UserRoleEnum.AdminRole.Name
            }
        };

        foreach (var role in Enumeration.GetAll<UserRoleEnum>())
        {
            var roleExists = await roleManager.RoleExistsAsync(role.Name);
            if (!roleExists) await roleManager.CreateAsync(new IdentityRole(role.Name));
        }

        foreach (var userToCreate in usersToCreate)
        {
            if (userManager.Users.Any(r => r.UserName == userToCreate.Email)) continue;

            var user = new AppUser
            {
                UserName = userToCreate.Email,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            await userManager.CreateAsync(user, userToCreate.Password);
            await userManager.AddToRoleAsync(user, userToCreate.Role);
        }
    }
}

public class SeedUserModel
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
}