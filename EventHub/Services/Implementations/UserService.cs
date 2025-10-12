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
            var bookings = await _context.Bookings.Where(b => b.EventId == id).ToListAsync();
            var users = _context.Users;
            foreach (var booking in bookings)
            {
                var user = await _context.Users.FirstAsync(u => u.Id == booking.UserId);
                users.Add(user);
            }
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
            var userExists1 = await _userManager.FindByNameAsync(dto.Username);
            if (userExists1 != null) { throw new Exception("Username is already taken"); }
            var userExists2 = await _userManager.FindByEmailAsync(dto.Email);
            if (userExists2 != null) { throw new Exception("Email is already taken"); }
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
        public async Task UdateUserUsernameAsync(UserUsernameUpdateDto dto)
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
            if (user != null) { await _userManager.ChangePasswordAsync(user, user.PasswordHash, dto.NewPassword); }
        }
        public async Task UpdateUserEmailAsync(UserEmailUpdateDto dto)
        {
            var userExists = await _userManager.FindByEmailAsync(dto.NewEmail);
            if (userExists != null) { throw new Exception("Email is already taken"); }
            var user = await _userManager.FindByIdAsync(dto.Id);
            if (user != null) { await _userManager.ChangeEmailAsync(user, dto.NewEmail, null); }
        }
        public async Task DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null) { await _userManager.DeleteAsync(user); }
        }
    }
}
