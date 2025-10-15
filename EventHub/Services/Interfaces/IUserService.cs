using EventHub.DTOs;

namespace EventHub.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserViewDto> GetUserByIdAsync(string id); //UserC, AdminC
        Task<IEnumerable<UserViewDto>> GetUsersByEventIdAsync(int id); //EventsC, AdminC
        Task<IEnumerable<UserViewDto>> GetUsersByRoleAsync(string role); //AdminC
        Task<IEnumerable<UserViewDto>> GetAllUsersAsync(); //AdminC
        Task<string> GetUserRoleAsync(string id); //AdminC
        Task UserRegisterAsync(UserRegisterDto dto); //UserC
        Task UserAuthAsync(UserAuthDto dto); //UserC
        Task UpdateUserUsernameAsync(UserUsernameUpdateDto dto); //UserC
        Task UpdateUserPasswordAsync(UserPasswordUpdateDto dto); //UserC
        Task UpdateUserEmailAsync(UserEmailUpdateDto dto); //UserC
        Task DeleteUserAsync(string id); //UserC, AdminC
    }
}
