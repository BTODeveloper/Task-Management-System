using TaskManagementAPI.DTOs;
using TaskManagementAPI.Models;

public interface IAuthService
{
    Task<string> Register(RegisterDTO registerDto);
    Task<string> Login(LoginDTO loginDto);
    string GenerateJwtToken(User user);
}