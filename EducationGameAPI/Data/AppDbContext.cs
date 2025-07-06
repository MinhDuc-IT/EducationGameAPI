using EducationGameAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace EducationGameAPI.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<GameSession> GameSessions { get; set; }
    }
}
