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
        public DbSet<RoomDiscount> RoomDiscounts => Set<RoomDiscount>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Room numbers must be unique
            modelBuilder.Entity<Room>()
                .HasIndex(r => r.Number)
                .IsUnique();

            // Room type names must be unique
            modelBuilder.Entity<RoomType>()
                .HasIndex(rt => rt.Name)
                .IsUnique();

            // Money precision
            modelBuilder.Entity<RoomType>()
                .Property(rt => rt.PricePerNight)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Booking>()
                .Property(r => r.PriceLocked)
                .HasPrecision(10, 2);

            // Guest
            modelBuilder.Entity<Guest>(g =>
            {
                g.Property(g => g.FirstName)
                    .HasMaxLength(50)
                    .IsRequired();
                g.Property(g => g.LastName)
                    .HasMaxLength(50)
                    .IsRequired();
                g.Property(g => g.Email)
                    .HasMaxLength(100)
                    .IsRequired();
            });

            // On delete behavior for bookings when a guest is deleted - cascade delete
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Guest)
                .WithMany(g => g.Bookings)
                .HasForeignKey(b => b.GuestId)
                .OnDelete(DeleteBehavior.Cascade);

            // On delete behavior for bookings when a room is deleted - restrict delete
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Room)
                .WithMany(r => r.Bookings)
                .HasForeignKey(b => b.RoomId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
