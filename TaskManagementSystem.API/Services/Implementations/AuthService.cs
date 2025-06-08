using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagementSystem.API.DTOs;
using TaskManagementSystem.API.Models;
using TaskManagementSystem.API.Services.Interfaces;

namespace TaskManagementSystem.API.Services.Implementations;

public class AuthService : IAuthService
{
    private static List<User> _users = new(); // Mocked for now
    private readonly IConfiguration _config;

    public AuthService(IConfiguration config)
    {
        _config = config;
    }

    public string Register(LoginRegisterDto dto)
    {
        if (_users.Any(u => u.Username == dto.Username))
            throw new Exception("User exists");

        var user = new User
        {
            Id = _users.Count + 1,
            Username = dto.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
        };

        _users.Add(user);
        return GenerateToken(user);
    }

    public string Login(LoginRegisterDto dto)
    {
        var user = _users.FirstOrDefault(u => u.Username == dto.Username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            throw new Exception("Invalid credentials");

        return GenerateToken(user);
    }

    private string GenerateToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
