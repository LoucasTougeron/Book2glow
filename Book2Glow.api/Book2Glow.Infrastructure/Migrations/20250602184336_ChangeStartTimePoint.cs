using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Book2Glow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeStartTimePoint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
        ALTER TABLE ""Bookings""
        ALTER COLUMN ""B_StartTime"" TYPE integer
        USING EXTRACT(EPOCH FROM ""B_StartTime"")::int / 60;
    ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
        ALTER TABLE ""Bookings""
        ALTER COLUMN ""B_StartTime"" TYPE time
        USING make_time(""B_StartTime"" / 60, ""B_StartTime"" % 60, 0);
    ");
        }
    }
}
