using User_Dashboard.Models;
using Microsoft.EntityFrameworkCore;

namespace User_Dashboard.Data
{
    public class LoginContext : DbContext
    {
        public LoginContext(DbContextOptions options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserLevel> UserLevels { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
