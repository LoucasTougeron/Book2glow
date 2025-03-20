using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Book2Glow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBuisnessTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Buisness",
                columns: table => new
                {
                    B_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    B_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    B_City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    B_PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    B_Latitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    B_Longitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    B_Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    B_Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    B_Website = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    B_UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buisness", x => x.B_Id);
                    table.ForeignKey(
                        name: "FK_Buisness_AspNetUsers_B_UserId",
                        column: x => x.B_UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Buisness_B_UserId",
                table: "Buisness",
                column: "B_UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Buisness");
        }
    }
}
