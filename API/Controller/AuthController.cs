using Auth.Common.Dtos;
using Auth.Core.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    public class AuthController(
        IAuthService authService)
        : BaseController
    {
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequestDto request)
        {
            await authService.LoginAsync(request.Username, request.Password);

            return Ok();
        }
    }
}