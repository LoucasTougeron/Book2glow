using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Book2Glow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddServicesAndRelatio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    S_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    S_Duration = table.Column<int>(type: "int", nullable: false),
                    S_Amount = table.Column<float>(type: "real", nullable: false),
                    S_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    S_CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.S_Id);
                    table.ForeignKey(
                        name: "FK_Service_Category_S_CategoryId",
                        column: x => x.S_CategoryId,
                        principalTable: "Category",
                        principalColumn: "C_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Service_S_CategoryId",
                table: "Service",
                column: "S_CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Service");
        }
    }
}
