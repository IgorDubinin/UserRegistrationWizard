using Microsoft.AspNetCore.Mvc;
using UserRegistrationWizard.Server.Interfaces;
using UserRegistrationWizard.Server.Models.Dto;

namespace UserRegistrationWizard.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto, CancellationToken cancellationToken)
        {
            if (registerDto.Password != registerDto.ConfirmPassword)
            {
                return BadRequest("Passwords do not match");
            }

            if (await _userService.UserExistsByEmailAsync(registerDto.Email, cancellationToken))
            {
                return BadRequest("Email already exists");
            }

            await _userService.RegisterUserAsync(registerDto, cancellationToken);

            return Ok("User registered");
        }
    }
}
