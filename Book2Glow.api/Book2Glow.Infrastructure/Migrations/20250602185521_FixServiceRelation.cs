using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Book2Glow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixServiceRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Service_ServiceModelId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_ServiceModelId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "ServiceModelId",
                table: "Bookings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ServiceModelId",
                table: "Bookings",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ServiceModelId",
                table: "Bookings",
                column: "ServiceModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Service_ServiceModelId",
                table: "Bookings",
                column: "ServiceModelId",
                principalTable: "Service",
                principalColumn: "S_Id");
        }
    }
}
