using TrackMyCoins.Api.Models.DTOs;
using TrackMyCoins.Api.Models.Entities;

namespace TrackMyCoins.Api.Services.Interfaces
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(RegisterDTO dto);
        Task<string?> LoginAsync(LoginDTO dto);

    }
}
