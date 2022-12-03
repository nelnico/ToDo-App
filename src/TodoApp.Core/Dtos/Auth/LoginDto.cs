using System.ComponentModel.DataAnnotations;

namespace TodoApp.Core.Dtos.Auth;

public class LoginDto
{
    [Required] public string Username { get; set; }

    [Required] public string Password { get; set; }
}