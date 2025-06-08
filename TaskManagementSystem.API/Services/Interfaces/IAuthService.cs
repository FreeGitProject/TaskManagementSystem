using TaskManagementSystem.API.DTOs;

namespace TaskManagementSystem.API.Services.Interfaces;

public interface IAuthService
{
    string Register(LoginRegisterDto dto);
    string Login(LoginRegisterDto dto);
}
