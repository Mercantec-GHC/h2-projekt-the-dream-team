using H2Projekt.Domain;
using Microsoft.EntityFrameworkCore;

namespace H2Projekt.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<Booking> Bookings => Set<Booking>();
        public DbSet<Guest> Guests => Set<Guest>();
        public DbSet<Room> Rooms => Set<Room>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
