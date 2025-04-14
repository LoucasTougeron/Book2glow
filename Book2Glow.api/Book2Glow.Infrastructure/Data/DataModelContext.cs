using Book2Glow.Infrastructure.Data.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Book2Glow.Infrastructure.Data
{
    public class DataModelContext : IdentityDbContext<ApplicationUser>
    {

        public DataModelContext(DbContextOptions<DataModelContext> options) : base(options)
        {

        }
        public DbSet<BusinessModel> Businesses { get; set; }

        public DbSet<CategoryModel> Categories { get; set; }

        public DbSet<ServiceModel> serviceModels { get; set; }

        public DbSet<BookingModel> Bookings { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ServiceModel>()
                .HasOne(s => s.Category)
                .WithMany(c => c.Services)
                .HasForeignKey(s => s.CategoryId);

            modelBuilder.Entity<BookingModel>()
                .HasOne(b => b.Service)
                .WithMany() 
                .HasForeignKey(b => b.ServiceId)
                .OnDelete(DeleteBehavior.Cascade);

            // 🗓️ Mapping de DateOnly
            modelBuilder.Entity<BookingModel>()
                .Property(b => b.StartDate)
                .HasColumnType("date"); 

            modelBuilder.Entity<BookingModel>()
                .Property(b => b.StartTime)
                .HasColumnType("time"); 

        }
    }
}

