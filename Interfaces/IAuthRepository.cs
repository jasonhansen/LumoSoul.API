using LumoSoul.API.Models;

namespace LumoSoul.API.Interfaces
{
    public interface IAuthRepository
    {
        Task<User> Login(string email, string password);
        Task<bool> UserExists(string email);
    }
}