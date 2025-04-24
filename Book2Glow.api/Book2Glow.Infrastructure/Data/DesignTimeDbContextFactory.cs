using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book2Glow.Infrastructure.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DataModelContext>
    {
        public DataModelContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataModelContext>();

            optionsBuilder.UseNpgsql("Host=bu6ioopbae4kp03qmpql-postgresql.services.clever-cloud.com;Database=bu6ioopbae4kp03qmpql;Username=u8gphyko5w4brsv1byg7;Password=XHmKDDYl9mTMJsOuXP2g;Port=7473;SSL Mode=Require;Trust Server Certificate=true");

            return new DataModelContext(optionsBuilder.Options);
        }
    }
}
