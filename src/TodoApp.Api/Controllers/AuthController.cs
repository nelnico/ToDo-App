using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.Api.Models.Auth;
using TodoApp.Core.Entities;
using TodoApp.Core.Enums;
using TodoApp.Core.Services;

namespace TodoApp.Api.Controllers;


public class AuthController : BaseApiController
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenService _tokenService;

    public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginModel model)
    {
        var user = await _userManager.Users
            .SingleOrDefaultAsync(x => x.UserName == model.EmailAddress.ToLower());

        if (user == null) return Unauthorized();

        var result = await _signInManager
            .CheckPasswordSignInAsync(user, model.Password, false);

        if (!result.Succeeded) return Unauthorized();

        var userRoles = await _userManager.GetRolesAsync(user);
        if (!userRoles.Any()) return Unauthorized();

        return Ok(new LoginResultModel
        {
            Username = user.UserName,
            Token = await _tokenService.CreateToken(user),
            Role = userRoles.First()
        });
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterModel model)
    {
        var existingUser = await _userManager.Users
            .SingleOrDefaultAsync(x => x.UserName == model.EmailAddress.ToLower());
        if(existingUser != null) return BadRequest("Username is taken");


        // in practice will verify email, etc but since this is a quickly built demo,
        // we set the account as active and return a user / token
        var user = new AppUser
        {
            UserName = model.EmailAddress.ToLower(),
            IsActive = true
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded) return BadRequest(result.Errors);

        var roleResult = await _userManager.AddToRoleAsync(user, UserRoleEnum.UserRole.Name);

        if (!roleResult.Succeeded) return BadRequest(result.Errors);

        return Ok(new LoginResultModel
        {
            Username = user.UserName,
            Token = await _tokenService.CreateToken(user),
            Role = UserRoleEnum.UserRole.Name
        });

    }
}