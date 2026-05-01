using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using TrackMyCoins.Api.Data;
using TrackMyCoins.Api.Models.Entities;
using TrackMyCoins.Api.Models.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using TrackMyCoins.Api.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TrackMyCoins.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger _logger;
        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            
            _authService = authService;
            _logger = logger;
        }

        private bool IsAdmin()
        {
            var isAdmin = User.Claims.FirstOrDefault(c => c.Type == "IsAdmin")?.Value;
            return isAdmin == "True";
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            _logger.LogInformation("Register attempt for {Email}", dto.Email);

            var user = await _authService.RegisterAsync(dto);
            if (user is null) 
            {
                return BadRequest(new { message = "Email already in use" });
            }
            _logger.LogInformation("User registered successfully: {Email}", dto.Email);
            return Ok(new { message = "User registered successfully!!" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto )
        {
            _logger.LogInformation("Login attempt for {Email}", dto.Email);
            var token = await _authService.LoginAsync(dto);
            if (token is null)
            {
                return BadRequest(new { message = "Wrong username or password." });
            }
            _logger.LogInformation("User logged in successfully");
             return Ok(new { token = token });


            }
    }
}
