using System.Security.Claims;
using Juntos.Models;

namespace Juntos.Interfaces
{
    public interface IAuthService
    {
        string CreateToken(User user);
        string GetUserEmailFromToken();
        Task<User> GetUserObjFromToken();
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordHash(User user, string password);
        Task<User> RegisterUser(UserDto request);
        Task<AuthResponseDto> Login(UserDto request);
    }
}