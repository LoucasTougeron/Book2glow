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

            optionsBuilder.UseNpgsql("Host=aws-0-eu-west-2.pooler.supabase.com;Port=5432;Database=postgres;Username=postgres.fnkhglnwexlvyvjgwwda;Password=tiyVl3LCLYCxSt29;SSL Mode=Require;Trust Server Certificate=true;");

            return new DataModelContext(optionsBuilder.Options);
        }
    }
}