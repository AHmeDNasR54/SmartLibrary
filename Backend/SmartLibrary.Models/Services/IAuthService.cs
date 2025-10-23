using SmartLibrary.Models.DTOs;
using SmartLibrary.Models;

namespace SmartLibrary.Models.Services
{
    public interface IAuthService
    {
        Task<AuthDto> RegisterAsync(RegisterDto model);
        Task<AuthDto> LoginAsync(LoginDto model);
        Task<string> AddRoleAsync(AddRoleDto model);

    }
}
