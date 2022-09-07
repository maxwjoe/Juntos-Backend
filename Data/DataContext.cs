// using Juntos.Models;
using Juntos.Models;
using Microsoft.EntityFrameworkCore;

namespace Juntos.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Event> Events { get; set; }

    }
}