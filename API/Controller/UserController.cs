using Core.Auth.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    public class UserController(IUserService userService) : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetLoggedInUserAsync()
        {
            //var user = await userService.GetUserByUniqueIdAsync();
            return Ok();
        }
    }
}