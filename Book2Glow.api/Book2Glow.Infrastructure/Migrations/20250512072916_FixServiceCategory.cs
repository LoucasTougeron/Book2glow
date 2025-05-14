using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Book2Glow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixServiceCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Business_Category_CategoryModelId",
                table: "Business");

            migrationBuilder.DropIndex(
                name: "IX_Business_CategoryModelId",
                table: "Business");

            migrationBuilder.DropColumn(
                name: "CategoryModelId",
                table: "Business");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CategoryModelId",
                table: "Business",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Business_CategoryModelId",
                table: "Business",
                column: "CategoryModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Business_Category_CategoryModelId",
                table: "Business",
                column: "CategoryModelId",
                principalTable: "Category",
                principalColumn: "C_Id");
        }
    }
}
