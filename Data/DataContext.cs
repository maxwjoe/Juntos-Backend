using Juntos.Models;
using Microsoft.EntityFrameworkCore;

namespace Juntos.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Club> Clubs => Set<Club>();
        public DbSet<Membership> Memberships => Set<Membership>();
        public DbSet<Event> Events => Set<Event>();

    }
}