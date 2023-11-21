using HotelApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelApplication.Contexts
{
    public class HotelsDbContext : DbContext
    {
        public HotelsDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        public DbSet<RoomAmenity> RoomAmenities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                    .HasMany(u => u.Rooms)
                    .WithOne(r => r.User)
                    .HasForeignKey(r => r.UserName)
                    .OnDelete(DeleteBehavior.NoAction);


            base.OnModelCreating(modelBuilder);
        }
    }
}
