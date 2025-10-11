using EventHub.DTOs;
using EventHub.Models;
using System.Runtime.Intrinsics.X86;

namespace EventHub.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserViewDto> GetUserByIdAsync(int id);
        Task<IEnumerable<UserViewDto>> GetUsersByEventIdAsync(int id);
        Task<IEnumerable<UserViewDto>> GetUsersByRoleAsync(string role);
        Task<IEnumerable<UserViewDto>> GetAllUsersAsync();
        Task UserRegisterAsync(UserRegisterDto dto);
        Task UserAuthAsync(UserAuthDto dto);
        Task UdateUserUsernameAsync(UserUsernameUpdateDto dto);
        Task UpdateUserPasswordAsync(UserPasswordUpdateDto dto);
        Task UpdateUserEmailAsync(UserEmailUpdateDto dto);
        Task UpdateUserRoleAsync(UserRoleUpdateDto dto);
        Task DeleteUserAsync(int id);
    }
}
