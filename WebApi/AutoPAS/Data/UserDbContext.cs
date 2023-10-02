using AutoPAS.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoPAS.Data
{
    public class UserDbContext:DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
            
        }
        public DbSet<portaluser> portaluser { get; set; }
        public DbSet<userpolicylist> userpolicylist { get; set; }
    }
}
