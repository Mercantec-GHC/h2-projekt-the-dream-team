using H2Projekt.Domain;
using Microsoft.EntityFrameworkCore;

namespace H2Projekt.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<Booking> Bookings => Set<Booking>();
        public DbSet<Guest> Guests => Set<Guest>();
        public DbSet<Room> Rooms => Set<Room>();
        public DbSet<RoomType> RoomTypes => Set<RoomType>();
        public DbSet<RoomRate> RoomRates => Set<RoomRate>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Room numbers must be unique
            modelBuilder.Entity<Room>()
                .HasIndex(r => r.Number)
                .IsUnique();

            // Room type names must be unique
            modelBuilder.Entity<RoomType>()
                .HasIndex(r => r.Name)
                .IsUnique();

            // Money precision
            modelBuilder.Entity<RoomRate>()
                    .Property(r => r.PricePerNight)
                    .HasPrecision(10, 2);

            modelBuilder.Entity<Booking>()
                .Property(r => r.PriceLocked)
                .HasPrecision(10, 2);
        }
    }
}
