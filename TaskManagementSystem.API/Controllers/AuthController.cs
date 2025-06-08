using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.API.DTOs;
using TaskManagementSystem.API.Services.Interfaces;

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
    public IActionResult Register(LoginRegisterDto dto)
    {
        var token = _authService.Register(dto);
        return Ok(new { Token = token });
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRegisterDto dto)
    {
        var token = _authService.Login(dto);
        return Ok(new { Token = token });
    }
}
