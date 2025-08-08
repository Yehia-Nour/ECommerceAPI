using ECommerceAPI.DTOs;
using ECommerceAPI.DTOs.CustomerDTOs;
using ECommerceAPI.Services.Implementations;
using ECommerceAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ActionResult<ApiResponse<string>>> Login([FromBody] LoginDTO loginDto)
        {
            var response = await _authService.LoginAsync(loginDto);
            if (!response.Success)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }

        [HttpPost("ChangePassword")]
        public async Task<ActionResult<ApiResponse<ConfirmationResponseDTO>>> ChangePassword([FromBody] ChangePasswordDTO changePasswordDto)
        {
            var response = await _authService.ChangePasswordAsync(changePasswordDto);
            if (!response.Success)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }

    }
}