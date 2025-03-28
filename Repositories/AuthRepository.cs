using LumoSoul.API.Data;
using LumoSoul.API.Interfaces;
using LumoSoul.API.Models;
using Microsoft.EntityFrameworkCore;

namespace LumoSoul.API.Repositories
{
    public class AuthRepository: IAuthRepository
    {
        private readonly LumoSoulDbContext _context;

        public AuthRepository(LumoSoulDbContext context)
        {
            _context = context;
        }

        public async Task<User> Login(string email, string password) 
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

            if (user == null || !VerifyPassword(password, user.PasswordHash))
            {
                return null;
            }

            return user;
        }


        // tim user da ton tai
        public async Task<bool> UserExists(string email)
        {
            return await _context.Users.AnyAsync(x => x.Email == email);
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            // BCrypt
            return password == storedHash;
        }

    }
}