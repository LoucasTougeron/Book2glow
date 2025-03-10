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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);

        }
    }
}

