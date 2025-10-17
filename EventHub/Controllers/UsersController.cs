using EventHub.DTOs;
using EventHub.Models;
using EventHub.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Runtime.Intrinsics.X86;
using System.Xml.Linq;

namespace EventHub.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return View(user);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDto dto)
        {
            await _userService.UserRegisterAsync(dto);
            return View(); //add redirect to user's page
        }

        [HttpGet]
        public IActionResult Auth()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Auth(UserAuthDto dto)
        {
            await _userService.UserAuthAsync(dto);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditUsername(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            var dto = new UserUsernameUpdateDto
            {
                Id = user.Id,
                NewUsername = user.Username
            };
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> EditUsername(UserUsernameUpdateDto dto)
        {
            await _userService.UpdateUserUsernameAsync(dto);
            return RedirectToAction(nameof(Details), new { id = dto.Id });
        }

        [HttpGet]
        public IActionResult EditPassword(string id)
        {
            var dto = new UserPasswordUpdateDto
            {
                Id = id,
                OldPassword = "",
                NewPassword = "",
                ConfirmNewPassword = ""
            };
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> EditPassword(UserPasswordUpdateDto dto)
        {
            await _userService.UpdateUserPasswordAsync(dto);
            return RedirectToAction(nameof(Details), new { id = dto.Id });
        }

        [HttpGet]
        public async Task<IActionResult> EditEmail(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            var dto = new UserEmailUpdateDto
            {
                Id = user.Id,
                NewEmail = user.Email
            };
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> EditEmail(UserEmailUpdateDto dto)
        {
            await _userService.UpdateUserEmailAsync(dto);
            return RedirectToAction(nameof(Details), new { id = dto.Id });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            await _userService.DeleteUserAsync(id);
            return View();
        }
    }
}
