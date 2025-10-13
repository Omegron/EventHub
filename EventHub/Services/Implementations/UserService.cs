using AutoMapper;
using EventHub.Data;
using EventHub.DTOs;
using EventHub.Models;
using EventHub.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;

namespace EventHub.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UserService(AppDbContext context, IMapper mapper, UserManager<User> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<UserViewDto> GetUserByIdAsync(string id)
        {
            var user = await _context.Users.FirstAsync(u => u.Id == id);
            return _mapper.Map<UserViewDto>(user);
        }
        public async Task<IEnumerable<UserViewDto>> GetUsersByEventIdAsync(int id)
        {
            var users = await _context.Bookings
                .Where(b => b.EventId == id)
                .Select(b => b.User)
                .ToListAsync();
            return _mapper.Map<IEnumerable<UserViewDto>>(users);
        }

        public async Task<IEnumerable<UserViewDto>> GetUsersByRoleAsync(string role)
        {
            var users = await _userManager.GetUsersInRoleAsync(role);
            return _mapper.Map<IEnumerable<UserViewDto>>(users);
        }
        public async Task<IEnumerable<UserViewDto>> GetAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            return _mapper.Map<IEnumerable<UserViewDto>>(users);
        }
        public async Task<string> GetUserRoleAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var role = await _userManager.GetRolesAsync(user);
            return role[0];
        }
        public async Task UserRegisterAsync(UserRegisterDto dto)
        {
            if (await _userManager.FindByNameAsync(dto.Username) != null) { throw new Exception("Username is already taken"); }
            if (await _userManager.FindByEmailAsync(dto.Email) != null) { throw new Exception("Email is already taken"); }
            if (dto.Password != dto.ConfirmPassword) { throw new Exception("Passwords don't match"); }

            var user = new User
            {
                UserName = dto.Username,
                Email = dto.Email
            };

            var newUser = await _userManager.CreateAsync(user, dto.Password);
            await _userManager.AddToRoleAsync(user, dto.Role);
        }
        public Task UserAuthAsync(UserAuthDto dto)
        {
            throw new NotImplementedException();
        }
        public async Task UpdateUserUsernameAsync(UserUsernameUpdateDto dto)
        {
            var userExists = await _userManager.FindByNameAsync(dto.NewUsername);
            if (userExists != null) { throw new Exception("Username is already taken"); }
            var user = await _userManager.FindByIdAsync(dto.Id);
            if (user != null) { user.UserName = dto.NewUsername; }
        }
        public async Task UpdateUserPasswordAsync(UserPasswordUpdateDto dto)
        {
            if (dto.NewPassword != dto.ConfirmNewPassword) { throw new Exception("Passwords dont't match"); }
            var user = await _userManager.FindByIdAsync(dto.Id);
            if (user == null) { throw new Exception("User not found"); }
            var result = await _userManager.ChangePasswordAsync(user, dto.OldPassword, dto.NewPassword);
            if (!result.Succeeded) { throw new Exception("Old password is wrong"); }
        }
        public async Task UpdateUserEmailAsync(UserEmailUpdateDto dto)
        {
            var userExists = await _userManager.FindByEmailAsync(dto.NewEmail);
            if (userExists != null) { throw new Exception("Email is already taken"); }
            var user = await _userManager.FindByIdAsync(dto.Id);
            if (user != null)
            {
                user.Email = dto.NewEmail;
            }
        }
        public async Task DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null) { await _userManager.DeleteAsync(user); }
        }
    }
}
