using Microsoft.EntityFrameworkCore;
using PiSec.Api.Entities;
using PiSec.Api.Model;

namespace PiSec.Api.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
