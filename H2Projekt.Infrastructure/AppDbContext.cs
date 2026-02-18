using H2Projekt.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

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

            // Unique constraints
            modelBuilder.Entity<Room>()
                .HasIndex(r => r.Number)
                .IsUnique();

            modelBuilder.Entity<RoomType>()
                .HasIndex(rt => rt.Name)
                .IsUnique();

            // Money precision
            modelBuilder.Entity<RoomType>()
                .Property(rt => rt.PricePerNight)
                .HasPrecision(10, 2);

            modelBuilder.Entity<RoomDiscount>()
                .Property(rd => rd.PricePerNight)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Booking>()
                .Property(b => b.PriceLocked)
                .HasPrecision(10, 2);

            // Max length and required fields
            modelBuilder.Entity<RoomDiscount>(rd =>
            {
                rd.Property(rd => rd.Description)
                    .HasMaxLength(200)
                    .IsRequired();
            });

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
