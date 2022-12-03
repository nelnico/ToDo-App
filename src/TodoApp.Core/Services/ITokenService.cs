using TodoApp.Core.Entities;

namespace TodoApp.Core.Services;

public interface ITokenService
{
    Task<string> CreateToken(AppUser user);
    string GenerateOneTimePin(int length = 5);
}