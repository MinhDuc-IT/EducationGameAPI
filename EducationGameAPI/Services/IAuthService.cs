using EducationGameAPI.Entities;
using EducationGameAPI.Models;

namespace EducationGameAPI.Services
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(UserDto request);
        Task<TokenResponseDto> LoginAsync(UserDto request);
        Task<TokenResponseDto> RefreshTokenAsync(RefreshTokenRequestDto request);
    }
}
