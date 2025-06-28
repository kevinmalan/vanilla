using Common.Auth.Dtos;
using Core.Auth.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    public class AuthController(
        IAuthService authService)
        : BaseController
    {
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequestDto request)
        {
            await authService.LoginAsync(request.Username, request.Password);

            return Ok();
        }
    }
}