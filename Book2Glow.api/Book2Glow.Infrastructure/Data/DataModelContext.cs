using Book2Glow.Infrastructure.Data.Model;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Book2Glow.Infrastructure.Data
{
    public class DataModelContext : DbContext
    {

        public DbSet<Client> clients { get; set; }
        public DbSet<Provider> providers { get; set; }
        public DataModelContext(DbContextOptions<DataModelContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);

            // Relation 1-1 entre User et Client
            modelBuilder.Entity<ApplicationUser>()
                .HasOne(u => u.Client)
                .WithOne(c => c.User)
                .HasForeignKey<Client>(c => c.Id);

            // Relation 1-1 entre User et Provider
            modelBuilder.Entity<ApplicationUser>()
                .HasOne(u => u.Provider)
                .WithOne(p => p.User)
                .HasForeignKey<Provider>(p => p.Id);
        }
    }
}
}
