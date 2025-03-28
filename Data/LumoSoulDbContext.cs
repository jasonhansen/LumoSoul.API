using LumoSoul.API.Models;
using Microsoft.EntityFrameworkCore;

namespace LumoSoul.API.Data
{
    public class LumoSoulDbContext: DbContext
    {
        public LumoSoulDbContext(DbContextOptions<LumoSoulDbContext> options) : base(options){}

        // set DBSet cho các model
        public DbSet<User> Users {get;set;}
        // public DbSet<Post> Users {get;set;}
        // public DbSet<User> Users {get;set;}

        
    }
}