using Core.Auth.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controller
{
    public class UserController(IUserService userService) : BaseController
    {
        /// <summary>
        /// Example using Roles and Claims
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetLoggedInUserAsync()
        {
            var username = User.Identity?.Name;
            var role = User.FindFirstValue(ClaimTypes.Role);
            var uniqueId = Guid.Parse(User.FindFirst("unique-id")?.Value ?? $"{Guid.Empty}");
            var status = User.FindFirst("status")?.Value;

            var user = await userService.GetUserByUniqueIdAsync(uniqueId);
            
            return Ok(user);
        }
    }
}