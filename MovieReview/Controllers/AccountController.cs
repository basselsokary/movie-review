using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieReview.Data.Services.Implementations;
using MovieReview.Data.Static;
using MovieReview.Models.DTOs;
using MovieReview.Models.Entities;

namespace MovieReview.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly TokenService _tokenService;

    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, TokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user != null)
        {
            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (result.Succeeded)
            {
                var token = _tokenService.GenerateToken(user.Id, user.UserName, await _userManager.GetRolesAsync(user));
                return Ok(
                    new NewUserDto
                    {
                        Email = user.Email,
                        UserName = user.UserName,
                        Token = token
                    }
                );
            }
        }

        return Unauthorized("Invalid email and/or password.");
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterDto dto) => await Register(dto, UserRoles.User);

    [HttpPost("register-admin")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> RegisterAdmin([FromBody] RegisterDto dto) => await Register(dto, UserRoles.Admin);

    private async Task<IActionResult> Register(RegisterDto dto, string role)
    {
        var existingUser = await _userManager.FindByEmailAsync(dto.Email);
        if (existingUser != null)
        {
            return BadRequest(ApiResponse<RegisterDto>.CreateFailure("Email already in use."));
        }

        var user = new AppUser { UserName = dto.UserName, Email = dto.Email};

        var userResult = await _userManager.CreateAsync(user, dto.Password);
        if (userResult.Succeeded)
        {
            var roleResult = await _userManager.AddToRoleAsync(user, role);
            if (roleResult.Succeeded)
            {
                return Ok("User registered successfully!");
            }

            return StatusCode(500, roleResult.Errors.Select(r => r.Description)); // Internal Server Error
        }

        return StatusCode(500, userResult.Errors.Select(r => r.Description)); // Internal Server Error
    }
}
