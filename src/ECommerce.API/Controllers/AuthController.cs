using ECommerce.Application.DTOs;
using ECommerce.Application.DTOs.CustomerDTOs;
using ECommerce.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult> Register([FromBody] CustomerRegistrationDTO customerDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new ApiResponse<string>(400, errors));
            }

            var response = await _authService.RegisterAsync(customerDto);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<string>>> Login([FromBody] LoginDTO loginDto)
        {
            var response = await _authService.LoginAsync(loginDto);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("ChangePassword")]
        public async Task<ActionResult<ApiResponse<ConfirmationResponseDTO>>> ChangePassword([FromBody] ChangePasswordDTO changePasswordDto)
        {
            var response = await _authService.ChangePasswordAsync(changePasswordDto);

            return StatusCode(response.StatusCode, response);
        }

    }
}