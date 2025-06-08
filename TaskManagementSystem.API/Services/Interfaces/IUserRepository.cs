using TaskManagementSystem.API.Models;

namespace TaskManagementSystem.API.Services.Interfaces;

public interface IUserRepository
{
    Task<User> GetByUsernameAsync(string username);
    Task AddAsync(User user);
    Task<bool> ExistsAsync(string username);
}