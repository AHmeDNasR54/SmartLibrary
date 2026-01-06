using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartLibrary.Models.DTOs;
using SmartLibrary.Models.Services;

namespace Smart_Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponseDto<object>
                {
                    Success = false,
                    Message = "Validation error",
                    Errors = ModelState
                        .Where(x => x.Value.Errors.Count > 0)
                        .ToDictionary(
                            k => k.Key,
                            v => v.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                        )
                });

            var result = await _authService.RegisterAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(new ApiResponseDto<object>
                {
                    Success = false,
                    Message = result.Message,
                });
            return Ok(new ApiResponseDto<AuthDto>
            {
                Success = true,
                Message = "Registered successfully",
                Data = result
            });
        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponseDto<object>
                {
                    Success = false,
                    Message = "Validation error",
                    Errors = ModelState
                                        .Where(x => x.Value.Errors.Count > 0)
                                        .ToDictionary(
                                            k => k.Key,
                                            v => v.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                                        )
                });

            var result = await _authService.LoginAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(new ApiResponseDto<object>
                {
                    Success = false,
                    Message = result.Message,
                });
            return Ok(new ApiResponseDto<AuthDto>
            {
                Success = true,
                Message = "Login successful",
                Data = result
            });
        }
        [HttpPost("addrole")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddRoleAsync([FromBody] AddRoleDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponseDto<object>
                {
                    Success = false,
                    Message = "Validation error",
                    Errors = ModelState
                                        .Where(x => x.Value.Errors.Count > 0)
                                        .ToDictionary(
                                            k => k.Key,
                                            v => v.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                                        )
                });
            var result = await _authService.AddRoleAsync(model);

            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);

            return Ok(model);
        }
    }
}
