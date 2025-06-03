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

        public DbSet<ServiceModel> Services{ get; set; }

        public DbSet<BookingModel> Bookings { get; set; }

        public DbSet<BookModel> BookingsBooks { get; set; }

        public DbSet<BusinessCategoryModel> BusinessCategories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<BookingModel>()
    .HasOne(b => b.Service)
    .WithMany(s => s.Bookings)
    .HasForeignKey(b => b.ServiceId)
    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BookingModel>()
                .Property(b => b.StartDate)
                .HasColumnType("date"); 

            modelBuilder.Entity<BookModel>()
        .HasOne(b => b.User)
        .WithMany()
        .HasForeignKey(b => b.ApplicationUserId)
        .OnDelete(DeleteBehavior.Restrict); 

            
            modelBuilder.Entity<BookModel>()
                .HasOne(b => b.Business)
                .WithMany()
                .HasForeignKey(b => b.BusinessId)
                .OnDelete(DeleteBehavior.Restrict); 

            
            modelBuilder.Entity<BookModel>()
                .HasOne(b => b.Booking)
                .WithMany()
                .HasForeignKey(b => b.BookingId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BusinessCategoryModel>()
                .HasOne(bc => bc.Business)
                .WithMany(b => b.BusinessCategories)
                .HasForeignKey(bc => bc.BusinessId);

            modelBuilder.Entity<BusinessCategoryModel>()
                .HasOne(bc => bc.Category)
                .WithMany(c => c.BusinessCategories)
                .HasForeignKey(bc => bc.CategoryId);

            modelBuilder.Entity<ServiceModel>()
                .HasOne(s => s.BusinessCategory)
                .WithMany(bc => bc.Services)
                .HasForeignKey(s => s.BusinessCategoryId);
        }
    }
}

