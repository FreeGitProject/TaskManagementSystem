using TaskManagementSystem.API.DTOs;

namespace TaskManagementSystem.API.Services.Interfaces;

public interface IAuthService
{
    Task<string> Register(LoginRegisterDto dto);
    Task<string> Login(LoginRegisterDto dto);
}
