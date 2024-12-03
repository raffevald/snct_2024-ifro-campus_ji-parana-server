using Application.Auth.Commands;
using Application.Auth.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("v1/Api/[controller]")]
    public class AuthController (
        IAuthService authService) : BaseController
    {

        private readonly IAuthService _authService = authService;

        [AllowAnonymous]
        [HttpPost]
        [Route("Authenticate")]
        public async Task<IActionResult> AuthenticateAsync(UserLoginCommand request)
        {
            var response = await _authService.AuthenticateAsync(request);
            return HandleResponse(response);
        }
    }
}
